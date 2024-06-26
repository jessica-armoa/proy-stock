using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
  public interface IReporteRepository
  {
    Task<List<Producto>> GetTop5ProductosMasVendidosAsync();
    Task<List<Producto>> GetTop5ProductosMenosVendidosAsync();
    Task<List<Producto>> GetPerdidasAsync();
    Task<List<Producto>> GetProductosConCantidadMinimaAsync();
  }
}
