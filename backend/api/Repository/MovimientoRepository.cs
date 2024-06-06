using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.DetalleDeMovimiento;
using api.Dtos.Movimiento;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class MovimientoRepository : IMovimientoRepository
    {
        private readonly ApplicationDbContext _context;
        public MovimientoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Movimiento> CreateAsync(Movimiento movimientoModel)
        {
            if(movimientoModel.DetallesDeMovimientos != null && movimientoModel.DetallesDeMovimientos.Any())
            {
                foreach(var detalle in movimientoModel.DetallesDeMovimientos)
                {
                    _context.Entry(detalle).State = EntityState.Added;
                }
            }
            await _context.movimientos.AddAsync(movimientoModel);
            await _context.SaveChangesAsync();
            return movimientoModel;
        }

        public async Task<Movimiento?> DeleteAsync(int id)
        {
            var movimientoModel = await _context.movimientos.FirstOrDefaultAsync(p => p.Id == id);
            if(movimientoModel == null) return null;

            _context.movimientos.Remove(movimientoModel);
            await _context.SaveChangesAsync();
            return movimientoModel;
        }

        public async Task<List<Movimiento>> GetAllAsync()
        {
            return await _context.movimientos
            .Where(m => m.Bool_borrado != true)
            .Include(m => m.DetallesDeMovimientos)
            .Where(d => d.Bool_borrado != true)
            .ToListAsync();
        }

        public async Task<Movimiento?> GetByIdAsync(int? id)
        {
            return await _context.movimientos
            .Where(m => m.Bool_borrado != true)
            .Include(m => m.DetallesDeMovimientos)
            .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<bool> MovimientoExists(int id)
        {
            return await _context.movimientos
            .Where(m => m.Bool_borrado != true)
            .AnyAsync(s => s.Id == id);
        }

        public async Task<Movimiento?> UpdateAsync(int id, Movimiento movimientoDto)
        {
            var movimientoExistente = await _context.movimientos
            .Where(m => m.Bool_borrado != true)
            .Include(m => m.DetallesDeMovimientos)
            .Where(d => d.Bool_borrado != true)
            .FirstOrDefaultAsync(m => m.Id == id);

            if(movimientoExistente == null) return null;

            movimientoExistente.Date_fecha = movimientoDto.Date_fecha;
            movimientoExistente.Bool_borrado = movimientoDto.Bool_borrado;

            if(movimientoDto.DetallesDeMovimientos != null)
            {
                foreach(var detalle in movimientoDto.DetallesDeMovimientos)
                {
                    if(movimientoExistente.DetallesDeMovimientos != null)
                    {
                        var detalleExistente = movimientoExistente.DetallesDeMovimientos
                            .Where(d => d.Bool_borrado != true)
                            .FirstOrDefault(d => d.Id == detalle.Id);

                        if(detalleExistente != null)
                        {
                            detalleExistente.Int_cantidad = detalle.Int_cantidad;
                        }
                    }
                }
            }
            
            await _context.SaveChangesAsync();
            return movimientoExistente;
        }
    }
}