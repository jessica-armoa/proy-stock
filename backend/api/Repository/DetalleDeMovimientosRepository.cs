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

        public async Task<DetalleDeMovimiento?> DeleteAsync(int id, DetalleDeMovimientoDto detalleDto)
        {
            var detalleExistente = await _context.detalles_de_movimientos
                .Where(d => d.Bool_borrado != true)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (detalleExistente == null) return null;

            detalleExistente.Int_cantidad = detalleDto.Int_cantidad;
            detalleExistente.Bool_borrado = detalleDto.Bool_borrado;

            await _context.SaveChangesAsync();
            return detalleExistente;
        }

        public async Task<bool> DetalleExists(int id)
        {
            return await _context.detalles_de_movimientos
            .Where(d => d.Bool_borrado != true)
            .AnyAsync(d => d.Id == id);
        }

        public async Task<List<DetalleDeMovimiento>> GetAllAsync()
        {
            return await _context.detalles_de_movimientos
            .Where(d => d.Bool_borrado != true)
            .Include(d => d.Movimiento)
            .Include(d => d.Producto)
            .Select(d => new DetalleDeMovimiento
            {
                Id = d.Id,
                Int_cantidad = d.Int_cantidad,
                MovimientoId = d.MovimientoId,
                ProductoId = d.ProductoId
            })
            .ToListAsync();
        }

        public async Task<DetalleDeMovimiento?> GetByIdAsync(int id)
        {
            return await _context.detalles_de_movimientos
            .Where(d => d.Bool_borrado != true)
            .Include(d => d.Movimiento)
            .Include(d => d.Producto)
            .Select(d => new DetalleDeMovimiento
            {
                Id = d.Id,
                Int_cantidad = d.Int_cantidad,
                MovimientoId = d.MovimientoId,
                ProductoId = d.ProductoId,
            })
            .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<DetalleDeMovimiento?> UpdateAsync(int id, DetalleDeMovimiento detalleDto)
        {
            var detalleExistente = await _context.detalles_de_movimientos
                .Where(d => d.Bool_borrado != true)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (detalleExistente == null) return null;

            detalleExistente.Int_cantidad = detalleDto.Int_cantidad;
            detalleExistente.Bool_borrado = detalleDto.Bool_borrado;

            await _context.SaveChangesAsync();
            return detalleExistente;
        }
    }
}