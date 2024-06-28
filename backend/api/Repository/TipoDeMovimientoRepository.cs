using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.TipoDeMovimiento;
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

        public async Task<TipoDeMovimiento> CreateAsync(TipoDeMovimiento tipoDeMovimientoModel)
        {
            await _context.tipos_de_movimientos.AddAsync(tipoDeMovimientoModel);
            await _context.SaveChangesAsync();
            return tipoDeMovimientoModel;
        }

        public async Task<TipoDeMovimiento?> DeleteAsync(int id)
        {
            var tipoDeMovimientoExistente = await _context.tipos_de_movimientos
                .Where(t => t.Bool_borrado != true)
                .FirstOrDefaultAsync(p => p.Id == id);

            if(tipoDeMovimientoExistente == null) return null;

            tipoDeMovimientoExistente.Bool_borrado = true;

            await _context.SaveChangesAsync();
            return tipoDeMovimientoExistente;
        }

        public async Task<List<TipoDeMovimiento>> GetAllAsync()
        {
            return await _context.tipos_de_movimientos
            .Where(p => p.Bool_borrado != true)
            .ToListAsync();
        }

        public async Task<TipoDeMovimiento?> GetByIdAsync(int? id)
        {
            return await _context.tipos_de_movimientos
            .Where(p => p.Bool_borrado != true)
            .Include(m=>m.MotivosPorTipoDeMovimiento)
            .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<bool> TipoDeMovimientoExists(int id)
        {
            return await _context.tipos_de_movimientos
            .Where(t => t.Bool_borrado != true)
            .AnyAsync(t => t.Id == id);
        }

        public async Task<TipoDeMovimiento?> UpdateAsync(int id, TipoDeMovimiento tipoDeMovimientoModel)
        {
            var tipoDeMovimientoExistente = await _context.tipos_de_movimientos
                .Where(t => t.Bool_borrado != true)
                .FirstOrDefaultAsync(t => t.Id == id);
            
            if(tipoDeMovimientoExistente == null) return null;

            tipoDeMovimientoExistente.Str_tipo = tipoDeMovimientoModel.Str_tipo;
            tipoDeMovimientoExistente.Bool_operacion = tipoDeMovimientoModel.Bool_operacion;
            tipoDeMovimientoExistente.Bool_borrado = false;

            await _context.SaveChangesAsync();
            return tipoDeMovimientoExistente;
        }   

    }
}