using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Deposito;
using api.Dtos.Movimiento;
using api.Dtos.Producto;
using api.Dtos.DetalleDeMovimiento;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;
using api.Dtos.Deposito.api.Dtos.Deposito;

namespace api.Repository
{
    public class DepositoRepository : IDepositoRepository
    {
        private readonly ApplicationDbContext _context;
        public DepositoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Deposito> CreateAsync(Deposito depositoModel)
        {
            await _context.depositos.AddAsync(depositoModel);
            await _context.SaveChangesAsync();
            return depositoModel;
        }

        public async Task<Deposito?> DeleteAsync(int id)
        {
            var depositoExistente = await _context.depositos
                .Where(d => !d.Bool_borrado)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (depositoExistente == null) return null;

            depositoExistente.Bool_borrado = true;
            await _context.SaveChangesAsync();
            return depositoExistente;
        }

        public async Task<bool> DepositoExists(int id)
        {
            return await _context.depositos
                .Where(d => !d.Bool_borrado)
                .AnyAsync(d => d.Id == id);
        }

        public async Task<List<DepositoDto>> GetAllAsync()
        {
            return await _context.depositos
                .Where(d => !d.Bool_borrado)
                .Include(d => d.Movimientos)
                .Include(d => d.Productos)
                .Include(d => d.Ferreteria)
                .Include(d => d.Encargado)
                .Select(d => new DepositoDto
                {
                    Id = d.Id,
                    Str_nombre = d.Str_nombre,
                    Str_direccion = d.Str_direccion,
                    Str_telefono = d.Str_telefono,
                    Bool_borrado = d.Bool_borrado,
                    FerreteriaId = d.FerreteriaId,
                    Str_ferreteriaNombre = d.Ferreteria.Str_nombre,
                    Str_ferreteriaTelefono = d.Ferreteria.Str_telefono,
                    EncargadoUsername = d.Encargado.UserName,
                    EncargadoEmail = d.Encargado.Email,
                    Movimientos = d.Movimientos.Select(m => new MovimientoDto
                    {
                        Id = m.Id,
                        Date_fecha = m.Date_fecha,
                        TipoDeMovimientoId = m.TipoDeMovimientoId,
                        DepositoOrigenId = m.DepositoOrigenId,
                        DepositoDestinoId = m.DepositoDestinoId
                    }).ToList(),
                    Productos = d.Productos.Select(p => new ProductoDto
                    {
                        Id = p.Id,
                        Str_ruta_imagen = p.Str_ruta_imagen,
                        Str_nombre = p.Str_nombre,
                        Str_descripcion = p.Str_descripcion,
                        Int_cantidad_actual = p.Int_cantidad_actual,
                        Int_cantidad_minima = p.Int_cantidad_minima,
                        Dec_costo = p.Dec_costo,
                        Dec_costo_PPP = p.Dec_costo_PPP,
                        Int_iva = p.Int_iva,
                        Dec_precio_mayorista = p.Dec_precio_mayorista,
                        Dec_precio_minorista = p.Dec_precio_minorista,
                        DetallesDeMovimientos = p.DetallesDeMovimientos.Select(d => new DetalleDeMovimientoDto
                        {
                            Id = d.Id,
                            Int_cantidad = d.Int_cantidad,
                            MovimientoId = d.MovimientoId,
                            ProductoId = d.ProductoId
                        }).ToList(),
                        DepositoId = p.DepositoId,
                        DepositoNombre = p.Deposito.Str_nombre,
                        ProveedorId = p.ProveedorId,
                        ProveedorNombre = p.Proveedor.Str_nombre,
                        MarcaId = p.MarcaId,
                        MarcaNombre = p.Marca.Str_nombre
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<DepositoDto?> GetByIdAsync(int? id)
        {
            return await _context.depositos
                .Where(d => !d.Bool_borrado)
                .Include(d => d.Movimientos)
                .Include(d => d.Productos)
                .Include(d => d.Ferreteria)
                .Include(d => d.Encargado)
                .Select(d => new DepositoDto
                {
                    Id = d.Id,
                    Str_nombre = d.Str_nombre,
                    Str_direccion = d.Str_direccion,
                    Str_telefono = d.Str_telefono,
                    Bool_borrado = d.Bool_borrado,
                    FerreteriaId = d.FerreteriaId,
                    Str_ferreteriaNombre = d.Ferreteria.Str_nombre,
                    Str_ferreteriaTelefono = d.Ferreteria.Str_telefono,
                    EncargadoUsername = d.Encargado.UserName,
                    EncargadoEmail = d.Encargado.Email,
                    Movimientos = d.Movimientos.Select(m => new MovimientoDto
                    {
                        Id = m.Id,
                        Date_fecha = m.Date_fecha,
                        TipoDeMovimientoId = m.TipoDeMovimientoId,
                        DepositoOrigenId = m.DepositoOrigenId,
                        DepositoDestinoId = m.DepositoDestinoId
                    }).ToList(),
                    Productos = d.Productos.Select(p => new ProductoDto
                    {
                        Id = p.Id,
                        Str_ruta_imagen = p.Str_ruta_imagen,
                        Str_nombre = p.Str_nombre,
                        Str_descripcion = p.Str_descripcion,
                        Int_cantidad_actual = p.Int_cantidad_actual,
                        Int_cantidad_minima = p.Int_cantidad_minima,
                        Dec_costo = p.Dec_costo,
                        Dec_costo_PPP = p.Dec_costo_PPP,
                        Int_iva = p.Int_iva,
                        Dec_precio_mayorista = p.Dec_precio_mayorista,
                        Dec_precio_minorista = p.Dec_precio_minorista,
                        DetallesDeMovimientos = p.DetallesDeMovimientos.Select(d => new DetalleDeMovimientoDto
                        {
                            Id = d.Id,
                            Int_cantidad = d.Int_cantidad,
                            MovimientoId = d.MovimientoId,
                            ProductoId = d.ProductoId
                        }).ToList(),
                        DepositoId = p.DepositoId,
                        DepositoNombre = p.Deposito.Str_nombre,
                        ProveedorId = p.ProveedorId,
                        ProveedorNombre = p.Proveedor.Str_nombre,
                        MarcaId = p.MarcaId,
                        MarcaNombre = p.Marca.Str_nombre
                    }).ToList()
                })
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Deposito?> UpdateAsync(int id, UpdateDepositoRequestDto depositoDto)
        {
            var depositoExistente = await _context.depositos
                .Where(d => !d.Bool_borrado)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (depositoExistente == null) return null;

            depositoExistente.Str_nombre = depositoDto.Str_nombre;
            depositoExistente.Str_direccion = depositoDto.Str_direccion;
            depositoExistente.Str_telefono = depositoDto.Str_telefono;
            depositoExistente.EncargadoId = depositoDto.EncargadoId;
            depositoExistente.Bool_borrado = depositoDto.Bool_borrado;

            await _context.SaveChangesAsync();
            return depositoExistente;
        }

        public async Task<Producto?> GetProductoEnDepositoAsync(int? depositoId, string producto_nombre)
        {
            return await _context.productos
                .Where(d => !d.Bool_borrado)
                .FirstOrDefaultAsync(p => p.DepositoId == depositoId && p.Str_nombre == producto_nombre);
        }
    }
}
