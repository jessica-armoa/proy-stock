using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

public class TimbradoRepository : ITimbradoRepository
{
  private readonly ApplicationDbContext _context;

  public TimbradoRepository(ApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<List<Timbrado>> GetAllAsync()
  {
    return await _context.timbrados.ToListAsync();
  }

  public async Task<Timbrado?> GetByIdAsync(int id)
  {
    return await _context.timbrados.FindAsync(id);
  }

  public async Task<Timbrado?> GetTimbradoActivoAsync()
  {
    return await _context.timbrados
        .Where(t => t.Date_inicio_vigencia <= DateTime.Now && t.Date_fin_vigencia >= DateTime.Now)
        .FirstOrDefaultAsync();
  }

  public async Task CreateAsync(Timbrado timbrado)
  {
    _context.timbrados.Add(timbrado);
    await _context.SaveChangesAsync();
  }

}