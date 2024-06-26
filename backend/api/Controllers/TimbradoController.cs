using api.Dtos.Timbrado;
using api.Interfaces;
using api.Mapper;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace api.Controllers
{
  [ApiController]
  [Route("api/timbrado")]
  public class TimbradoController : ControllerBase
  {
    private readonly ITimbradoRepository _timbradoRepo;

    public TimbradoController(ITimbradoRepository timbradoRepo)
    {
      _timbradoRepo = timbradoRepo;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTimbrado([FromBody] CreateTimbradoDto createTimbradoDto)
    {
      if (createTimbradoDto == null)
      {
        return BadRequest("Invalid data.");
      }

      var timbrado = createTimbradoDto.ToTimbrado();
      await _timbradoRepo.CreateAsync(timbrado);
      return CreatedAtAction(nameof(GetById), new { id = timbrado.Id }, timbrado);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
      var timbrado = await _timbradoRepo.GetByIdAsync(id);
      if (timbrado == null)
      {
        return NotFound();
      }
      return Ok(timbrado.ToTimbradoDto());
    }

    [HttpGet]
    [Route("activo")]
    public async Task<IActionResult> GetTimbradoActivo()
    {
      var timbrado = await _timbradoRepo.GetTimbradoActivoAsync();
      if (timbrado == null)
      {
        return NotFound();
      }
      return Ok(timbrado.ToTimbradoDto());
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var timbrados = await _timbradoRepo.GetAllAsync();
      return Ok(timbrados.Select(t => t.ToTimbradoDto()).ToList());
    }
  }
}
