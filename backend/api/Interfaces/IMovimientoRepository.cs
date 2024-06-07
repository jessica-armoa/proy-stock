using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Movimiento;
using api.Models;

namespace api.Interfaces
{
    public interface IMovimientoRepository
    {
        Task<List<Movimiento>> GetAllAsync();
        Task<Movimiento?> GetByIdAsync(int? id);
        Task<Movimiento> CreateAsync(Movimiento movimientoModel);
        Task<Movimiento?> UpdateAsync(int id, Movimiento movimientoDto);
        Task<Movimiento?> DeleteAsync(int id); 
        Task<bool> MovimientoExists(int id);
    }
}