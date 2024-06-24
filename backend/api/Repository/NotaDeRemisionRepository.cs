using System;
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
      // Buscar el movimiento correspondiente al MovimientoId proporcionado
      if (notaDeRemision.MovimientoId.HasValue)
      {
        var movimiento = await _context.movimientos
            .FirstOrDefaultAsync(m => m.Id == notaDeRemision.MovimientoId.Value);

        if (movimiento != null)
        {
          notaDeRemision.Movimiento = movimiento;
        }
        else
        {
          throw new Exception("No se encontró un movimiento adecuado para asignar.");
        }
      }

      // Buscar el timbrado correspondiente al TimbradoId proporcionado
      var timbrado = await _context.timbrados
          .FirstOrDefaultAsync(t => t.Id == notaDeRemision.TimbradoId);

      if (timbrado != null)
      {
        notaDeRemision.Timbrado = timbrado;
      }
      else
      {
        throw new Exception("No se encontró el timbrado");
      }

      // Agregar la nota de remisión al contexto y guardar los cambios
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
      // Buscar el movimiento correspondiente al MovimientoId proporcionado
      if (notaDeRemision.MovimientoId.HasValue)
      {
        var movimiento = await _context.movimientos
            .FirstOrDefaultAsync(m => m.Id == notaDeRemision.MovimientoId.Value);

        if (movimiento != null)
        {
          notaDeRemision.Movimiento = movimiento;
        }
        else
        {
          throw new Exception("No se encontró el movimiento");
        }
      }

      // Buscar el timbrado correspondiente al TimbradoId proporcionado
      var timbrado = await _context.timbrados
          .FirstOrDefaultAsync(t => t.Id == notaDeRemision.TimbradoId);

      if (timbrado != null)
      {
        notaDeRemision.Timbrado = timbrado;
      }
      else
      {
        throw new Exception("No se encontró el timbrado");
      }

      _context.Entry(notaDeRemision).State = EntityState.Modified;
      await _context.SaveChangesAsync();
    }

    public async Task<string> GetSiguienteNumeroAsync()
    {
      var lastNotaDeRemision = await _context.notas_de_remision
          .OrderByDescending(n => n.Id)
          .FirstOrDefaultAsync();

      int lastNumero = 0;
      if (lastNotaDeRemision != null && int.TryParse(lastNotaDeRemision.Str_numero, out lastNumero))
      {
        lastNumero++;
      }
      else
      {
        lastNumero = 1;
      }

      return lastNumero.ToString("D7"); // Formatea el número con siete dígitos
    }
  }
}
