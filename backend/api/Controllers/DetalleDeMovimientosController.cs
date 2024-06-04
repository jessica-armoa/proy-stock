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
        private readonly ITipoDeMovimientoRepository _tipoDeMovimientoRepo;
        public DetalleDeMovimientosController(IDetalleDeMovimientosRepository detalleRepo, IProductoRepository productoRepo, IMovimientoRepository movimientoRepo, IDepositoRepository depositoRepo, ApplicationDbContext context, ITipoDeMovimientoRepository tipoDeMovimientoRepo)
        {
            _detalleRepo = detalleRepo;
            _productoRepo = productoRepo;
            _movimientoRepo = movimientoRepo;
            _depositoRepo = depositoRepo;
            _context = context;
            _tipoDeMovimientoRepo = tipoDeMovimientoRepo;
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
            var tipo_de_movimiento = await _tipoDeMovimientoRepo.GetByIdAsync(movimiento.TipoDeMovimientoId);
            //var motivo = await _motivoRepo.GetByIdAsync(tipo_de_movimiento.MotivoId);
            var producto = await _productoRepo.GetByIdAsync(productoId);
            var productoEnOrigen = await _depositoRepo.GetProductoEnDepositoAsync(movimiento.DepositoOrigenId, producto.Str_nombre);
            var productoEnDestino = await _depositoRepo.GetProductoEnDepositoAsync(movimiento.DepositoDestinoId, producto.Str_nombre);

            ///////////////////////////////////

            if (tipo_de_movimiento.Str_descripcion.ToLower() == "ingreso")
            {
                if (productoEnOrigen == null || !await _productoRepo.ProductoExistsName(producto.Str_nombre))
                {
                    // Si el producto no existe en el depósito destino, crearlo
                    var nuevoProductoOrigenModel = new CreateProductoCantidadDto
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
                        Dec_precio_minorista = producto.Dec_precio_minorista
                    };

                    var nuevoProducto = nuevoProductoOrigenModel.ToProductoCantidadFromCreate(producto.DepositoId, producto.ProveedorId, producto.MarcaId);
                    await _productoRepo.CreateAsync(nuevoProducto);
                }
                else
                {
                    // Si el producto ya existe en el depósito destino, actualizar la cantidad
                    producto.Int_cantidad_actual += detalleDto.Int_cantidad;
                    await _productoRepo.UpdateAsync(producto.Id, producto);
                }
            }

            if (movimiento.TipoDeMovimiento.Str_descripcion.ToLower() == "egreso")
            {
                if (productoEnOrigen == null || productoEnOrigen.Int_cantidad_actual < detalleDto.Int_cantidad)
                {
                    return BadRequest("No hay suficiente cantidad del producto en el depósito origen!!");
                }
            }

            if (movimiento.TipoDeMovimiento.Str_descripcion.ToLower() == "transferencia")
            {
                if(movimiento.DepositoOrigenId == movimiento.DepositoDestinoId)
                {
                    return BadRequest("No puedes hacer una transferencia en el mismo deposito");
                }     
                if (productoEnOrigen == null || productoEnOrigen.Int_cantidad_actual < detalleDto.Int_cantidad)
                {
                    return BadRequest("No hay suficiente cantidad del producto en el depósito origen!!");
                }
                // Realizar la transferencia
                productoEnOrigen.Int_cantidad_actual -= detalleDto.Int_cantidad;

                if (productoEnDestino == null || productoEnOrigen.Str_nombre != productoEnDestino.Str_nombre)
                {
                    // Si el producto no existe en el depósito destino, crearlo
                    var productoEnDestino2 = new CreateProductoCantidadDto
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

                    var nuevo_producto = productoEnDestino2.ToProductoCantidadFromCreate(movimiento.DepositoDestinoId, productoEnOrigen.ProveedorId, productoEnOrigen.MarcaId);
                    await _productoRepo.CreateAsync(nuevo_producto);
                }
                else
                {
                    // Si el producto ya existe en el depósito destino, actualizar la cantidad
                    productoEnDestino.Int_cantidad_actual += detalleDto.Int_cantidad;
                }
            }

            var detalleModel = detalleDto.ToDetalleFromCreate(movimientoId, productoId);
            await _detalleRepo.CreateAsync(detalleModel);
            return CreatedAtAction(nameof(GetById), new { id = detalleModel.Id }, detalleModel.ToDetalleDeMovimientoDto());
        }
        ////////////////////////////////////////////////////


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