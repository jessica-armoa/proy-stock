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
        private readonly IDetalleDeMovimientosRepository _detalleRepo;
        private readonly INotaDeRemisionRepository _notaDeRemisionRepo;
        private readonly ITimbradoRepository _timbradoRepo;
        public MovimientoController(
            IMovimientoRepository movimientoRepo,
            IDepositoRepository depositoRepo,
            ITipoDeMovimientoRepository tipoDeMovimientoRepo,
            ApplicationDbContext context,
            IProductoRepository productoRepo,
            IDetalleDeMovimientosRepository detalleRepo,
            INotaDeRemisionRepository notaDeRemisionRepo,
            ITimbradoRepository timbradoRepo)
        {
            _movimientoRepo = movimientoRepo;
            _depositoRepo = depositoRepo;
            _tipoDeMovimientoRepo = tipoDeMovimientoRepo;
            _context = context;
            _productoRepo = productoRepo;
            _detalleRepo = detalleRepo;
            _notaDeRemisionRepo = notaDeRemisionRepo;
            _timbradoRepo = timbradoRepo;
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
                DetallesDeMovimientos = m.DetallesDeMovimientos.Select(d => new DetalleDeMovimientoDto
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
        public async Task<IActionResult> CrearMovimiento([FromBody] MovimientoDto movimiento)
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
                            .FirstOrDefaultAsync(p => p.Id == detalle.ProductoId && p.DepositoId == movimiento.DepositoOrigenId);

                        if (producto == null && tipoDeMovimiento.Str_descripcion.ToLower() != "ingreso")
                        {
                            return BadRequest("Producto no encontrado en el depósito origen.");
                        }

                        if (tipoDeMovimiento.Str_descripcion.ToLower() == "ingreso")
                        {
                            producto.Int_cantidad_actual += detalle.Int_cantidad;
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
                            producto.Int_cantidad_actual -= detalle.Int_cantidad;
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
                                    Str_nombre = producto.Str_nombre,
                                    Int_cantidad_actual = detalle.Int_cantidad
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

                    // Obtener el timbrado activo
                    var timbradoActivo = await _timbradoRepo.GetTimbradoActivoAsync();
                    if (timbradoActivo == null)
                    {
                        return BadRequest("No hay un timbrado activo disponible.");
                    }

                    //Obtener ultima nota de remision creada
                    var ultimaNotaDeRemision = await _notaDeRemisionRepo.GetUltimaNotaDeRemisionAsync();
                    int nuevoNumeroDeComprobanteActual = 1;

                    if (ultimaNotaDeRemision != null && ultimaNotaDeRemision.Str_timbrado == timbradoActivo.Str_timbrado)
                    {
                        nuevoNumeroDeComprobanteActual = int.Parse(ultimaNotaDeRemision.Str_numero_de_comprobante_actual) + 1;
                        if (nuevoNumeroDeComprobanteActual > 1000)
                        {
                            return BadRequest("El número de comprobante ha excedido el límite de 1000.");
                        }
                    }

                    var nuevaNotaDeRemision = new NotaDeRemision
                    {
                        Str_numero = $"001-001-{nuevoNumeroDeComprobanteActual.ToString("D4")}",
                        Str_timbrado = timbradoActivo.Str_timbrado,
                        Str_numero_de_comprobante_inicial = "0001",
                        Str_numero_de_comprobante_final = "1000",
                        Str_numero_de_comprobante_actual = nuevoNumeroDeComprobanteActual.ToString("D4"),
                        Date_fecha_de_expedicion = DateTime.Now,
                        Date_fecha_de_vencimiento = DateTime.Now.AddMonths(1),
                        MovimientoId = movimientoModel.Id,
                        /*
                        // Datos de Empresa
                        EmpresaNombre = movimiento.empresa_nombre,
                        EmpresaDireccion = movimiento.empresa_direccion,
                        EmpresaTelefono = movimiento.empresa_telefono,
                        EmpresaSucursal = movimiento.empresa_sucursal,
                        EmpresaActividad = movimiento.empresa_actividad,

                        // Otros datos
                        Ruc = movimiento.ruc,
                        DestinatarioNombre = movimiento.destinatario_nombre,
                        DestinatarioDocumento = movimiento.destinatario_documento,
                        PuntoPartida = movimiento.punto_partida,
                        PuntoLlegada = movimiento.punto_llegada,
                        TrasladoFechaInicio = movimiento.traslado_fecha_inicio,
                        TrasladoFechaFin = movimiento.traslado_fecha_fin,
                        TrasladoVehiculo = movimiento.traslado_vehiculo,
                        TrasladoRuta = movimiento.traslado_ruta,
                        TransportistaNombre = movimiento.traslado_transportista_nombre,
                        TransportistaRuc = movimiento.traslado_transportista_ruc,
                        ConductorNombre = movimiento.traslado_conductor_nombre,
                        ConductorDocumento = movimiento.traslado_conductor_documento,
                        ConductorDireccion = movimiento.traslado_conductor_direccion,
                        Motivo = movimiento.traslado_motivo,
                        MotivoDescripcion = movimiento.traslado_motivo_descripcion,
                        ComprobanteVenta = movimiento.traslado_comprobante_venta
                        */
                    };

                    await _notaDeRemisionRepo.CreateAsync(nuevaNotaDeRemision);

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
    }
}