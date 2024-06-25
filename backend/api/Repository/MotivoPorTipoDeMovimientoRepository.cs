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
    public class MotivoPorTipoDeMovimientoRepository : IMotivoPorTipoDeMovimientoRepository
    {
        private readonly ApplicationDbContext _context;
        public MotivoPorTipoDeMovimientoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<MotivoPorTipoDeMovimiento> CreateAsync(MotivoPorTipoDeMovimiento motivoPorTipoDeMovimientoModel)
        {
            await _context.motivos_por_tipo_de_movimiento.AddAsync(motivoPorTipoDeMovimientoModel);
            await _context.SaveChangesAsync();
            return motivoPorTipoDeMovimientoModel;
        }

        public async Task<MotivoPorTipoDeMovimiento?> DeleteAsync(int id)
        {
            var motivoPorTipoDeMovimientoExistente = await _context.motivos_por_tipo_de_movimiento
                .Where(m => m.Bool_borrado != true)
                .FirstOrDefaultAsync(m => m.Id == id);

            if(motivoPorTipoDeMovimientoExistente == null) return null;

            motivoPorTipoDeMovimientoExistente.Bool_borrado = true;

            await _context.SaveChangesAsync();
            return motivoPorTipoDeMovimientoExistente;
        }

        public async Task<List<MotivoPorTipoDeMovimiento>> GetAllAsync()
        {
            return await _context.motivos_por_tipo_de_movimiento
            .Where(m => m.Bool_borrado != true)
            .Include(m => m.Movimientos).ThenInclude(m => m.DepositoOrigen)
            .Include(m => m.Movimientos).ThenInclude(m => m.DepositoDestino)
            .ToListAsync();
        }

        public async Task<MotivoPorTipoDeMovimiento?> GetByIdAsync(int? id)
        {
            return await _context.motivos_por_tipo_de_movimiento
            .Where(m => m.Bool_borrado != true)
            .Include(m => m.Movimientos).ThenInclude(m => m.DepositoOrigen)
            .Include(m => m.Movimientos).ThenInclude(m => m.DepositoDestino)
            .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<bool> MotivoPorTipoMovimientoExists(int id)
        {
            return await _context.motivos_por_tipo_de_movimiento
            .Where(m => m.Bool_borrado != true)
            .AnyAsync(m => m.Id == id);
        }

        public Task<MotivoPorTipoDeMovimiento?> UpdateAsync(int id, MotivoPorTipoDeMovimiento motivoPorTipoDeMovimientoModel)
        {
            throw new NotImplementedException();
        }
    }
}