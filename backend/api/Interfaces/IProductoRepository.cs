using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Producto;
using api.Models;

namespace api.Interfaces
{
    public interface IProductoRepository
    {
        Task<List<Producto>> GetAllAsync();
        Task<List<Producto>> ObtenerProductosPorDepositoAsync(int depositoId);
        Task<Producto?> GetByIdAsync(int? id);
        Task<Producto?> GetByNombreAsync(string nombre);
        Task<Producto> CreateAsync(Producto productoModel);
        Task<Producto?> UpdateAsync(int id, Producto productoDto);
        Task<Producto?> DeleteAsync(int id); 
        Task<bool> ProductoExists(int id);
        Task<bool> ProductoExistsName(string nombreProducto);
        Task<Producto?> ObtenerProductoEnDeposito(string productoNombre, int? depositoId);
    }
}