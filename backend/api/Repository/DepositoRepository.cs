using api.Data;
using api.Dtos.Deposito;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;


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

        public async Task<List<Deposito>> GetAllAsync()
        {
            return await _context.depositos
            .Where(d => d.Bool_borrado != true)
            .Include(d => d.Movimientos)
            .Include(d => d.Productos).ThenInclude(d => d.Proveedor)
            .Include(d => d.Productos).ThenInclude(d => d.Marca)
            .Include(d => d.Ferreteria)
            .Include(d => d.Encargado)
            .ToListAsync();
        }

        public async Task<Deposito?> GetByIdAsync(int? id)
        {
            return await _context.depositos
            .Where(d => d.Bool_borrado != true)
            .Include(d => d.Movimientos)
            .Include(d => d.Productos).ThenInclude(d => d.Proveedor)
            .Include(d => d.Productos).ThenInclude(d => d.Marca)
            .Include(d => d.Ferreteria)
            .Include(d => d.Encargado)
            .FirstOrDefaultAsync(d => d.Id == id);
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
