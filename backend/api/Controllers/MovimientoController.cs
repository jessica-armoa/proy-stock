using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.DetalleDeMovimiento;
using api.Dtos.Movimiento;
using api.Dtos.Producto;
using api.Interfaces;
using api.Mapper;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/movimiento")]
    [ApiController]
    public class MovimientoController : ControllerBase
    {
        private readonly IMovimientoRepository _movimientoRepo;
        //private readonly IDepositoRepository _depositoRepo;
        private readonly ITipoDeMovimientoRepository _tipoDeMovimientoRepo;
        private readonly ApplicationDbContext _context;
        private readonly IProductoRepository _productoRepo;
        private readonly IDetalleDeMovimientosRepository _detalleRepo;
        public MovimientoController(IMovimientoRepository movimientoRepo, IDepositoRepository depositoRepo, ITipoDeMovimientoRepository tipoDeMovimientoRepo, ApplicationDbContext context, IProductoRepository productoRepo, IDetalleDeMovimientosRepository detalleRepo)
        {
            _movimientoRepo = movimientoRepo;
            //_depositoRepo = depositoRepo;
            _tipoDeMovimientoRepo = tipoDeMovimientoRepo;
            _context = context;
            _productoRepo = productoRepo;
            _detalleRepo = detalleRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            /*var movimientos = await _movimientoRepo.GetAllAsync();
            var movimientosDto = movimientos.Select(m => m.ToMovimientoDto());
            return Ok(movimientosDto);*/
            var movimientos = await _context.movimientos
            .Include(m => m.DetallesDeMovimientos)
            .Select(m => new MovimientoDto
            {
                Id = m.Id,
                Date_fecha = m.Date_fecha,
                TipoDeMovimientoId = m.TipoDeMovimientoId,
                DepositoOrigenId = m.DepositoOrigenId,
                DepositoDestinoId = m.DepositoDestinoId,
                Bool_borrado = m.Bool_borrado,
                DetallesDeMovimientos = m.DetallesDeMovimientos
                .Where(d => d.Bool_borrado != true)
                .Select(d => new DetalleDeMovimientoDto
                {
                    Id = d.Id,
                    ProductoId = d.ProductoId,
                    MovimientoId = d.MovimientoId,
                    Int_cantidad = d.Int_cantidad,
                    Bool_borrado = d.Bool_borrado
                }).ToList()
            })
            .ToListAsync();

            return Ok(movimientos);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var movimiento = await _movimientoRepo.GetByIdAsync(id);
            if (movimiento == null) return NotFound();

            return Ok(movimiento.ToMovimientoDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MovimientoDto movimiento)
        {
            if (movimiento == null || movimiento.DetallesDeMovimientos == null || !movimiento.DetallesDeMovimientos.Any())
            {
                return BadRequest("Movimiento o detalles del movimiento no válidos.");
            }

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                var tipoDeMovimiento = await _tipoDeMovimientoRepo.GetByIdAsync(movimiento.TipoDeMovimientoId);
                if (tipoDeMovimiento == null)
                {
                    BadRequest();
                }
                try
                {
                    var nuevoMovimiento = new CreateMovimientoRequestDto
                    {
                        Date_fecha = movimiento.Date_fecha
                    };
                    var movimientoModel = nuevoMovimiento.ToMovimientoFromCreate(movimiento.TipoDeMovimientoId, movimiento.DepositoOrigenId, movimiento.DepositoDestinoId);
                    await _movimientoRepo.CreateAsync(movimientoModel);

                    foreach (var detalle in movimiento.DetallesDeMovimientos)
                    {
                        var producto = await _context.productos
                            .Where(p => p.Bool_borrado != true)
                            .FirstOrDefaultAsync(p => p.Id == detalle.ProductoId && p.DepositoId == movimientoModel.DepositoOrigenId);

                        if (producto == null)
                        {
                            return BadRequest("Producto no encontrado en el depósito origen.");
                        }

                        if (tipoDeMovimiento.Str_descripcion.ToLower() == "ingreso")
                        {
                            var nuevoDetalle = new CreateDetalleRequestDto
                            {
                                Int_cantidad = detalle.Int_cantidad,
                            };

                            var detalleModel = nuevoDetalle.ToDetalleFromCreate(movimientoModel.Id, detalle.ProductoId);
                            await _detalleRepo.CreateAsync(detalleModel);
                        }
                        else if (tipoDeMovimiento.Str_descripcion.ToLower() == "egreso")
                        {
                            if (producto.Int_cantidad_actual < detalle.Int_cantidad)
                            {
                                return BadRequest("Cantidad insuficiente en el depósito origen.");
                            }

                            var nuevoDetalle = new CreateDetalleRequestDto
                            {
                                Int_cantidad = detalle.Int_cantidad,
                            };

                            var detalleModel = nuevoDetalle.ToDetalleFromCreate(movimientoModel.Id, detalle.ProductoId);
                            await _detalleRepo.CreateAsync(detalleModel);
                        }
                        else if (tipoDeMovimiento.Str_descripcion.ToLower() == "transferencia")
                        {
                            if (producto.Int_cantidad_actual < detalle.Int_cantidad)
                            {
                                return BadRequest("Cantidad insuficiente en el depósito origen.");
                            }
                            producto.Int_cantidad_actual -= detalle.Int_cantidad;

                            var productoDestino = await _context.productos
                                .Where(p => p.Bool_borrado != true)
                                .FirstOrDefaultAsync(p => p.Str_nombre == producto.Str_nombre && p.DepositoId == movimiento.DepositoDestinoId);

                            if (productoDestino == null)
                            {
                                var nuevoProductoDestino = new CreateProductoCantidadDto
                                {
                                    Str_ruta_imagen = producto.Str_ruta_imagen,
                                    Str_nombre = producto.Str_nombre,
                                    Str_descripcion = producto.Str_descripcion,
                                    Int_cantidad_actual = detalle.Int_cantidad,
                                    Int_cantidad_minima = producto.Int_cantidad_minima,
                                    Dec_costo = producto.Dec_costo,
                                    Dec_costo_PPP = producto.Dec_costo_PPP,
                                    Int_iva = producto.Int_iva,
                                    Dec_precio_mayorista = producto.Dec_precio_mayorista,
                                    Dec_precio_minorista = producto.Dec_precio_minorista,
                                    Bool_borrado = false
                                };
                                var productoModel = nuevoProductoDestino.ToProductoCantidadFromCreate(movimiento.DepositoDestinoId, producto.ProveedorId, producto.MarcaId);
                                await _productoRepo.CreateAsync(productoModel);

                                var nuevoDetalle = new CreateDetalleRequestDto
                                {
                                    Int_cantidad = detalle.Int_cantidad,
                                };
                                var detalleModel = nuevoDetalle.ToDetalleFromCreate(movimientoModel.Id, detalle.ProductoId);
                                await _detalleRepo.CreateAsync(detalleModel);
                            }
                            else
                            {
                                productoDestino.Int_cantidad_actual += detalle.Int_cantidad;
                                var nuevoDetalle = new CreateDetalleRequestDto
                                {
                                    Int_cantidad = detalle.Int_cantidad,
                                };
                                var detalleModel = nuevoDetalle.ToDetalleFromCreate(movimientoModel.Id, detalle.ProductoId);
                                await _detalleRepo.CreateAsync(detalleModel);
                            }
                        }
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return Ok(movimientoModel.ToMovimientoDto());
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return StatusCode(500, $"Error interno del servidor: {ex.Message}");
                }
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMovimientoRequestDto dto)
        {
            return Ok();
        }
    }
}