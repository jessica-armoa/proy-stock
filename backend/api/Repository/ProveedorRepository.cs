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
            var proveedorExistente = await _context.proveedores
                .Where(p => p.Bool_borrado != true)
                .FirstOrDefaultAsync(p => p.Id == id);

            if(proveedorExistente == null) return null;

            proveedorExistente.Bool_borrado = true;
            
            await _context.SaveChangesAsync();
            return proveedorExistente;
        }

        public async Task<List<Proveedor>> GetAllAsync()
        {
            return await _context.proveedores
            .Where(p => p.Bool_borrado != true)
            .Include(p => p.Productos)
            .Include(p => p.Categorias)
            .Include(p => p.Marcas)
            .ToListAsync();
        }

        public async Task<Proveedor?> GetByIdAsync(int id)
        {
            return await _context.proveedores
            .Where(p => p.Bool_borrado != true)
            .Include(p => p.Productos)
            .Include(p => p.Categorias)
            .Include(p => p.Marcas)
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> ProveedorExists(int id)
        {
            return await _context.proveedores
            .Where(p => p.Bool_borrado != true)
            .AnyAsync(p => p.Id == id);
        }

        public async Task<Proveedor?> UpdateAsync(int id, Proveedor proveedorModel)
        {
            var proveedorExistente = await _context.proveedores
                .Where(p => p.Bool_borrado != true)
                .FirstOrDefaultAsync(p => p.Id == id);

            if(proveedorExistente == null) return null;

            proveedorExistente.Str_nombre = proveedorModel.Str_nombre;
            proveedorExistente.Str_telefono = proveedorModel.Str_telefono;
            proveedorExistente.Str_direccion = proveedorModel.Str_direccion;
            proveedorExistente.Str_correo = proveedorModel.Str_correo;
            proveedorExistente.Bool_borrado = false;

            await _context.SaveChangesAsync();
            return proveedorExistente;
        }
    }
}