using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IMotivoPorTipoDeMovimientoRepository
    {
        Task<List<MotivoPorTipoDeMovimiento>> GetAllAsync();
        Task<MotivoPorTipoDeMovimiento?> GetByIdAsync(int? id);
        Task<bool> MotivoPorTipoMovimientoExists(int id);
        Task<MotivoPorTipoDeMovimiento> CreateAsync(MotivoPorTipoDeMovimiento motivoPorTipoDeMovimientoModel);
        Task<MotivoPorTipoDeMovimiento?> UpdateAsync(int id, MotivoPorTipoDeMovimiento motivoPorTipoDeMovimientoModel);
        Task<MotivoPorTipoDeMovimiento?> DeleteAsync(int id);
    }
}