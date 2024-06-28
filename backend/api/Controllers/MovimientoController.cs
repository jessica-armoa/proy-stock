using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.DetalleDeMovimiento;
using api.Dtos.Movimiento;
using api.Dtos.Producto;
using api.Dtos.Deposito;
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
        private readonly ITipoDeMovimientoRepository _tipoDeMovimientoRepo;
        private readonly IProductoRepository _productoRepo;
        private readonly IDetalleDeMovimientosRepository _detalleRepo;
        private readonly INotaDeRemisionRepository _notaDeRemisionRepo;
        private readonly ITimbradoRepository _timbradoRepo;
        private readonly IMotivoPorTipoDeMovimientoRepository _motivoPorTipoMovimientoRepo;
        private readonly IMotivoRepository _motivoRepo;
        private readonly IDepositoRepository _depositoRepo;
        private readonly ApplicationDbContext _context;
        private readonly IAsientoRepository _asientoRepo;
        public MovimientoController(IMovimientoRepository movimientoRepo
            , IDepositoRepository depositoRepo
            , ITipoDeMovimientoRepository tipoDeMovimientoRepo
            , IProductoRepository productoRepo
            , IDetalleDeMovimientosRepository detalleRepo
            , IMotivoPorTipoDeMovimientoRepository motivoPorTipoMovimientoRepo
            , IMotivoRepository motivoRepo
            , ApplicationDbContext context
            , IAsientoRepository asientoRepo
            , INotaDeRemisionRepository notaDeRemisionRepo
            , ITimbradoRepository timbradoRepo)
        {
            _movimientoRepo = movimientoRepo;
            _tipoDeMovimientoRepo = tipoDeMovimientoRepo;
            _productoRepo = productoRepo;
            _detalleRepo = detalleRepo;
            _motivoPorTipoMovimientoRepo = motivoPorTipoMovimientoRepo;
            _motivoRepo = motivoRepo;
            _depositoRepo = depositoRepo;
            _context = context;
            _asientoRepo = asientoRepo;
            _notaDeRemisionRepo = notaDeRemisionRepo;
            _timbradoRepo = timbradoRepo;
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
        [Route("{motivoPorTipoMovimientoId:int}/{depositoOrigenId:int}/{depositoDestinoId:int}")]
        public async Task<IActionResult> Create([FromBody] CreateMovimientoRequestDto movimientoDto, [FromRoute] int motivoPorTipoMovimientoId, [FromRoute] int depositoOrigenId, [FromRoute] int depositoDestinoId)
        {
            if (movimientoDto == null || movimientoDto.DetallesDeMovimientos == null)
            {
                return BadRequest("Movimiento o detalles no válidos!!");
            }

            if (!await _motivoPorTipoMovimientoRepo.MotivoPorTipoMovimientoExists(motivoPorTipoMovimientoId))
            {
                return BadRequest("El motivo por tipo de movimiento no existe!!");
            }

            var motivoPorTipoMovimiento = await _motivoPorTipoMovimientoRepo.GetByIdAsync(motivoPorTipoMovimientoId);
            var tipoDeMovimiento = await _tipoDeMovimientoRepo.GetByIdAsync(motivoPorTipoMovimiento.TipodemovimientoId);
            var motivo = await _motivoRepo.GetByIdAsync(motivoPorTipoMovimiento.MotivoId);
            if (tipoDeMovimiento == null || motivo == null)
            {
                return BadRequest("motivo y tipo de movimiento nulo");
            }

            if (!await _depositoRepo.DepositoExists(depositoOrigenId))
            {
                return BadRequest("El deposito origen no existe!!");
            }

            if (!await _depositoRepo.DepositoExists(depositoDestinoId))
            {
                return BadRequest("El deposito destino no existe!!");
            }

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var movimientoModel = movimientoDto.ToMovimientoFromCreate(motivoPorTipoMovimientoId, depositoOrigenId, depositoDestinoId);
                    await _movimientoRepo.CreateAsync(movimientoModel);

                    foreach (var detalleDto in movimientoDto.DetallesDeMovimientos)
                    {
                        var detalle = detalleDto.ToDetalleFromCreate(movimientoModel.Id, detalleDto.ProductoId);

                        var producto = await _productoRepo.GetByIdAsync(detalle.ProductoId);

                        if (motivo == null) return null;
                        if (producto == null)
                        {
                            return BadRequest();
                        }
                        /*
                            BOOL OPERACION TRUE = ENTRADA
                            BOOL OPERACION FALSE = SALIDA

                            INGRESO DEPOSITO DESTINO
                            EGRESO DEPOSITO ORIGEN
                        */
                        if (tipoDeMovimiento.Str_tipo.ToLower() == "ingreso")
                        {
                            decimal total = detalle.Int_cantidad * detalle.Dec_costo;
                            decimal iva = (total * producto.Int_iva) / 100;

                            await _detalleRepo.CreateAsync(detalle);

                            var asiento1 = new Asiento
                            {
                                movimientoId = movimientoModel.Id,
                                Str_cuenta = "1.1.1.1",
                                Str_concepto = "Compra",
                                Dec_debe = total - iva,
                                Dec_haber = 0,
                                Bool_borrado = false
                            };

                            var asiento2 = new Asiento
                            {
                                movimientoId = movimientoModel.Id,
                                Str_cuenta = "1.1.1.1.1",
                                Str_concepto = $" I.V.A {producto.Int_iva}%",
                                Dec_debe = iva,
                                Dec_haber = 0,
                                Bool_borrado = false
                            };

                            var asiento3 = new Asiento
                            {
                                movimientoId = movimientoModel.Id,
                                Str_cuenta = "1.1.1.1.2",
                                Str_concepto = "Cuenta por pagar",
                                Dec_debe = 0,
                                Dec_haber = total,
                                Bool_borrado = false
                            };

                            await _asientoRepo.CreateAsync(asiento1);
                            await _asientoRepo.CreateAsync(asiento2);
                            await _asientoRepo.CreateAsync(asiento3);
                        }

                        else if (tipoDeMovimiento.Str_tipo.ToLower() == "egreso" && motivo.Bool_perdida == false)
                        {
                            if (producto.Int_cantidad_actual < detalle.Int_cantidad)
                            {
                                return BadRequest("Cantidad insuficiente del producto!!");
                            }

                            var detalleCreado = await _detalleRepo.CreateAsync(detalle);
                            decimal total = detalleCreado.Int_cantidad * detalleCreado.Dec_costo;
                            decimal iva = (total * producto.Int_iva) / 100;

                            var asiento1 = new Asiento
                            {
                                movimientoId = movimientoModel.Id,
                                Str_cuenta = "1.1.1.1.3",
                                Str_concepto = "Cuentas por cobrar",
                                Dec_debe = total,
                                Dec_haber = 0,
                                Bool_borrado = false
                            };

                            var asiento2 = new Asiento
                            {
                                movimientoId = movimientoModel.Id,
                                Str_cuenta = "1.1.1.1.4",
                                Str_concepto = "Ventas",
                                Dec_debe = 0,
                                Dec_haber = total - iva,
                                Bool_borrado = false
                            };

                            var asiento3 = new Asiento
                            {
                                movimientoId = movimientoModel.Id,
                                Str_cuenta = "1.1.1.1.5",
                                Str_concepto = $" I.V.A {producto.Int_iva}%",
                                Dec_debe = 0,
                                Dec_haber = iva,
                                Bool_borrado = false
                            };

                            await _asientoRepo.CreateAsync(asiento1);
                            await _asientoRepo.CreateAsync(asiento2);
                            await _asientoRepo.CreateAsync(asiento3);
                        }

                        else if(tipoDeMovimiento.Str_tipo.ToLower() == "egreso" && motivo.Bool_perdida == true)
                        {
                            if (producto.Int_cantidad_actual < detalle.Int_cantidad)
                            {
                                return BadRequest("Cantidad insuficiente del producto!!");
                            }
                            
                            await _detalleRepo.CreateAsync(detalle);
                        }

                        else if (tipoDeMovimiento.Str_tipo.ToLower() == "transferencia")
                        {
                            if (producto.Int_cantidad_actual < detalle.Int_cantidad)
                            {
                                return BadRequest("Cantidad insuficiente del producto!!");
                            }

                            var productoEnOrigen = await _productoRepo.ObtenerProductoEnDeposito(producto.Str_nombre, movimientoModel.DepositoOrigenId);
                            var productoEnDestino = await _productoRepo.ObtenerProductoEnDeposito(producto.Str_nombre, movimientoModel.DepositoDestinoId);

                            if (tipoDeMovimiento.Bool_operacion == true)
                            {
                                if (productoEnDestino == null)
                                {
                                    return BadRequest("El producto que desea transferir no existe en el deposito destino!!");
                                }

                                if (productoEnOrigen == null)
                                {
                                    var nuevoProducto = new CreateProductoCantidadDto
                                    {
                                        Str_ruta_imagen = producto.Str_ruta_imagen,
                                        Str_nombre = producto.Str_nombre,
                                        Str_descripcion = producto.Str_descripcion,
                                        Int_cantidad_actual = detalle.Int_cantidad,
                                        Int_cantidad_minima = producto.Int_cantidad_minima,
                                        //PUEDE CAMBIAR POR EL COSTO PPP
                                        Dec_costo = 0,
                                        Dec_costo_PPP = producto.Dec_costo_PPP,
                                        Int_iva = producto.Int_iva,
                                        Dec_precio_mayorista = producto.Dec_precio_mayorista,
                                        Dec_precio_minorista = producto.Dec_precio_minorista,
                                    };

                                    detalle.Dec_costo = producto.Dec_costo_PPP;
                                    producto.Int_cantidad_actual -= detalle.Int_cantidad;

                                    var productoModel = nuevoProducto.ToProductoCantidadFromCreate(movimientoModel.DepositoOrigenId, producto.ProveedorId, producto.MarcaId);
                                    await _productoRepo.CreateAsync(productoModel);
                                    await _detalleRepo.CreateAsync(detalle);
                                }
                                else
                                {
                                    detalle.Dec_costo = producto.Dec_costo_PPP;
                                    producto.Int_cantidad_actual -= detalle.Int_cantidad;

                                    productoEnOrigen.Int_cantidad_actual += detalle.Int_cantidad;
                                    productoEnOrigen.Dec_costo = ((productoEnOrigen.Int_cantidad_actual * productoEnOrigen.Dec_costo_PPP) + (detalle.Int_cantidad * detalle.Dec_costo)) / (productoEnOrigen.Int_cantidad_actual + detalle.Int_cantidad);
                                    await _detalleRepo.CreateAsync(detalle);
                                }
                            }

                            else
                            {
                                if (productoEnOrigen == null)
                                {
                                    return BadRequest("El producto que desea transferir no existe en el deposito origen!!");
                                }

                                if (productoEnDestino == null)
                                {
                                    var nuevoProducto = new CreateProductoCantidadDto
                                    {
                                        Str_ruta_imagen = producto.Str_ruta_imagen,
                                        Str_nombre = producto.Str_nombre,
                                        Str_descripcion = producto.Str_descripcion,
                                        Int_cantidad_actual = detalle.Int_cantidad,
                                        Int_cantidad_minima = producto.Int_cantidad_minima,
                                        //PUEDE CAMBIAR POR EL COSTO PPP
                                        Dec_costo = 0,
                                        Dec_costo_PPP = producto.Dec_costo_PPP,
                                        Int_iva = producto.Int_iva,
                                        Dec_precio_mayorista = producto.Dec_precio_mayorista,
                                        Dec_precio_minorista = producto.Dec_precio_minorista,
                                    };

                                    producto.Int_cantidad_actual -= detalle.Int_cantidad;

                                    var productoModel = nuevoProducto.ToProductoCantidadFromCreate(movimientoModel.DepositoDestinoId, producto.ProveedorId, producto.MarcaId);
                                    await _productoRepo.CreateAsync(productoModel);

                                    await _detalleRepo.CreateAsync(detalle);
                                }
                                else
                                {
                                    detalle.Dec_costo = producto.Dec_costo_PPP;
                                    producto.Int_cantidad_actual -= detalle.Int_cantidad;
                                    productoEnDestino.Dec_costo_PPP = ((productoEnDestino.Int_cantidad_actual * productoEnDestino.Dec_costo_PPP) + (detalle.Int_cantidad * detalle.Dec_costo)) / (productoEnDestino.Int_cantidad_actual + detalle.Int_cantidad);
                                    productoEnDestino.Int_cantidad_actual += detalle.Int_cantidad;

                                    await _detalleRepo.CreateAsync(detalle);
                                }
                            }
                        }

                        else
                        {
                            return BadRequest();
                        }
                    }
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return Ok(movimientoModel.ToMovimientoDto());
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    // Log the exception details
                    Console.WriteLine($"Error: {ex.Message}");
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine($"\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\nInner Exception: {ex.InnerException.Message}\n\n\n\n\n\n\n\n\n\n\n\n\n");
                    }
                    return StatusCode(500, $"Error interno del servidor: {ex.Message}");
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