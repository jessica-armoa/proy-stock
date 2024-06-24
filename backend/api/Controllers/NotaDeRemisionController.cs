using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Dtos.NotaDeRemision;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
  [Route("api/notas-de-remision")]
  [ApiController]
  public class NotaDeRemisionController : ControllerBase
  {
    private readonly INotaDeRemisionRepository _notaDeRemisionRepository;

    public NotaDeRemisionController(INotaDeRemisionRepository notaDeRemisionRepository)
    {
      _notaDeRemisionRepository = notaDeRemisionRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<NotaDeRemision>>> GetAllAsync()
    {
      var notasDeRemision = await _notaDeRemisionRepository.GetAllAsync();
      return Ok(notasDeRemision);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<NotaDeRemision>> GetByIdAsync(int? id)
    {
      var notaDeRemision = await _notaDeRemisionRepository.GetByIdAsync(id);
      if (notaDeRemision == null)
      {
        return NotFound();
      }
      return Ok(notaDeRemision);
    }

    [HttpGet("ultimo")]
    public async Task<ActionResult<NotaDeRemision>> GetUltimaNotaDeRemisionAsync()
    {
      var notaDeRemision = await _notaDeRemisionRepository.GetUltimaNotaDeRemisionAsync();
      if (notaDeRemision == null)
      {
        return NotFound();
      }
      return Ok(notaDeRemision);
    }

    [HttpPost]
    public async Task<ActionResult<NotaDeRemision>> CreateAsync(CreateNotaDeRemisionDto notaDeRemisionDto)
    {
      try
      {
        // Crear la entidad NotaDeRemision a partir del DTO
        var notaDeRemision = new NotaDeRemision
        {
          Str_numero = notaDeRemisionDto.Str_numero,
          TimbradoId = notaDeRemisionDto.TimbradoId,
          MovimientoId = notaDeRemisionDto.MovimientoId,
          Date_fecha_de_expedicion = notaDeRemisionDto.Date_fecha_de_expedicion,
          Date_fecha_de_vencimiento = notaDeRemisionDto.Date_fecha_de_vencimiento,
          EmpresaNombre = notaDeRemisionDto.EmpresaNombre,
          EmpresaDireccion = notaDeRemisionDto.EmpresaDireccion,
          EmpresaTelefono = notaDeRemisionDto.EmpresaTelefono,
          EmpresaSucursal = notaDeRemisionDto.EmpresaSucursal,
          EmpresaActividad = notaDeRemisionDto.EmpresaActividad,
          Ruc = notaDeRemisionDto.Ruc,
          DestinatarioNombre = notaDeRemisionDto.DestinatarioNombre,
          DestinatarioDocumento = notaDeRemisionDto.DestinatarioDocumento,
          PuntoPartida = notaDeRemisionDto.PuntoPartida,
          PuntoLlegada = notaDeRemisionDto.PuntoLlegada,
          TrasladoFechaInicio = notaDeRemisionDto.TrasladoFechaInicio,
          TrasladoFechaFin = notaDeRemisionDto.TrasladoFechaFin,
          Motivo = notaDeRemisionDto.Motivo,
          MotivoDescripcion = notaDeRemisionDto.MotivoDescripcion,
          ComprobanteVenta = notaDeRemisionDto.ComprobanteVenta,
        };

        // Llamar al repositorio para crear la nota de remisión
        await _notaDeRemisionRepository.CreateAsync(notaDeRemision);

        // Devolver la nota de remisión creada
        return CreatedAtAction(nameof(GetByIdAsync), new { id = notaDeRemision.Id }, notaDeRemision);
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"Error interno del servidor: {ex.Message}");
      }
    }



    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, NotaDeRemision notaDeRemision)
    {
      if (id != notaDeRemision.Id)
      {
        return BadRequest();
      }

      try
      {
        await _notaDeRemisionRepository.UpdateAsync(notaDeRemision);
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"Error interno del servidor: {ex.Message}");
      }

      return NoContent();
    }

    [HttpGet("getSiguienteNumero")]
    public async Task<ActionResult<int>> GetSiguienteNumeroAsync()
    {
      try
      {
        var nextNumber = await _notaDeRemisionRepository.GetSiguienteNumeroAsync();
        return Ok(nextNumber);
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"Error interno del servidor: {ex.Message}");
      }
    }
  }
}
