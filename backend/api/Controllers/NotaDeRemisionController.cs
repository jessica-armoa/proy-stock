/*using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Dtos.NotaDeRemision;
using api.Interfaces;

namespace api.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class NotaDeRemisionController : ControllerBase
  {
    private readonly INotaDeRemisionRepository _notaDeRemisionRepository;

    public NotaDeRemisionController(INotaDeRemisionRepository notaDeRemisionRepository)
    {
      _notaDeRemisionRepository = notaDeRemisionRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<NotaDeRemisionDto>>> GetNotasDeRemision()
    {
      var notas = await _notaDeRemisionRepository.GetNotasDeRemisionAsync();
      return Ok(notas);
    }
  }
}
*/