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
           /* if (movimientoModel.DetallesDeMovimientos != null && movimientoModel.DetallesDeMovimientos.Any())
            {
                foreach (var detalle in movimientoModel.DetallesDeMovimientos)
                {
                    await _context.detalles_de_movimientos.AddAsync(detalle);
                }
            }*/
            await _context.movimientos.AddAsync(movimientoModel);
            await _context.SaveChangesAsync();
            return movimientoModel;
        }

        public async Task<Movimiento?> DeleteAsync(int id)
        {
            var movimientoModel = await _context.movimientos
                .Where(m => m.Bool_borrado != true)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (movimientoModel == null) return null;

            movimientoModel.Bool_borrado = true;
            await _context.SaveChangesAsync();
            return movimientoModel;
        }

        public async Task<List<Movimiento>> GetAllAsync()
        {
            return await _context.movimientos
            .Where(m => m.Bool_borrado != true) 
            .Include(m => m.MotivoPorTipoDeMovimiento)
            .Include(m => m.DepositoOrigen)
            .Include(m => m.DepositoDestino)
            .Include(m => m.DetallesDeMovimientos).ThenInclude(m => m.Producto)
            .ToListAsync();
        }

        public async Task<Movimiento?> GetByIdAsync(int? id)
        {
            return await _context.movimientos
            .Where(m => m.Bool_borrado != true)
            .Include(m => m.MotivoPorTipoDeMovimiento)
            .Include(m => m.DepositoOrigen)
            .Include(m => m.DepositoDestino)
            .Include(m => m.DetallesDeMovimientos)
            .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task GuardarCambiosAsync()
        {
            await _context.SaveChangesAsync();
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

            if (movimientoExistente == null) return null;

            movimientoExistente.Date_fecha = movimientoDto.Date_fecha;
            movimientoExistente.Bool_borrado = false;

            if (movimientoDto.DetallesDeMovimientos != null)
            {
                foreach (var detalle in movimientoDto.DetallesDeMovimientos)
                {
                    if (movimientoExistente.DetallesDeMovimientos != null)
                    {
                        var detalleExistente = movimientoExistente.DetallesDeMovimientos
                            .Where(d => d.Bool_borrado != true)
                            .FirstOrDefault(d => d.Id == detalle.Id);

                        if (detalleExistente == null)
                        {
                            return null;
                        }
                        /*
                            AQUI REALIZAR CAMBIOS, DEPENDE EL TIPO DE MOVIMIENTO
                        */
                        
                        detalleExistente.Int_cantidad = detalle.Int_cantidad;
                        detalleExistente.Dec_costo = detalle.Dec_costo;
                    }
                }
            }

            await _context.SaveChangesAsync();
            return movimientoExistente;
        }
    }
}