using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Interfaces
{
  public class NotaDeRemisionRepository : INotaDeRemisionRepository
  {
    private readonly ApplicationDbContext _context;

    public NotaDeRemisionRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task CreateAsync(NotaDeRemision notaDeRemision)
    {
      _context.notas_de_remision.Add(notaDeRemision);
      await _context.SaveChangesAsync();
    }

    public async Task<List<NotaDeRemision>> GetAllAsync()
    {
      return await _context.notas_de_remision.ToListAsync();
    }

    public async Task<NotaDeRemision?> GetByIdAsync(int? id)
    {
      return await _context.notas_de_remision.FirstOrDefaultAsync(n => n.Id == id);
    }

    public async Task<NotaDeRemision?> GetUltimaNotaDeRemisionAsync()
    {
      return await _context.notas_de_remision.OrderByDescending(n => n.Id).FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(NotaDeRemision notaDeRemision)
    {
      _context.Entry(notaDeRemision).State = EntityState.Modified;
      await _context.SaveChangesAsync();
    }
  }
}
