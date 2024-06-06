using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Deposito;
using api.Models;

namespace api.Interfaces
{
    public interface IDepositoRepository
    {
        Task<List<DepositoDto>> GetAllAsync();
        Task<DepositoDto?> GetByIdAsync(int? id);
        Task<Deposito> CreateAsync(Deposito depositoModel);
        Task<Deposito?> UpdateAsync(int id, UpdateDepositoRequestDto depositoDto);
        Task<Deposito?> DeleteAsync(int id, DepositoDto depositoDto);
        Task<bool> DepositoExists(int id);
        Task<Producto?> GetProductoEnDepositoAsync(int? depositoId, string producto_nombre);
    }
}