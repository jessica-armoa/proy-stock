using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.NotaDeRemision;
using api.Dtos.DetalleDeMovimiento;
using api.Interfaces;
using api.Data;
using Microsoft.EntityFrameworkCore;
using api.Models;

namespace api.Repository
{
  public class NotaDeRemisionRepository : INotaDeRemisionRepository
  {
    private readonly ApplicationDbContext _context;

    public NotaDeRemisionRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<NotaDeRemision?> GetByIdAsync(int? id)
    {
        return await _context.notas_de_remision
            .Include(n => n.Movimiento)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<List<NotaDeRemision>> GetAllAsync()
    {
        return await _context.notas_de_remision
          .Include(n => n.Movimiento)
          .ToListAsync();
    }
  }
}
