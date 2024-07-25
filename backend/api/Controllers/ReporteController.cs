using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Producto;
using api.Interfaces;
using api.Models;
using api.Dtos.DetalleDeMovimiento;
using api.Dtos.Movimiento;
using api.Mapper;

namespace api.Controllers
{
  [ApiController]
  [Route("api/reporte")]
  public class ReporteController : ControllerBase
  {
    private readonly IReporteRepository _reporteRepository;

    public ReporteController(IReporteRepository reporteRepository)
    {
      _reporteRepository = reporteRepository;
    }

    [HttpGet]
    [Route("top5productosMasVendidos")]
    public async Task<IActionResult> GetTop5ProductosMasVendidos()
    {
      var productos = await _reporteRepository.GetTop5ProductosMasVendidosAsync();
      var productosDto = productos.Select(p => p.ToProductoDto());

      return Ok(productosDto);
    }

    [HttpGet]
    [Route("top5productosMenosVendidos")]
    public async Task<IActionResult> GetTop5ProductosMenosVendidos()
    {
      var productos = await _reporteRepository.GetTop5ProductosMenosVendidosAsync();
      var productosDto = productos.Select(p => new ProductoDto
      {
        Id = p.Id,
        Str_nombre = p.Str_nombre,
        Str_ruta_imagen = p.Str_ruta_imagen,
        Str_descripcion = p.Str_descripcion,
        Int_cantidad_actual = p.Int_cantidad_actual,
        DepositoId = p.Deposito?.Id,
        DepositoNombre = p.Deposito?.Str_nombre,
        ProveedorId = p.Proveedor?.Id,
        ProveedorNombre = p.Proveedor?.Str_nombre,
        MarcaId = p.Marca?.Id,
        MarcaNombre = p.Marca?.Str_nombre,
        Dec_costo = p.Dec_costo,
        Dec_costo_PPP = p.Dec_costo_PPP,
        Int_iva = p.Int_iva,
        Dec_precio_mayorista = p.Dec_precio_mayorista,
        Dec_precio_minorista = p.Dec_precio_minorista,
        DetallesDeMovimientos = p.DetallesDeMovimientos.Select(d => new DetalleDeMovimientoDto
        {
          Id = d.Id,
          Int_cantidad = d.Int_cantidad,
          MovimientoId = d.MovimientoId,
          ProductoId = d.ProductoId
        }).ToList()
      }).ToList();

      return Ok(productosDto);
    }

    [HttpGet]
    [Route("perdidasPorDeposito")]
    public async Task<IActionResult> GetPerdidasDepositoAsync()
    {
      var movimientos = await _reporteRepository.GetPerdidasAsync();

      // Calculate total quantity
      var totalQuantity = movimientos
          .SelectMany(m => m.DetallesDeMovimientos)
          .Sum(d => d.Int_cantidad);

      // Group by DepositoOrigen and then by MotivoPorTipoDeMovimiento
      var perdidasGroupedByDeposito = movimientos
          .GroupBy(m => m.DepositoOrigen?.Str_nombre)
          .Select(g => new
          {
            Name = g.Key,
            Quantity = g.SelectMany(m => m.DetallesDeMovimientos)
                           .Sum(d => d.Int_cantidad),
            Details = g.GroupBy(m => m.MotivoPorTipoDeMovimiento?.Str_descripcion)
                         .Select(subGroup => new
                         {
                           Motivo = subGroup.Key,
                           Cantidad = subGroup.SelectMany(m => m.DetallesDeMovimientos)
                                                 .Sum(d => d.Int_cantidad)
                         }).ToList()
          }).ToList();

      // Prepare the final result
      var result = new
      {
        Total = totalQuantity,
        Depositos = perdidasGroupedByDeposito
      };

      return Ok(result);
    }


