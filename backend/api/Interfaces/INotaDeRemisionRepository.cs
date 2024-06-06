using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
  public interface INotaDeRemisionRepository
  {
    Task<List<NotaDeRemision>> GetAllAsync();
    Task<NotaDeRemision?> GetByIdAsync(int? id);
  }
}