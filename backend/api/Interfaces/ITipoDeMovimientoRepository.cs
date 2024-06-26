using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.TipoDeMovimiento;
using api.Models;

namespace api.Interfaces
{
    public interface ITipoDeMovimientoRepository
    {
        Task<List<TipoDeMovimiento>> GetAllAsync();
        Task<TipoDeMovimiento?> GetByIdAsync(int? id);
        Task<bool> TipoDeMovimientoExists(int id);
        Task<TipoDeMovimiento> CreateAsync(TipoDeMovimiento tipoDeMovimientoModel);
        Task<TipoDeMovimiento?> UpdateAsync(int id, TipoDeMovimiento tipoDeMovimientoModel);
        Task<TipoDeMovimiento?> DeleteAsync(int id); 
    }
}