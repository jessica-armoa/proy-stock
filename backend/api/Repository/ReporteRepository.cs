using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
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
          .Include(d => d.Producto)
          .ThenInclude(p => p.Marca)
          .Include(d => d.Producto)
          .ThenInclude(p => p.Proveedor)
          .Include(d => d.Producto)
          .ThenInclude(p => p.Deposito)
          //.Where(d => d.Movimiento.TipoDeMovimiento.MotivoId == 1) //El motivo 1 siempre es venta a cliente
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
        .Include(d => d.Producto)
            .ThenInclude(p => p.Marca)
        .Include(d => d.Producto)
            .ThenInclude(p => p.Proveedor)
        .Include(d => d.Producto)
            .ThenInclude(p => p.Deposito)
        .Include(m => m.Movimiento)
            .ThenInclude(m => m.MotivoPorTipoDeMovimiento)
                .ThenInclude(m => m.TipoDeMovimiento)
        .Where(d => d.Movimiento.MotivoPorTipodeMovimientoId == 3) // El motivo 3 siempre es venta a cliente
        .GroupBy(d => d.ProductoId)
        .Select(g => new
        {
          Producto = g.First().Producto,
          CantidadActual = g.First().Int_cantidad // Obtener la cantidad del primer detalle de movimiento en el grupo
        })
        .Select(g => new Producto
        {
          Id = g.Producto.Id,
          Str_nombre = g.Producto.Str_nombre,
          Str_ruta_imagen = g.Producto.Str_ruta_imagen,
          Str_descripcion = g.Producto.Str_descripcion,
          Int_cantidad_actual = g.CantidadActual, // Usar la cantidad del detalle de movimiento
          Deposito = g.Producto.Deposito,
          Proveedor = g.Producto.Proveedor,
          Marca = g.Producto.Marca,
          Dec_costo = g.Producto.Dec_costo,
          Dec_costo_PPP = g.Producto.Dec_costo_PPP,
          Int_iva = g.Producto.Int_iva,
          Dec_precio_mayorista = g.Producto.Dec_precio_mayorista,
          Dec_precio_minorista = g.Producto.Dec_precio_minorista
        })
        .OrderBy(d => d.Int_cantidad_actual) // Orden ascendente
        .Take(5)
        .ToListAsync();
}

    public async Task<List<Producto>> GetPerdidasAsync()
    {
      return await _context.detalles_de_movimientos
          .Include(d => d.Producto)
          .ThenInclude(p => p.Marca)
          .Include(d => d.Producto)
          .ThenInclude(p => p.Proveedor)
          .Include(d => d.Producto)
          .ThenInclude(p => p.Deposito)
          //.Where(d => d.Movimiento.TipoDeMovimiento.Motivo.Bool_perdida)
          .OrderByDescending(d => d.Movimiento.Date_fecha) // Ordenar por fecha de movimiento en orden descendente
          .GroupBy(d => d.ProductoId)
          .Select(g => new Producto
          {
            Id = g.Key.Value,
            Str_nombre = g.First().Producto.Str_nombre,
            Str_ruta_imagen = g.First().Producto.Str_ruta_imagen,
            Str_descripcion = g.First().Producto.Str_descripcion,
            Int_cantidad_actual = g.First().Producto.Int_cantidad_actual,
            Deposito = g.First().Producto.Deposito,
            Proveedor = g.First().Producto.Proveedor,
            Marca = g.First().Producto.Marca,
            Dec_costo = g.First().Producto.Dec_costo,
            Dec_costo_PPP = g.First().Producto.Dec_costo_PPP,
            Int_iva = g.First().Producto.Int_iva,
            Dec_precio_mayorista = g.First().Producto.Dec_precio_mayorista,
            Dec_precio_minorista = g.First().Producto.Dec_precio_minorista
          })
          .ToListAsync();
    }

    public async Task<List<Producto>> GetProductosConCantidadMinimaAsync()
    {
      return await _context.productos
      .Where(p => p.Int_cantidad_actual <= p.Int_cantidad_minima && !p.Bool_borrado)
      .ToListAsync();
    }
  }
}
