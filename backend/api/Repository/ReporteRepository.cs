using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Deposito;
using api.Dtos.Proveedor;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
  public class ReporteRepository : IReporteRepository
  {
    private readonly ApplicationDbContext _context;

    public ReporteRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<List<Producto>> GetTop5ProductosMasVendidosAsync()
    {
      return await _context.detalles_de_movimientos
          .Where(d => d.Bool_borrado != true)
          .Include(d => d.Producto)
          .ThenInclude(p => p.Marca)
          .Include(d => d.Producto)
          .ThenInclude(p => p.Proveedor)
          .Include(d => d.Producto)
          .ThenInclude(p => p.Deposito)
          .Where(d => d.Movimiento.MotivoPorTipodeMovimientoId == 3)
          .GroupBy(d => d.ProductoId)
          .Select(g => new Producto
          {
            Id = g.Key.Value,
            Str_nombre = g.First().Producto.Str_nombre,
            Str_ruta_imagen = g.First().Producto.Str_ruta_imagen,
            Str_descripcion = g.First().Producto.Str_descripcion,
            Int_cantidad_actual = g.Sum(d => d.Int_cantidad), // Cantidad vendida
            Deposito = g.First().Producto.Deposito,
            Proveedor = g.First().Producto.Proveedor,
            Marca = g.First().Producto.Marca,
            Dec_costo = g.First().Producto.Dec_costo,
            Dec_costo_PPP = g.First().Producto.Dec_costo_PPP,
            Int_iva = g.First().Producto.Int_iva,
            Dec_precio_mayorista = g.First().Producto.Dec_precio_mayorista,
            Dec_precio_minorista = g.First().Producto.Dec_precio_minorista
          })
          .OrderByDescending(p => p.Int_cantidad_actual)
          .Take(5)
          .ToListAsync();
    }

    public async Task<List<Producto>> GetTop5ProductosMenosVendidosAsync()
    {
      return await _context.detalles_de_movimientos
        .Where(d => d.Bool_borrado != true)
          .Include(d => d.Producto)
          .ThenInclude(p => p.Marca)
          .Include(d => d.Producto)
          .ThenInclude(p => p.Proveedor)
          .Include(d => d.Producto)
          .ThenInclude(p => p.Deposito)
          .Where(d => d.Movimiento.MotivoPorTipodeMovimientoId == 3)
          .GroupBy(d => d.ProductoId)
          .Select(g => new Producto
          {
            Id = g.Key.Value,
            Str_nombre = g.First().Producto.Str_nombre,
            Str_ruta_imagen = g.First().Producto.Str_ruta_imagen,
            Str_descripcion = g.First().Producto.Str_descripcion,
            Int_cantidad_actual = g.Sum(d => d.Int_cantidad), // Cantidad vendida
            Deposito = g.First().Producto.Deposito,
            Proveedor = g.First().Producto.Proveedor,
            Marca = g.First().Producto.Marca,
            Dec_costo = g.First().Producto.Dec_costo,
            Dec_costo_PPP = g.First().Producto.Dec_costo_PPP,
            Int_iva = g.First().Producto.Int_iva,
            Dec_precio_mayorista = g.First().Producto.Dec_precio_mayorista,
            Dec_precio_minorista = g.First().Producto.Dec_precio_minorista
          })
          .OrderBy(p => p.Int_cantidad_actual)
          .Take(5)
          .ToListAsync();
    }

    public async Task<List<Movimiento>> GetPerdidasAsync()
    {
      return await _context.movimientos
          .Include(m => m.MotivoPorTipoDeMovimiento)
          .Include(m => m.DepositoOrigen)
          .Include(m => m.DepositoDestino)
          .Include(m => m.DetallesDeMovimientos)
              .ThenInclude(d => d.Producto)
                  .ThenInclude(p => p.Marca)
          .Include(m => m.DetallesDeMovimientos)
              .ThenInclude(d => d.Producto)
                  .ThenInclude(p => p.Proveedor)
          .Include(m => m.DetallesDeMovimientos)
              .ThenInclude(d => d.Producto)
                  .ThenInclude(p => p.Deposito)
          .Where(m => m.MotivoPorTipoDeMovimiento.Motivo.Bool_perdida)
          .OrderByDescending(m => m.Date_fecha)
          .Take(10)
          .ToListAsync();
    }


    public async Task<List<Producto>> GetProductosConCantidadMinimaAsync()
    {
      return await _context.productos
      .Where(p => p.Int_cantidad_actual <= p.Int_cantidad_minima && !p.Bool_borrado)
      .ToListAsync();
    }

    public async Task<List<OnlyProveedorDto>> GetProveedoresMasCompradosAsync()
    {
      return await _context.detalles_de_movimientos
          .Include(d => d.Producto)
          .ThenInclude(p => p.Proveedor)
          .Where(d => !d.Bool_borrado && !d.Producto.Bool_borrado && !d.Producto.Proveedor.Bool_borrado)
          .GroupBy(d => new { d.Producto.ProveedorId, d.Producto.Proveedor.Str_nombre, d.Producto.Proveedor.Str_telefono, d.Producto.Proveedor.Str_direccion, d.Producto.Proveedor.Str_correo })
          .Select(g => new OnlyProveedorDto
          {
            Id = g.Key.ProveedorId ?? 0,
            Str_nombre = g.Key.Str_nombre,
            Str_telefono = g.Key.Str_telefono,
            Str_direccion = g.Key.Str_direccion,
            Str_correo = g.Key.Str_correo,
            Bool_borrado = false,
            TotalCompras = g.Sum(d => d.Int_cantidad)
          })
          .OrderByDescending(p => p.TotalCompras)
          .Take(10)
          .ToListAsync();
    }

  }
}
