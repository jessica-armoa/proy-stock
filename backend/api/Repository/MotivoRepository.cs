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
    public class MotivoRepository : IMotivoRepository
    {
        private readonly ApplicationDbContext _context;
        public MotivoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Motivo> CreateAsync(Motivo motivoModel)
        {
            await _context.motivos.AddAsync(motivoModel);
            await _context.SaveChangesAsync();
            return motivoModel;
        }

        public async Task<Motivo?> DeleteAsync(int id)
        {
            var motivoExistente = await _context.motivos
                .Where(m => m.Bool_borrado != true)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (motivoExistente == null) return null;

            motivoExistente.Bool_borrado = true;

            await _context.SaveChangesAsync();
            return motivoExistente;
        }

        public async Task<List<Motivo>> GetAllAsync()
        {
            return await _context.motivos
            .Where(m => m.Bool_borrado != true)
            .Include(m => m.MotivosPorTipoDeMovimiento)
            .ToListAsync(); 
        }

        public async Task<Motivo?> GetByIdAsync(int? id)
        {
            return await _context.motivos
            .Where(m => m.Bool_borrado != true)
            .Include(m => m.MotivosPorTipoDeMovimiento)
            .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<bool> MotivoExists(int id)
        {
            return await _context.motivos
            .Where(m => m.Bool_borrado != true)
            .AnyAsync(m => m.Id == id);
        }

        public Task<Motivo?> UpdateAsync(int id, Motivo motivoModel)
        {
            throw new NotImplementedException();
        }
    }
}