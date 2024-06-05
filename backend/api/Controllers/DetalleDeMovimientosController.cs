using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.DetalleDeMovimiento;
using api.Dtos.Producto;
using api.Interfaces;
using api.Mapper;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/detalle")]
    [ApiController]
    public class DetalleDeMovimientosController : ControllerBase
    {
        private readonly IDetalleDeMovimientosRepository _detalleRepo;
        private readonly IProductoRepository _productoRepo;
        private readonly IMovimientoRepository _movimientoRepo;
        private readonly IDepositoRepository _depositoRepo;
        private readonly ApplicationDbContext _context;
        public DetalleDeMovimientosController(IDetalleDeMovimientosRepository detalleRepo, IProductoRepository productoRepo, IMovimientoRepository movimientoRepo, IDepositoRepository depositoRepo, ApplicationDbContext context)
        {
            _detalleRepo = detalleRepo;
            _productoRepo = productoRepo;
            _movimientoRepo = movimientoRepo;
            _depositoRepo = depositoRepo;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var detalles = await _detalleRepo.GetAllAsync();
            var detalleDto = detalles.Select(f => f.ToDetalleDeMovimientoDto());
            return Ok(detalleDto);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var detalle = await _detalleRepo.GetByIdAsync(id);
            if (detalle == null) return NotFound();

            return Ok(detalle);
        }

        [HttpPost]
        [Route("{movimientoId:int}/{productoId}")]
        public async Task<IActionResult> Post([FromBody] CreateDetalleRequestDto detalleDto, [FromRoute] int movimientoId, [FromRoute] int productoId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _movimientoRepo.MovimientoExists(movimientoId))
            {
                return BadRequest("El movimiento ingresado no existe!!");
            }

            if (!await _productoRepo.ProductoExists(productoId))
            {
                return BadRequest("El producto ingresado no existe!!");
            }

            var movimiento = await _movimientoRepo.GetByIdAsync(movimientoId);
            var producto = await _productoRepo.GetByIdAsync(productoId);
            var productoEnOrigen = await _depositoRepo.GetProductoEnDepositoAsync(movimiento.DepositoOrigenId, producto.Str_nombre);
            var productoEnDestino = await _depositoRepo.GetProductoEnDepositoAsync(movimiento.DepositoDestinoId, producto.Str_nombre);


            if (productoEnOrigen == null || productoEnOrigen.Int_cantidad_actual < detalleDto.Int_cantidad)
            {
                return BadRequest("No hay suficiente cantidad del producto en el depósito origen!!");
            }

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Realizar la transferencia
                    productoEnOrigen.Int_cantidad_actual -= detalleDto.Int_cantidad;

                    if (productoEnDestino == null || productoEnOrigen.Str_nombre != productoEnDestino.Str_nombre)
                    {
                        // Si el producto no existe en el depósito destino, crearlo
                        var productoEnDestino2 = new CreateProductoRequestDto
                        {
                            Str_ruta_imagen = productoEnOrigen.Str_ruta_imagen,
                            Str_nombre = productoEnOrigen.Str_nombre,
                            Str_descripcion = productoEnOrigen.Str_descripcion,
                            Int_cantidad_actual = detalleDto.Int_cantidad,
                            Int_cantidad_minima = productoEnOrigen.Int_cantidad_minima,
                            Dec_costo = productoEnOrigen.Dec_costo,
                            Dec_costo_PPP = productoEnOrigen.Dec_costo_PPP,
                            Int_iva = productoEnOrigen.Int_iva,
                            Dec_precio_mayorista = productoEnOrigen.Dec_precio_mayorista,
                            Dec_precio_minorista = productoEnOrigen.Dec_precio_minorista
                        };

                        var nuevo_producto = productoEnDestino2.ToProductoFromCreate(movimiento.DepositoDestinoId, productoEnOrigen.ProveedorId, productoEnOrigen.MarcaId);
                        await _productoRepo.CreateAsync(nuevo_producto);
                    }
                    else
                    {
                        // Si el producto ya existe en el depósito destino, actualizar la cantidad
                        productoEnDestino.Int_cantidad_actual += detalleDto.Int_cantidad;
                    }


                    var detalleModel = detalleDto.ToDetalleFromCreate(movimientoId, productoId);

                    await _detalleRepo.CreateAsync(detalleModel);
                    await transaction.CommitAsync();
                    return CreatedAtAction(nameof(GetById), new { id = detalleModel.Id }, detalleModel.ToDetalleDeMovimientoDto());
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return StatusCode(500, "Ocurrió un error al realizar la transferencia: " + ex.Message);
                }
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateDetalleRequestDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var detalleModel = await _detalleRepo.UpdateAsync(id, updateDto.ToDetalleFromUpdate());
            if (detalleModel == null) return NotFound();

            return Ok(detalleModel.ToDetalleDeMovimientoDto());
        }
    }
}