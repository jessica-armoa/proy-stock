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
    public class TipoDeMovimientoRepository : ITipoDeMovimientoRepository
    {
        private readonly ApplicationDbContext _context;
        public TipoDeMovimientoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<TipoDeMovimiento>> GetAllAsync()
        {
            return await _context.tipos_de_movimientos
            .Where(p => p.Bool_borrado != true)
            .Include(t => t.Movimientos)
            .Include(t => t.Motivo)
            .ToListAsync();
        }

        public async Task<TipoDeMovimiento?> GetByIdAsync(int? id)
        {
            return await _context.tipos_de_movimientos
            .Where(p => p.Bool_borrado != true)
            .Include(t => t.Movimientos)
            .Include(t => t.Motivo)
            .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<bool> TipoDeMovimientoExists(int id)
        {
            return await _context.tipos_de_movimientos
            .Where(t => t.Bool_borrado != true)
            .AnyAsync(t => t.Id == id);
        }
    }
}