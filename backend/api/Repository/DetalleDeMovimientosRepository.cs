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

            var tipoDeMovimiento = await _context.tipos_de_movimientos
                .Where(t => t.Bool_borrado != true)
                .FirstOrDefaultAsync(t => t.Id == movimientoDetalle.TipoDeMovimientoId);

            if (tipoDeMovimiento == null)
            {
                return null;
            }

            if (tipoDeMovimiento.Str_descripcion.ToLower() == "ingreso")
            {
                var productoEnOrigen = await _context.productos
                    .Where(p => p.Bool_borrado != true)
                    .FirstOrDefaultAsync(p => p.Id == detalleModel.ProductoId);

                if (tipoDeMovimiento == null || productoEnOrigen == null)
                {
                    return null;
                }
                productoEnOrigen.Int_cantidad_actual += detalleModel.Int_cantidad;
            }

            if (tipoDeMovimiento.Str_descripcion.ToLower() == "egreso")
            {
                var productoEnOrigen = await _context.productos
                    .Where(p => p.Bool_borrado != true)
                    .FirstOrDefaultAsync(p => p.Id == detalleModel.ProductoId);

                if (tipoDeMovimiento == null || productoEnOrigen == null)
                {
                    return null;
                }
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

            var tipoDeMovimiento = await _context.tipos_de_movimientos
                .Where(t => t.Bool_borrado != true)
                .FirstOrDefaultAsync(t => t.Id == movimientoDetalle.TipoDeMovimientoId);

            if (tipoDeMovimiento == null) return null;

            if (tipoDeMovimiento.Str_descripcion.ToLower() == "ingreso")
            {
                var productoDetalle = await _context.productos
                .Where(p => p.Bool_borrado != true)
                .FirstOrDefaultAsync(p => p.Id == detalleExistente.ProductoId);

                if (productoDetalle == null) return null;
                productoDetalle.Int_cantidad_actual -= detalleExistente.Int_cantidad;
            }

            if (tipoDeMovimiento.Str_descripcion.ToLower() == "egreso")
            {
                var productoDetalle = await _context.productos
                .Where(p => p.Bool_borrado != true)
                .FirstOrDefaultAsync(p => p.Id == detalleExistente.ProductoId);

                if (productoDetalle == null) return null;
                productoDetalle.Int_cantidad_actual += detalleExistente.Int_cantidad;
            }

            if (tipoDeMovimiento.Str_descripcion.ToLower() == "transferencia")
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
            var detalleAntiguo = await _context.detalles_de_movimientos
                .Where(d => d.Bool_borrado != true)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (detalleAntiguo == null) return null;

            var movimientoDetalle = await _context.movimientos
                .Where(m => m.Bool_borrado != true)
                .FirstOrDefaultAsync(m => m.Id == detalleAntiguo.MovimientoId);

            if (movimientoDetalle == null) return null;

            var tipoDeMovimiento = await _context.tipos_de_movimientos
                .Where(t => t.Bool_borrado != true)
                .FirstOrDefaultAsync(t => t.Id == movimientoDetalle.TipoDeMovimientoId);

            if (tipoDeMovimiento == null) return null;

            if (tipoDeMovimiento.Str_descripcion.ToLower() == "ingreso")
            {
                var productoDetalle = await _context.productos
                .Where(p => p.Bool_borrado != true)
                .FirstOrDefaultAsync(p => p.Id == detalleAntiguo.ProductoId);

                if (productoDetalle == null) return null;
                productoDetalle.Int_cantidad_actual -= detalleAntiguo.Int_cantidad;
                productoDetalle.Int_cantidad_actual += detalleDto.Int_cantidad;
            }

            if (tipoDeMovimiento.Str_descripcion.ToLower() == "egreso")
            {
                var productoDetalle = await _context.productos
                .Where(p => p.Bool_borrado != true)
                .FirstOrDefaultAsync(p => p.Id == detalleAntiguo.ProductoId);

                if (productoDetalle == null) return null;
                productoDetalle.Int_cantidad_actual += detalleAntiguo.Int_cantidad;
                productoDetalle.Int_cantidad_actual -= detalleDto.Int_cantidad;
            }

            if (tipoDeMovimiento.Str_descripcion.ToLower() == "transferencia")
            {
                var productoEnOrigen = await _context.productos
                    .Where(p => p.Bool_borrado != true)
                    .FirstOrDefaultAsync(p => p.Id == detalleAntiguo.ProductoId && p.DepositoId == movimientoDetalle.DepositoOrigenId);

                var productoEnDestino = await _context.productos
                    .Where(p => p.Bool_borrado != true)
                    .FirstOrDefaultAsync(p => p.Id == detalleAntiguo.ProductoId && p.DepositoId == movimientoDetalle.DepositoDestinoId);

                if (productoEnDestino == null || productoEnOrigen == null)
                {
                    return null;
                }
                //Borrar las cantidades antiguas
                productoEnOrigen.Int_cantidad_actual += detalleAntiguo.Int_cantidad;
                productoEnDestino.Int_cantidad_actual -= detalleAntiguo.Int_cantidad;
                //Cargar las cantidades nuevas
                productoEnOrigen.Int_cantidad_actual -= detalleDto.Int_cantidad;
                productoEnDestino.Int_cantidad_actual += detalleDto.Int_cantidad;
            }

            detalleAntiguo.Int_cantidad -= detalleAntiguo.Int_cantidad;
            detalleAntiguo.Int_cantidad += detalleDto.Int_cantidad;
            detalleAntiguo.Bool_borrado = false;

            await _context.SaveChangesAsync();
            return detalleAntiguo;
        }
    }
}