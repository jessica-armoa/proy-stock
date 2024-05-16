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
    public class MarcaRepository : IMarcaRepository
    {
        private readonly ApplicationDbContext _context;
        public MarcaRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Marca> CreateAsync(Marca marcaModel)
        {
            await _context.marcas.AddAsync(marcaModel);
            await _context.SaveChangesAsync();
            return marcaModel;
        }

        public async Task<Marca?> DeleteAsync(int id)
        {
            var marcaModel = await _context.marcas.FirstOrDefaultAsync(m => m.Id == id);
            if(marcaModel == null) return null;

            _context.Remove(marcaModel);
            await _context.SaveChangesAsync();
            return marcaModel;
        }

        public async Task<List<Marca>> GetAllAsync()
        {
            return await _context.marcas.Include(m => m.Productos).ToListAsync();
        }

        public async Task<Marca?> GetByIdAsync(int id)
        {
            return await _context.marcas.Include(m => m.Productos).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<bool> MarcaExists(int id)
        {
            return await _context.marcas.AnyAsync(m => m.Id == id);
        }

        public async Task<Marca?> UpdateAsync(int id, Marca marcaModel)
        {
            var marcaExistente = await _context.marcas.FirstOrDefaultAsync(m => m.Id == id);
            if(marcaExistente == null) return null;

            marcaExistente.Str_nombre = marcaModel.Str_nombre;

            await _context.SaveChangesAsync();
            return marcaExistente;
        }
    }
}