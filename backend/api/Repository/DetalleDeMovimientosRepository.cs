using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.DetalleDeMovimiento;
using api.Dtos.Producto;
using api.Interfaces;
using api.Mapper;
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
            var movimientoDetalle = await _context.movimientos
                .Where(m => m.Bool_borrado != true)
                .FirstOrDefaultAsync(m => m.Id == detalleModel.MovimientoId);

            if (movimientoDetalle == null)
            {
                return null;
            }

            var motivoPorTipoMovimiento = await _context.motivos_por_tipo_de_movimiento
                .Where(m => m.Bool_borrado != true)
                .FirstOrDefaultAsync(m => m.Id == movimientoDetalle.MotivoPorTipodeMovimientoId);

            var tipoDeMovimiento = await _context.tipos_de_movimientos
                .Where(t => t.Bool_borrado != true)
                .FirstOrDefaultAsync(t => t.Id == motivoPorTipoMovimiento.TipodemovimientoId);

            if (tipoDeMovimiento == null)
            {
                return null;
            }

            if (tipoDeMovimiento.Str_tipo.ToLower() == "ingreso")
            {
                var productoEnDestino = await _context.productos
                    .Where(p => p.Bool_borrado != true)
                    .FirstOrDefaultAsync(p => p.Id == detalleModel.ProductoId);

                if (productoEnDestino == null)
                {
                    return null;
                }
                productoEnDestino.Dec_costo_PPP = (((productoEnDestino.Int_cantidad_actual * productoEnDestino.Dec_costo_PPP) + (detalleModel.Int_cantidad * detalleModel.Dec_costo)) / (productoEnDestino.Int_cantidad_actual + detalleModel.Int_cantidad));
                productoEnDestino.Int_cantidad_actual += detalleModel.Int_cantidad;

            }

            if (tipoDeMovimiento.Str_tipo.ToLower() == "egreso")
            {
                var productoEnOrigen = await _context.productos
                    .Where(p => p.Bool_borrado != true)
                    .FirstOrDefaultAsync(p => p.Id == detalleModel.ProductoId && p.DepositoId == movimientoDetalle.DepositoOrigenId);

                if (productoEnOrigen == null)
                {
                    return null;
                }

                detalleModel.Dec_costo = productoEnOrigen.Dec_costo_PPP;
                productoEnOrigen.Int_cantidad_actual -= detalleModel.Int_cantidad;
            }


            await _context.detalles_de_movimientos.AddAsync(detalleModel);
            await _context.SaveChangesAsync();
            return detalleModel;
        }

        public async Task<DetalleDeMovimiento?> DeleteAsync(int id)
        {
            var detalleExistente = await _context.detalles_de_movimientos
               .Where(d => d.Bool_borrado != true)
               .FirstOrDefaultAsync(d => d.Id == id);
            if (detalleExistente == null) return null;

            var movimientoDetalle = await _context.movimientos
                .Where(m => m.Bool_borrado != true)
                .FirstOrDefaultAsync(m => m.Id == detalleExistente.MovimientoId);
            if (movimientoDetalle == null) return null;

            var motivoPorTipoMovimiento = await _context.motivos_por_tipo_de_movimiento
                .Where(m => m.Bool_borrado != true)
                .FirstOrDefaultAsync(m => m.Id == movimientoDetalle.MotivoPorTipodeMovimientoId);
            if (motivoPorTipoMovimiento == null) return null;

            var tipoDeMovimiento = await _context.tipos_de_movimientos
                .Where(t => t.Bool_borrado != true)
                .FirstOrDefaultAsync(t => t.Id == motivoPorTipoMovimiento.TipodemovimientoId);
            if (tipoDeMovimiento == null) return null;

            if (tipoDeMovimiento.Str_tipo.ToLower() == "ingreso")
            {
                var productoDetalle = await _context.productos
                .Where(p => p.Bool_borrado != true)
                .FirstOrDefaultAsync(p => p.Id == detalleExistente.ProductoId);

                if (productoDetalle == null) return null;
                productoDetalle.Int_cantidad_actual -= detalleExistente.Int_cantidad;
            }

            if (tipoDeMovimiento.Str_tipo.ToLower() == "egreso")
            {
                var productoDetalle = await _context.productos
                .Where(p => p.Bool_borrado != true)
                .FirstOrDefaultAsync(p => p.Id == detalleExistente.ProductoId);

                if (productoDetalle == null) return null;
                productoDetalle.Int_cantidad_actual += detalleExistente.Int_cantidad;
            }

            if (tipoDeMovimiento.Str_tipo.ToLower() == "transferencia")
            {
                var productoEnOrigen = await _context.productos
                    .Where(p => p.Bool_borrado != true)
                    .FirstOrDefaultAsync(p => p.Id == detalleExistente.ProductoId && p.DepositoId == movimientoDetalle.DepositoOrigenId);

                var productoEnDestino = await _context.productos
                    .Where(p => p.Bool_borrado != true)
                    .FirstOrDefaultAsync(p => p.Id == detalleExistente.ProductoId && p.DepositoId == movimientoDetalle.DepositoDestinoId);

                if (productoEnDestino == null)
                {
                    return null;
                }

                productoEnOrigen.Int_cantidad_actual += detalleExistente.Int_cantidad;
                productoEnDestino.Int_cantidad_actual -= detalleExistente.Int_cantidad;
            }
            
            detalleExistente.Bool_borrado = true;

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
                .Include(d => d.Producto)
                .ToListAsync();
        }

        public async Task<DetalleDeMovimiento?> GetByIdAsync(int id)
        {
            return await _context.detalles_de_movimientos
                .Where(d => d.Bool_borrado != true)
                .Include(d => d.Producto)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public Task<DetalleDeMovimiento?> UpdateAsync(int id, DetalleDeMovimiento detalleDto)
        {
            throw new NotImplementedException();
        }
        
    }
}