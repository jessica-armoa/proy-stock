using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models;
using api.Dtos;
using api.Dtos.Proveedor;

namespace api.Interfaces
{
  public interface IReporteRepository
  {
    Task<List<Producto>> GetTop5ProductosMasVendidosAsync();
    Task<List<Producto>> GetTop5ProductosMenosVendidosAsync();
    Task<List<Movimiento>> GetPerdidasAsync();
    Task<List<Producto>> GetProductosConCantidadMinimaAsync();
    Task<List<OnlyProveedorDto>> GetProveedoresMasCompradosAsync();
  }
}
