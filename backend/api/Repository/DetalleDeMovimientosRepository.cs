using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.DetalleDeMovimiento;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class DetalleDeMovimientosRepository : IDetalleDeMovimientosRepository
    {
        private readonly ApplicationDbContext _context;
        public DetalleDeMovimientosRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<DetalleDeMovimiento> CreateAsync(DetalleDeMovimiento detalleModel)
        {
            await _context.detalles_de_movimientos.AddAsync(detalleModel);
            await _context.SaveChangesAsync();
            return detalleModel;
        }

        public async Task<DetalleDeMovimiento?> DeleteAsync(int id)
        {
            var detalleModel = await _context.detalles_de_movimientos.FirstOrDefaultAsync(d => d.Id == id);
            if(detalleModel == null) return null;

            _context.detalles_de_movimientos.Remove(detalleModel);
            await _context.SaveChangesAsync();
            return detalleModel;
        }

        public async Task<bool> DetalleExists(int id)
        {
            return await _context.detalles_de_movimientos.AnyAsync(d => d.Id == id);
        }

        public async Task<List<DetalleDeMovimiento>> GetAllAsync()
        {
            return await _context.detalles_de_movimientos
            .Include(d => d.Movimiento)
            .Include(d => d.Producto)
            .ToListAsync();
        }

        public async Task<DetalleDeMovimiento?> GetByIdAsync(int id)
        {
            return await _context.detalles_de_movimientos
            .Include(d => d.Movimiento)
            .Include(d => d.Producto)
            .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<DetalleDeMovimiento?> UpdateAsync(int id, DetalleDeMovimiento detalleDto)
        {
            var detalleExistente = await _context.detalles_de_movimientos.FirstOrDefaultAsync(d => d.Id == id);
            if(detalleExistente == null) return null;

            _context.Entry(detalleExistente).State = EntityState.Modified;

           await _context.SaveChangesAsync();
           return detalleExistente;
        }
    }
}