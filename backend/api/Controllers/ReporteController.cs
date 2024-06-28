using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Producto;
using api.Interfaces;
using api.Models;
using api.Dtos.DetalleDeMovimiento;
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
    [Route("perdidas")]
    public async Task<IActionResult> GetPerdidasAsync()
    {
      var productos = await _reporteRepository.GetPerdidasAsync();
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
  }
}
