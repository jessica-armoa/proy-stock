using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ProveedorRepository : IProveedorRepository
    {
        private readonly ApplicationDbContext _context;
        public ProveedorRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Proveedor> CreateAsync(Proveedor proveedorModel)
        {
            await _context.proveedores.AddAsync(proveedorModel);
            await _context.SaveChangesAsync();
            return proveedorModel;
        }

        public async Task<Proveedor?> DeleteAsync(int id)
        {
            var proveedorModel = await _context.proveedores.FirstOrDefaultAsync(p => p.Id == id);
            if(proveedorModel == null) return null;

            _context.proveedores.Remove(proveedorModel);
            await _context.SaveChangesAsync();
            return proveedorModel;
        }

        public async Task<List<Proveedor>> GetAllAsync()
        {
            return await _context.proveedores
            .Include(p => p.Productos)
            .Include(p => p.Categorias)
            .ToListAsync();
        }

        public async Task<Proveedor?> GetByIdAsync(int id)
        {
            return await _context.proveedores
            .Include(p => p.Productos)
            .Include(p => p.Categorias)
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> ProveedorExists(int id)
        {
            return await _context.proveedores.AnyAsync(p => p.Id == id);
        }

        public async Task<Proveedor?> UpdateAsync(int id, Proveedor proveedorModel)
        {
            var proveedorExistente = await _context.proveedores.FirstOrDefaultAsync(p => p.Id == id);
            if(proveedorExistente == null) return null;

            proveedorExistente.Str_nombre = proveedorModel.Str_nombre;
            proveedorExistente.Str_telefono = proveedorModel.Str_telefono;
            proveedorExistente.Str_direccion = proveedorModel.Str_direccion;
            proveedorExistente.Str_correo = proveedorModel.Str_correo;

            await _context.SaveChangesAsync();
            return proveedorExistente;
        }
    }
}