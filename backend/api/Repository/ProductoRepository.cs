using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Producto;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Producto> CreateAsync(Producto productoModel)
        {
            await _context.productos.AddAsync(productoModel);
            await _context.SaveChangesAsync();
            return productoModel;
        }

        public async Task<Producto?> DeleteAsync(int id)
        {
            var productoModel = await _context.productos.FirstOrDefaultAsync(p => p.Id == id);
            if(productoModel == null)
            {
                return null;
            }

            _context.productos.Remove(productoModel);
            await _context.SaveChangesAsync();
            return productoModel;
        }

        public async Task<List<Producto>> GetAllAsync()
        {
            return await _context.productos.Include(p => p.Detalles_De_Movimientos).ToListAsync();
        }

        public async Task<Producto?> GetByIdAsync(int id)
        {
            return await _context.productos.Include(p => p.Detalles_De_Movimientos).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Producto?> GetByNombreAsync(string nombre)
        {
            return await _context.productos.FirstOrDefaultAsync(s => s.Str_nombre == nombre);
        }

        public async Task<bool> ProductoExists(int id)
        {
            return await _context.productos.AnyAsync(s => s.Id == id);
        }

        public async Task<Producto?> UpdateAsync(int id, UpdateProductoRequestDto productoDto)
        {
            var productoExistente = await _context.productos.FirstOrDefaultAsync(s => s.Id == id);
            if(productoExistente == null)
            {
                return null;
            }

            productoExistente.Str_ruta_imagen = productoDto.str_ruta_imagen;
            productoExistente.Str_nombre = productoDto.str_nombre;
            productoExistente.Str_descripcion = productoDto.str_descripcion;
            productoExistente.Int_cantidad_actual = productoDto.int_cantidad_actual;
            productoExistente.Int_cantidad_minima = productoDto.int_cantidad_minima;
            productoExistente.Dec_costo_PPP = productoDto.dec_costo_PPP;
            productoExistente.Int_iva = productoDto.int_iva;
            productoExistente.Dec_precio_mayorista = productoDto.dec_precio_mayorista;
            productoExistente.Dec_precio_minorista = productoDto.dec_precio_minorista;

            await _context.SaveChangesAsync();
            return productoExistente;
        }
    }
}