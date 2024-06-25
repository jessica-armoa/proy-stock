using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;

namespace api.Repository
{
    public class AsientoRepository : IAsientoRepository
    {
        private readonly ApplicationDbContext _context;
        public AsientoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Asiento> CreateAsync(Asiento asientoModel)
        {
            await _context.asientos.AddAsync(asientoModel);
            await _context.SaveChangesAsync();
            return asientoModel;
        }
    }
}