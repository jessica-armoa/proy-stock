using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Dtos.NotaDeRemision;
using api.Interfaces;
using api.Models;
using api.Mapper;
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
                var notaDeRemision = notaDeRemisionDto.ToNotaDeRemision();
                await _notaDeRemisionRepository.CreateAsync(notaDeRemision);

                return CreatedAtAction(nameof(GetByIdAsync), new { id = notaDeRemision.Id }, notaDeRemision);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, NotaDeRemisionDto notaDeRemisionDto)
        {
            if (id != notaDeRemisionDto.Id)
            {
                return BadRequest();
            }

            try
            {
                var notaDeRemision = notaDeRemisionDto.ToNotaDeRemision();
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
