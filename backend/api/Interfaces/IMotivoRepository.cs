using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IMotivoRepository
    {
        Task<List<Motivo>> GetAllAsync();
        Task<Motivo?> GetByIdAsync(int? id);
        Task<Motivo> CreateAsync(Motivo motivoModel);
        Task<Motivo?> UpdateAsync(int id, Motivo motivoModel);
        Task<Motivo?> DeleteAsync(int id);
        Task<bool> MotivoExists(int id);
    }
}