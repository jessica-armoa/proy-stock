/*using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.NotaDeRemision;
using api.Dtos.DetalleDeMovimiento;
using api.Interfaces;
using api.Data;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
  public class NotaDeRemisionRepository : INotaDeRemisionRepository
  {
    private readonly ApplicationDbContext _context;

    public NotaDeRemisionRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<NotaDeRemisionDto>> GetNotasDeRemisionAsync()
    {
      var notas = await _context.NotasDeRemision
          .Include(n => n.DetallesDeMovimientos)
          .ToListAsync();

      // Mapear entidades a DTOs
      var notasDto = notas.Select(n => new NotaDeRemisionDto
      {
        Id = n.Id,
        Date_fecha = n.Date_fecha,
        TipoDeMovimientoId = n.TipoDeMovimientoId,
        DepositoOrigenId = n.DepositoOrigenId,
        DepositoDestinoId = n.DepositoDestinoId,
        DetallesDeMovimientos = n.DetallesDeMovimientos.Select(d => new DetalleDeMovimientoDto
        {
          // Mapear propiedades del detalle
        }).ToList()
      }).ToList();

      return notasDto;
    }
  }
}*/