    [HttpGet]
    [Route("perdidasPorProducto")]
    /*
    public async Task<IActionResult> GetPerdidasAsync()
    {
      var movimientos = await _reporteRepository.GetPerdidasAsync();
      var movimientosDto = movimientos.Select(m => new MovimientoDto
      {
        Id = m.Id,
        Date_fecha = m.Date_fecha,
        Str_motivoPorTipoDeMovimiento = m.MotivoPorTipoDeMovimiento?.Str_descripcion ?? string.Empty,
        DepositoOrigenId = m.DepositoOrigenId,
        Str_depositoOrigen = m.DepositoOrigen?.Str_nombre ?? string.Empty,
        DepositoDestinoId = m.DepositoDestinoId,
        Str_depositoDestino = m.DepositoDestino?.Str_nombre ?? string.Empty,
        Bool_borrado = m.Bool_borrado,
        DetallesDeMovimientos = m.DetallesDeMovimientos.Select(d => new DetalleDeMovimientoDto
        {
          Id = d.Id,
          Int_cantidad = d.Int_cantidad,
          MovimientoId = d.MovimientoId,
          ProductoId = d.ProductoId,
          Str_producto = d.Producto?.Str_nombre ?? string.Empty
        }).ToList()
      }).ToList();

      return Ok(movimientosDto);
    }
    */
    public async Task<IActionResult> GetPerdidasAsync()
    {
      var movimientos = await _reporteRepository.GetPerdidasAsync();

      // Calculate total quantity
      var totalQuantity = movimientos
          .SelectMany(m => m.DetallesDeMovimientos)
          .Sum(d => d.Int_cantidad);

      // Group by motivoPorTipoDeMovimiento and calculate quantities
      var perdidasGrouped = movimientos
          .GroupBy(m => m.MotivoPorTipoDeMovimiento?.Str_descripcion)
          .Select(g => new
          {
            motivo = g.Key,
            Quantity = g.SelectMany(m => m.DetallesDeMovimientos)
                          .Sum(d => d.Int_cantidad)
          }).ToList();

      // Prepare the final result
      var result = new
      {
        Total = totalQuantity,
        Perdidas = perdidasGrouped
      };

      return Ok(result);
    }
    //reporte de perdidas por deposito

    [HttpGet]
    [Route("productosConCantidadMinima")]
    public async Task<IActionResult> GetProductosConCantidadMinimaAsync()
    {
      var productos = await _reporteRepository.GetProductosConCantidadMinimaAsync();
      var productosDto = productos.Select(p => new ProductoDto
      {
        Id = p.Id,
        Str_nombre = p.Str_nombre,
        Str_ruta_imagen = p.Str_ruta_imagen,
        Str_descripcion = p.Str_descripcion,
        Int_cantidad_actual = p.Int_cantidad_actual,
        DepositoId = p.Deposito?.Id,
        DepositoNombre = p.Deposito?.Str_nombre,
        ProveedorId = p.Proveedor?.Id,
        ProveedorNombre = p.Proveedor?.Str_nombre,
        MarcaId = p.Marca?.Id,
        MarcaNombre = p.Marca?.Str_nombre,
        Dec_costo = p.Dec_costo,
        Dec_costo_PPP = p.Dec_costo_PPP,
        Int_iva = p.Int_iva,
        Dec_precio_mayorista = p.Dec_precio_mayorista,
        Dec_precio_minorista = p.Dec_precio_minorista,
        DetallesDeMovimientos = p.DetallesDeMovimientos.Select(d => new DetalleDeMovimientoDto
        {
          Id = d.Id,
          Int_cantidad = d.Int_cantidad,
          MovimientoId = d.MovimientoId,
          ProductoId = d.ProductoId
        }).ToList()
      }).ToList();

      return Ok(productosDto);
    }

    [HttpGet]
    [Route("proveedores-mas-comprados")]
    public async Task<IActionResult> GetProveedoresMasCompradosAsync()
    {
      var proveedores = await _reporteRepository.GetProveedoresMasCompradosAsync();
      return Ok(proveedores);
    }
  }
}


