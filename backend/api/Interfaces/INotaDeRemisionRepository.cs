using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
  public interface INotaDeRemisionRepository
  {
    Task CreateAsync(NotaDeRemision notaDeRemision);
    Task<List<NotaDeRemision>> GetAllAsync();
    Task<NotaDeRemision?> GetByIdAsync(int? id);
    Task<NotaDeRemision?> GetUltimaNotaDeRemisionAsync();// Método para obtener la última nota de remisión creada
    Task UpdateAsync(NotaDeRemision notaDeRemision);
  }
}