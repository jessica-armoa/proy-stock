using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.DetalleDeMovimiento;
using api.Models;

namespace api.Interfaces
{
    public interface IDetalleDeMovimientosRepository
    {
        Task<List<DetalleDeMovimiento>> GetAllAsync();
        Task<DetalleDeMovimiento?> GetByIdAsync(int id);
        Task<DetalleDeMovimiento> CreateAsync(DetalleDeMovimiento detalleModel);
        Task<DetalleDeMovimiento?> UpdateAsync(int id, DetalleDeMovimiento detalleDto);
        Task<DetalleDeMovimiento?> DeleteAsync(int id);
        Task<bool> DetalleExists(int id);
    }
}