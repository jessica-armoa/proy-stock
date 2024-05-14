using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var depositoModel = await _context.depositos.FirstOrDefaultAsync(d => d.Id == id);
            if(depositoModel == null)
            {
                return null;
            }

            _context.depositos.Remove(depositoModel);
            await _context.SaveChangesAsync();
            return depositoModel;
        }

        public async Task<bool> DepositoExists(int id)
        {
            return await _context.depositos.AnyAsync(d => d.Id == id);
        }

        public async Task<List<Deposito>> GetAllAsync()
        {
            return await _context.depositos
            .Include(d => d.Movimientos)
            .Include(d => d.Productos)
            .ToListAsync();
        }

        public async Task<Deposito?> GetByIdAsync(int id)
        {
            return await _context.depositos
            .Include(d => d.Movimientos)
            .Include(d => d.Productos)
            .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Deposito?> UpdateAsync(int id, UpdateDepositoRequestDto depositoDto)
        {
            var depositoExistente = await _context.depositos.FirstOrDefaultAsync(d => d.Id == id);
            if(depositoExistente == null) return null;

            depositoExistente.Str_nombre = depositoDto.Str_nombre;
            depositoExistente.Str_direccion = depositoDto.Str_direccion;

            await _context.SaveChangesAsync();
            return depositoExistente;
        }
    }
}