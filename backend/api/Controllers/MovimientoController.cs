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
        private readonly IDepositoRepository _depositoRepo;
        private readonly ITipoDeMovimientoRepository _tipoDeMovimientoRepo;
        private readonly ApplicationDbContext _context;
        private readonly IProductoRepository _productoRepo;
        public MovimientoController(IMovimientoRepository movimientoRepo, IDepositoRepository depositoRepo, ITipoDeMovimientoRepository tipoDeMovimientoRepo, ApplicationDbContext context, IProductoRepository productoRepo)
        {
            _movimientoRepo = movimientoRepo;
            _depositoRepo = depositoRepo;
            _tipoDeMovimientoRepo = tipoDeMovimientoRepo;
            _context = context;
            _productoRepo = productoRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var movimientos = await _movimientoRepo.GetAllAsync();
            var movimientosDto = movimientos.Select(m => m.ToMovimientoDto());
            return Ok(movimientosDto);
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
        public async Task<IActionResult> CreateMovimiento([FromBody] MovimientoDto movimientoDto)
        {
            var tipoDeMovimiento = await _context.tipos_de_movimientos.FindAsync(movimientoDto.TipoDeMovimientoId);
            if (tipoDeMovimiento == null)
            {
                return BadRequest("Tipo de movimiento no válido");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var nuevoMovimiento = new CreateMovimientoRequestDto
                {
                    Date_fecha = movimientoDto.Date_fecha
                };
                var movimientoModel = nuevoMovimiento.ToMovimientoFromCreate(tipoDeMovimiento.Id, movimientoDto.DepositoOrigenId, movimientoDto.DepositoDestinoId);

                foreach (var detalleDto in movimientoDto.DetallesDeMovimientos)
                {
                    var producto = await _context.productos.FindAsync(detalleDto.ProductoId);
                    if (producto == null)
                    {
                        return BadRequest($"Producto con ID {detalleDto.ProductoId} no encontrado");
                    }

                    var nuevoDetalle = new CreateDetalleRequestDto
                    {
                        Int_cantidad = detalleDto.Int_cantidad
                    };
                    var detalleModel = nuevoDetalle.ToDetalleFromCreate(detalleDto.MovimientoId, detalleDto.ProductoId);
                    movimientoModel.DetallesDeMovimientos.Add(detalleModel);

                    switch (tipoDeMovimiento.Str_descripcion.ToLower())
                    {
                        case "ingreso":
                            await HandleIngreso(detalleDto, movimientoModel);
                            break;

                        case "egreso":
                            await HandleEgreso(detalleDto, movimientoModel);
                            break;

                        case "transferencia":
                            await HandleTransferencia(detalleDto, movimientoModel);
                            break;

                        default:
                            return BadRequest("Tipo de movimiento no soportado");
                    }
                }

                _context.movimientos.Add(movimientoModel);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var movimientoResponseDto = movimientoModel.ToMovimientoDto();
                return Ok(movimientoResponseDto);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        private async Task HandleIngreso(DetalleDeMovimientoDto detalleDto, Movimiento movimientoDto)
        {
            var tipoDeMovimiento = await _tipoDeMovimientoRepo.GetByIdAsync(movimientoDto.TipoDeMovimientoId);
            var producto = await _productoRepo.GetByIdAsync(detalleDto.ProductoId);
            var depositoOrigen = await _depositoRepo.GetByIdAsync(movimientoDto.DepositoOrigenId);
            var productoEnDeposito = depositoOrigen.Productos
                .FirstOrDefault(p => p.Id == detalleDto.ProductoId);

            if (tipoDeMovimiento.Str_descripcion.ToLower() == "ingreso")
            {
                if (productoEnDeposito == null)
                {
                    var nuevoProducto = new CreateProductoCantidadDto
                    {
                        Str_ruta_imagen = producto.Str_ruta_imagen,
                        Str_nombre = producto.Str_nombre,
                        Str_descripcion = producto.Str_descripcion,
                        Int_cantidad_actual = detalleDto.Int_cantidad,
                        Int_cantidad_minima = producto.Int_cantidad_minima,
                        Dec_costo = producto.Dec_costo,
                        Dec_costo_PPP = producto.Dec_costo_PPP,
                        Int_iva = producto.Int_iva,
                        Dec_precio_mayorista = producto.Dec_precio_mayorista,
                        Dec_precio_minorista = producto.Dec_precio_minorista,
                    };
                    var nuevoProductoModel = nuevoProducto.ToProductoCantidadFromCreate(movimientoDto.DepositoOrigenId, producto.ProveedorId, producto.MarcaId);
                    await _productoRepo.CreateAsync(nuevoProductoModel);
                }
                else
                {
                    productoEnDeposito.Int_cantidad_actual += detalleDto.Int_cantidad;
                }
            }
            else if (tipoDeMovimiento.Str_descripcion.ToLower() == "transferencia")
            {
                var productoEnOrigen = await _depositoRepo.GetProductoEnDepositoAsync(movimientoDto.DepositoOrigenId, producto.Str_nombre);
                var depositoDestino = await _depositoRepo.GetByIdAsync(movimientoDto.DepositoDestinoId);
                var productoEnDestino = depositoDestino.Productos
                    .FirstOrDefault(p => p.Str_nombre == productoEnOrigen.Str_nombre);

                if (productoEnOrigen == null)
                {
                    throw new Exception("El producto no existe en el depósito de origen.");
                }

                if (productoEnOrigen.Int_cantidad_actual < detalleDto.Int_cantidad)
                {
                    throw new Exception("No hay suficiente cantidad en el depósito de origen.");
                }

                if (productoEnDestino == null)
                {
                    var nuevoProducto = new CreateProductoCantidadDto
                    {
                        Str_ruta_imagen = producto.Str_ruta_imagen,
                        Str_nombre = producto.Str_nombre,
                        Str_descripcion = producto.Str_descripcion,
                        Int_cantidad_actual = detalleDto.Int_cantidad,
                        Int_cantidad_minima = producto.Int_cantidad_minima,
                        Dec_costo = producto.Dec_costo,
                        Dec_costo_PPP = producto.Dec_costo_PPP,
                        Int_iva = producto.Int_iva,
                        Dec_precio_mayorista = producto.Dec_precio_mayorista,
                        Dec_precio_minorista = producto.Dec_precio_minorista,
                    };
                    var nuevoProductoModel = nuevoProducto.ToProductoCantidadFromCreate(movimientoDto.DepositoDestinoId, productoEnOrigen.ProveedorId, productoEnOrigen.MarcaId);
                    await _productoRepo.CreateAsync(nuevoProductoModel);
                }
                else
                {
                    productoEnDestino.Int_cantidad_actual += detalleDto.Int_cantidad;
                }

                productoEnOrigen.Int_cantidad_actual -= detalleDto.Int_cantidad;
            }

            await _context.SaveChangesAsync();
        }

        private async Task HandleTransferencia(DetalleDeMovimientoDto detalleDto, Movimiento movimientoDto)
        {
            var producto = await _productoRepo.GetByIdAsync(detalleDto.ProductoId);
            var productoEnOrigen = await _depositoRepo.GetProductoEnDepositoAsync(movimientoDto.DepositoOrigenId, producto.Str_nombre);
            var depositoDestino = await _depositoRepo.GetByIdAsync(movimientoDto.DepositoDestinoId);

            var productoEnDestino = depositoDestino.Productos
                .FirstOrDefault(p => p.Str_nombre == productoEnOrigen.Str_nombre);

            if (productoEnOrigen == null)
            {
                throw new Exception("El producto no existe en el depósito de origen.");
            }

            if (productoEnOrigen.Int_cantidad_actual < detalleDto.Int_cantidad)
            {
                throw new Exception("No hay suficiente cantidad en el depósito de origen.");
            }

            if (productoEnDestino == null)
            {
                var nuevoProducto = new CreateProductoCantidadDto
                {
                    Str_ruta_imagen = producto.Str_ruta_imagen,
                    Str_nombre = producto.Str_nombre,
                    Str_descripcion = producto.Str_descripcion,
                    Int_cantidad_actual = detalleDto.Int_cantidad,
                    Int_cantidad_minima = producto.Int_cantidad_minima,
                    Dec_costo = producto.Dec_costo,
                    Dec_costo_PPP = producto.Dec_costo_PPP,
                    Int_iva = producto.Int_iva,
                    Dec_precio_mayorista = producto.Dec_precio_mayorista,
                    Dec_precio_minorista = producto.Dec_precio_minorista,
                };
                var nuevoProductoModel = nuevoProducto.ToProductoCantidadFromCreate(movimientoDto.DepositoDestinoId, productoEnOrigen.ProveedorId, productoEnOrigen.MarcaId);
                await _productoRepo.CreateAsync(nuevoProductoModel);
            }
            else
            {
                productoEnDestino.Int_cantidad_actual += detalleDto.Int_cantidad;
            }

            productoEnOrigen.Int_cantidad_actual -= detalleDto.Int_cantidad;

            await _context.SaveChangesAsync();
        }

        private async Task HandleEgreso(DetalleDeMovimientoDto detalleDto, Movimiento movimientoDto)
        {
            var tipoDeMovimiento = await _tipoDeMovimientoRepo.GetByIdAsync(movimientoDto.TipoDeMovimientoId);
            var producto = await _productoRepo.GetByIdAsync(detalleDto.ProductoId);
            var depositoOrigen = await _depositoRepo.GetByIdAsync(movimientoDto.DepositoOrigenId);

            var productoEnDeposito = depositoOrigen.Productos
                .FirstOrDefault(p => p.Id == detalleDto.ProductoId);


            if (productoEnDeposito == null)
            {
                throw new Exception("El producto no existe en el depósito de origen.");
            }

            if (productoEnDeposito.Int_cantidad_actual < detalleDto.Int_cantidad)
            {
                throw new Exception("No hay suficiente cantidad en el depósito de origen.");
            }

            productoEnDeposito.Int_cantidad_actual -= detalleDto.Int_cantidad;


            await _context.SaveChangesAsync();
        }


        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, Movimiento movimiento)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            /*var movimiento = await _movimientoRepo.UpdateAsync(id, movimientoDto.ToMovimientoFromUpdate());
            if(movimiento == null) return NotFound("El movimiento que desea modificar no existe!!");

            return Ok(movimiento.ToMovimientoDto());*/

            if (!await _movimientoRepo.MovimientoExists(id))
            {
                return BadRequest("El movimiento que desea actualizar no existe!!");
            }

            var movimientoExistente = await _context.movimientos.Include(m => m.DetallesDeMovimientos).FirstOrDefaultAsync(m => m.Id == id);
            movimientoExistente.Date_fecha = DateTime.Now;

            foreach (var detalle in movimiento.DetallesDeMovimientos)
            {
                var detalleExistente = movimientoExistente.DetallesDeMovimientos.FirstOrDefault(d => d.Id == detalle.Id);
                if (detalleExistente != null)
                {
                    detalleExistente.Int_cantidad = detalle.Int_cantidad;
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _movimientoRepo.MovimientoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var movimientoModel = await _movimientoRepo.DeleteAsync(id);
            if (movimientoModel == null)
            {
                return NotFound("El movimiento que desea eliminar no existe!!");
            }

            return Ok(movimientoModel);
        }
    }
}