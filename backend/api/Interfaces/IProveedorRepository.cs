using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IProveedorRepository
    {
        Task<List<Proveedor>> GetAllAsync();
        Task<Proveedor?> GetByIdAsync(int id);
        //Task<Proveedor?> GetByNombreAsync(string nombre);
        Task<Proveedor> CreateAsync(Proveedor proveedorModel);
        Task<Proveedor?> UpdateAsync(int id, Proveedor proveedorModel);
        Task<Proveedor?> DeleteAsync(int id); 
        Task<bool> ProveedorExists(int id);
    }
}