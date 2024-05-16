using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IMarcaRepository
    {
        Task<List<Marca>> GetAllAsync();
        Task<Marca?> GetByIdAsync(int id);
        Task<Marca> CreateAsync(Marca marcaModel);
        Task<Marca?> UpdateAsync(int id, Marca marcaModel);
        Task<Marca?> DeleteAsync(int id);
        Task<bool> MarcaExists(int id);
    }
}