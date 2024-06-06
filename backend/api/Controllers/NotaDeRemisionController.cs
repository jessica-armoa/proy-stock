using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Dtos.NotaDeRemision;
using api.Interfaces;
using api.Mapper;

namespace api.Controllers
{
  [ApiController]
  [Route("api/nota")]
  public class NotaDeRemisionController : ControllerBase
  {
    private readonly INotaDeRemisionRepository _notaDeRemisionRepository;

    public NotaDeRemisionController(INotaDeRemisionRepository notaDeRemisionRepository)
    {
      _notaDeRemisionRepository = notaDeRemisionRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var notas = await _notaDeRemisionRepository.GetAllAsync();
      var notasDto = notas.Select(n => n.ToNotaDeRemisionDto());
      return Ok(notasDto);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      try
      {
        var nota = await _notaDeRemisionRepository.GetByIdAsync(id);
        if (nota == null)
        {
          return NotFound("La nota de remisión no existe");
        }
        return Ok(nota.ToNotaDeRemisionDto());
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }
  }
}
