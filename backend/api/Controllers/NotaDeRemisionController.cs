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
        private readonly IMovimientoRepository _movimientoRepo;

        public NotaDeRemisionController(INotaDeRemisionRepository notaDeRemisionRepository, IMovimientoRepository movimientoRepo)
        {
            _notaDeRemisionRepository = notaDeRemisionRepository;
            _movimientoRepo = movimientoRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<NotaDeRemision>>> GetAllAsync()
        {
            var notasDeRemision = await _notaDeRemisionRepository.GetAllAsync();
            var notasDeRemisionDto = notasDeRemision.Select(n => n.ToNotaDeRemisionDto());
            return Ok(notasDeRemisionDto);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<NotaDeRemision>> GetById([FromRoute] int id)
        {
            var notaDeRemision = await _notaDeRemisionRepository.GetByIdAsync(id);
            if (notaDeRemision == null)
            {
                return NotFound();
            }
            return Ok(notaDeRemision.ToNotaDeRemisionDto());
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
        [Route("{timbradoId:int}/{movimientoId}")]
        public async Task<ActionResult<NotaDeRemision>> Create([FromBody] CreateNotaDeRemisionDto notaDeRemisionDto, [FromRoute] int timbradoId, [FromRoute] int movimientoId)
        {
            try
            {
                if(!await _movimientoRepo.MovimientoExists(movimientoId))
                {
                    return NotFound("Movimiento ingresado no existe!!");
                }
                /*if(!await _timbradoRepo.TimbradoExists(timbradoId))
                {
                    return NotFound("Timbrado ingresado no existe!!");
                }*/

                var notaDeRemision = notaDeRemisionDto.ToNotaDeRemisionFromCreate(timbradoId, movimientoId);
                await _notaDeRemisionRepository.CreateAsync(notaDeRemision);

                return CreatedAtAction(nameof(GetById), new { id = notaDeRemision.Id }, notaDeRemision.ToNotaDeRemisionDto());
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
        public async Task<ActionResult<string>> GetSiguienteNumeroAsync()
        {
            try
            {
                var siguienteNumero = await _notaDeRemisionRepository.GetSiguienteNumeroAsync();
                return Ok(siguienteNumero);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
