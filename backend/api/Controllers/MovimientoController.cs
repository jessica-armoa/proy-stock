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
        public MovimientoController(IMovimientoRepository movimientoRepo, IDepositoRepository depositoRepo, ITipoDeMovimientoRepository tipoDeMovimientoRepo, ApplicationDbContext context, IProductoRepository productoRepo, IDetalleDeMovimientosRepository detalleRepo)
        {
            _movimientoRepo = movimientoRepo;
            _depositoRepo = depositoRepo;
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

        /*[HttpPost]
        public async Task<IActionResult> CreateMovimiento([FromBody] CreateMovimientoRequestDto movimientoDto)
        {
            var tipoDeMovimiento = await _tipoDeMovimientoRepo.GetByIdAsync(movimientoDto.TipoDeMovimientoId);
            if (tipoDeMovimiento == null)
            {
                return BadRequest("Tipo de movimiento no válido");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var nuevoMovimiento = movimientoDto.ToMovimientoFromCreate(movimientoDto.TipoDeMovimientoId, movimientoDto.DepositoOrigenId, movimientoDto.DepositoDestinoId);
                await _movimientoRepo.CreateAsync(nuevoMovimiento);
                foreach (var detalleDto in movimientoDto.DetallesDeMovimientos)
                {
                    var producto = await _productoRepo.GetByIdAsync(detalleDto.ProductoId);
                    if (producto == null)
                    {
                        return BadRequest($"Producto con ID {detalleDto.ProductoId} no encontrado");
                    }

                    var detalleModel = detalleDto.ToDetalleFromCreate(nuevoMovimiento.Id, producto.Id);
                    await _detalleRepo.CreateAsync(detalleModel);
                    //nuevoMovimiento.DetallesDeMovimientos.Add(detalleModel);

                    switch (tipoDeMovimiento.Str_descripcion.ToLower())
                    {
                        case "ingreso":
                            await HandleIngreso(detalleDto, movimientoDto);
                            break;

                        case "egreso":
                            await HandleEgreso(detalleDto, movimientoDto);
                            break;

                        case "transferencia":
                            await HandleTransferencia(detalleDto, movimientoDto);
                            break;

                        default:
                            return BadRequest("Tipo de movimiento no soportado");
                    }
                }

                await transaction.CommitAsync();

                var movimientoResponseDto = new MovimientoDto
                {
                    Id = nuevoMovimiento.Id,
                    TipoDeMovimientoId = nuevoMovimiento.TipoDeMovimientoId,
                    Str_tipoDeMovimiento = tipoDeMovimiento.Str_descripcion,
                    DepositoOrigenId = nuevoMovimiento.DepositoOrigenId,
                    DepositoDestinoId = nuevoMovimiento.DepositoDestinoId,
                    DetallesDeMovimientos = nuevoMovimiento.DetallesDeMovimientos.Select(d => new DetalleDeMovimientoDto
                    {
                        Id = d.Id,
                        Int_cantidad = d.Int_cantidad,
                        MovimientoId = d.MovimientoId,
                        ProductoId = d.ProductoId
                    }).ToList()
                };

                return Ok(movimientoResponseDto);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        private async Task HandleIngreso(CreateDetalleRequestDto detalleDto, CreateMovimientoRequestDto movimientoDto)
        {
            var producto = await _productoRepo.GetByIdAsync(detalleDto.ProductoId);
            var depositoOrigen = await _depositoRepo.GetByIdAsync(movimientoDto.DepositoOrigenId);
            var productoEnDeposito = depositoOrigen.Productos.FirstOrDefault(p => p.Id == detalleDto.ProductoId);

            if (productoEnDeposito == null || productoEnDeposito.Str_nombre != producto.Str_nombre)
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
                await _context.SaveChangesAsync();
            }

        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, UpdateMovimientoRequestDto movimientoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var movimientoModel = await _movimientoRepo.UpdateAsync(id, movimientoDto.ToMovimientoFromUpdate());
            if(movimientoModel == null)
            {
                return NotFound();
            }

            return Ok(movimientoModel);
    }

        private async Task HandleTransferencia(CreateDetalleRequestDto detalleDto, CreateMovimientoRequestDto movimientoDto)
        {
            var producto = await _productoRepo.GetByIdAsync(detalleDto.ProductoId);
            var depositoOrigen = await _depositoRepo.GetByIdAsync(movimientoDto.DepositoOrigenId);
            var depositoDestino = await _depositoRepo.GetByIdAsync(movimientoDto.DepositoDestinoId);
            if (depositoOrigen.Id == depositoDestino.Id)
            {
                throw new Exception("Los depositos no pueden ser iguales en una transferencia.");
            }

            var productoEnOrigen = depositoOrigen.Productos.FirstOrDefault(p => p.Id == detalleDto.ProductoId);
            var productoEnDestino = depositoDestino.Productos.FirstOrDefault(p => p.Id == detalleDto.ProductoId);

            if (productoEnOrigen == null || productoEnOrigen.Int_cantidad_actual < detalleDto.Int_cantidad)
            {
                throw new Exception("No hay suficiente cantidad en el depósito de origen.");
            }

            productoEnOrigen.Int_cantidad_actual -= detalleDto.Int_cantidad;

            if (productoEnDestino == null || productoEnOrigen.Str_nombre != productoEnDestino.Str_nombre)
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

            await _context.SaveChangesAsync();
        }

        private async Task HandleEgreso(CreateDetalleRequestDto detalleDto, CreateMovimientoRequestDto movimientoDto)
        {
            var producto = await _productoRepo.GetByIdAsync(detalleDto.ProductoId);
            var depositoOrigen = await _depositoRepo.GetByIdAsync(movimientoDto.DepositoOrigenId);

            var productoEnDeposito = depositoOrigen.Productos.FirstOrDefault(p => p.Id == detalleDto.ProductoId);

            if (productoEnDeposito == null || productoEnDeposito.Int_cantidad_actual < detalleDto.Int_cantidad)
            {
                throw new Exception("No hay suficiente cantidad en el depósito de origen.");
            }

            productoEnDeposito.Int_cantidad_actual -= detalleDto.Int_cantidad;

            await _context.SaveChangesAsync();
        }

    }
*/
        /*[HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, Movimiento movimiento)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            /*var movimiento = await _movimientoRepo.UpdateAsync(id, movimientoDto.ToMovimientoFromUpdate());
            if(movimiento == null) return NotFound("El movimiento que desea modificar no existe!!");

            return Ok(movimiento.ToMovimientoDto());*/
        /*
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
    }*/
        /*[HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateMovimiento([FromRoute] int id, [FromBody] MovimientoDto movimientoDto)
        {
            var movimientoExistente = await _context.movimientos
                .Include(m => m.DetallesDeMovimientos)
                .ThenInclude(d => d.Producto)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movimientoExistente == null)
            {
                return NotFound("Movimiento no encontrado");
            }

            var tipoDeMovimiento = await _context.tipos_de_movimientos.FindAsync(movimientoDto.TipoDeMovimientoId);
            if (tipoDeMovimiento == null)
            {
                return BadRequest("Tipo de movimiento no válido");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Revertir los cambios de los detalles actuales
                foreach (var detalleExistente in movimientoExistente.DetallesDeMovimientos)
                {
                    var detalleDto = detalleExistente.ToDetalleDeMovimientoDto();
                    switch (tipoDeMovimiento.Str_descripcion.ToLower())
                    {
                        case "ingreso":
                            await RevertirIngreso(detalleDto, movimientoExistente.DepositoOrigenId);
                            break;

                        case "egreso":
                            await RevertirEgreso(detalleDto, movimientoExistente.DepositoOrigenId);
                            break;

                        case "transferencia":
                            await RevertirTransferencia(detalleDto, movimientoExistente.DepositoOrigenId, movimientoExistente.DepositoDestinoId);
                            break;
                    }
                }

                // Actualizar detalles con los nuevos datos
                movimientoExistente.Date_fecha = movimientoDto.Date_fecha;

                var depositoOrigen = await _context.depositos
                    .Include(d => d.Productos)
                    .FirstOrDefaultAsync(d => d.Id == movimientoExistente.DepositoOrigenId);

                var depositoDestino = await _context.depositos
                    .Include(d => d.Productos)
                    .FirstOrDefaultAsync(d => d.Id == movimientoExistente.DepositoDestinoId);

                foreach (var detalleDto in movimientoDto.DetallesDeMovimientos)
                {
                    var nuevoDetalle = new UpdateDetalleRequestDto
                    {
                        Int_cantidad = detalleDto.Int_cantidad
                    };
                    var nuevoDetalleModel = nuevoDetalle.ToDetalleFromUpdate();
                    movimientoExistente.DetallesDeMovimientos.Add(nuevoDetalleModel);
                    var movimiento = await _movimientoRepo.GetByIdAsync(id);
                    if (movimiento == null)
                    {
                        return NotFound("Movimiento no existe!!");
                    }
                    switch (tipoDeMovimiento.Str_descripcion.ToLower())
                    {
                        case "ingreso":
                            await HandleIngreso(nuevoDetalleModel.ToDetalleDeMovimientoDto(), movimiento);
                            break;

                        case "egreso":
                            await HandleEgreso(nuevoDetalleModel.ToDetalleDeMovimientoDto(), movimiento);
                            break;

                        case "transferencia":
                            await HandleTransferencia(nuevoDetalleModel.ToDetalleDeMovimientoDto(), movimiento);
                            break;
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var movimientoResponseDto = movimientoExistente.ToMovimientoDto();
                return Ok(movimientoResponseDto);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        // Métodos para revertir los cambios de ingreso, egreso y transferencia
        private async Task RevertirIngreso(DetalleDeMovimientoDto detalleDto, int? depositoId)
        {
            var deposito = await _context.depositos.Include(d => d.Productos).FirstOrDefaultAsync(d => d.Id == depositoId);
            if (deposito == null) throw new Exception("Depósito no encontrado");

            var productoEnDeposito = deposito.Productos.FirstOrDefault(p => p.Id == detalleDto.ProductoId);
            if (productoEnDeposito != null)
            {
                productoEnDeposito.Int_cantidad_actual -= detalleDto.Int_cantidad;
                if (productoEnDeposito.Int_cantidad_actual < 0) productoEnDeposito.Int_cantidad_actual = 0;
            }

            await _context.SaveChangesAsync();
        }

        private async Task RevertirEgreso(DetalleDeMovimientoDto detalleDto, int? depositoId)
        {
            var deposito = await _context.depositos.Include(d => d.Productos).FirstOrDefaultAsync(d => d.Id == depositoId);
            if (deposito == null) throw new Exception("Depósito no encontrado");

            var productoEnDeposito = deposito.Productos.FirstOrDefault(p => p.Id == detalleDto.ProductoId);
            if (productoEnDeposito != null)
            {
                productoEnDeposito.Int_cantidad_actual += detalleDto.Int_cantidad;
            }

            await _context.SaveChangesAsync();
        }

        private async Task RevertirTransferencia(DetalleDeMovimientoDto detalleDto, int? depositoOrigenId, int? depositoDestinoId)
        {
            var depositoOrigen = await _context.depositos.Include(d => d.Productos).FirstOrDefaultAsync(d => d.Id == depositoOrigenId);
            var depositoDestino = await _context.depositos.Include(d => d.Productos).FirstOrDefaultAsync(d => d.Id == depositoDestinoId);
            if (depositoOrigen == null || depositoDestino == null) throw new Exception("Depósito no encontrado");

            var productoEnOrigen = depositoOrigen.Productos.FirstOrDefault(p => p.Id == detalleDto.ProductoId);
            var productoEnDestino = depositoDestino.Productos.FirstOrDefault(p => p.Id == detalleDto.ProductoId);

            if (productoEnOrigen != null)
            {
                productoEnOrigen.Int_cantidad_actual += detalleDto.Int_cantidad;
            }

            if (productoEnDestino != null)
            {
                productoEnDestino.Int_cantidad_actual -= detalleDto.Int_cantidad;
                if (productoEnDestino.Int_cantidad_actual < 0) productoEnDestino.Int_cantidad_actual = 0;
            }

            await _context.SaveChangesAsync();
        }
    }*/
    }
}