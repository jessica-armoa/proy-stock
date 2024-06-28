using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Ferreteria;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class FerreteriaRepository : IFerreteriaRepository
    {
        private readonly ApplicationDbContext _context;
        public FerreteriaRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Ferreteria> CreateAsync(Ferreteria ferreteriaModel)
        {
            await _context.ferreterias.AddAsync(ferreteriaModel);
            await _context.SaveChangesAsync();
            return ferreteriaModel;
        }

        public async Task<Ferreteria?> DeleteAsync(int id)
        {
            var ferreteriaExistente = await _context.ferreterias
                .Where(f => f.Bool_borrado != true)
                .FirstOrDefaultAsync(f => f.Id == id);

            if(ferreteriaExistente == null) return null;

            ferreteriaExistente.Bool_borrado = true;

            await _context.SaveChangesAsync();
            return ferreteriaExistente;
        }

        public async Task<bool> FerreteriaExists(int id)
        {
            return await _context.ferreterias
            .Where(f => f.Bool_borrado != true)
            .AnyAsync(f => f.Id == id);
        }

        public async Task<List<Ferreteria>> GetAllAsync()
        {
            return await _context.ferreterias
            .Where(f => f.Bool_borrado != true)
            .ToListAsync();
        }

        public async Task<Ferreteria?> GetByIdAsync(int id)
        {
            return await _context.ferreterias
            .Where(f => f.Bool_borrado != true)
            .Include(f => f.Depositos)
            .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Ferreteria?> UpdateAsync(int id, Ferreteria ferreteriaModel)
        {
            var ferreteriaExistente = await _context.ferreterias
                .Where(f => f.Bool_borrado != true)
                .FirstOrDefaultAsync(f => f.Id == id);

            if(ferreteriaExistente == null) return null;

            ferreteriaExistente.Str_nombre = ferreteriaModel.Str_nombre;
            ferreteriaExistente.Str_ruc = ferreteriaModel.Str_ruc;
            ferreteriaExistente.Str_telefono = ferreteriaModel.Str_telefono;
            ferreteriaExistente.Bool_borrado = ferreteriaModel.Bool_borrado;

            await _context.SaveChangesAsync();
            return ferreteriaExistente;
        }
    }
}