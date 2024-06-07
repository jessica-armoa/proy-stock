using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
  public interface ITimbradoRepository
  {
    Task<List<Timbrado>> GetAllAsync();
    Task<Timbrado?> GetByIdAsync(int id);
    Task<Timbrado?> GetTimbradoActivoAsync(); // Método para obtener el timbrado activo
    Task CreateAsync(Timbrado timbrado);
  }
}
