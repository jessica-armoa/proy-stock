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
        public async Task<IActionResult> GetAllAsync()
        {
            var notasDeRemision = await _notaDeRemisionRepository.GetAllAsync();
            var notasDeRemisionDto = notasDeRemision.Select(n => n.ToNotaDeRemisionDto());
            return Ok(notasDeRemisionDto);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            Console.WriteLine($"\n\n\nGET BY ID ASYNC...\n\n\n");
            var notaDeRemision = await _notaDeRemisionRepository.GetByIdAsync(id);
            if (notaDeRemision == null)
            {
                return NotFound();
            }
            return Ok(notaDeRemision.ToNotaDeRemisionDto());
        }

        [HttpGet("ultimo")]
        public async Task<IActionResult> GetUltimaNotaDeRemisionAsync()
        {
            var notaDeRemision = await _notaDeRemisionRepository.GetUltimaNotaDeRemisionAsync();
            if (notaDeRemision == null)
            {
                return NotFound();
            }
            return Ok(notaDeRemision);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateNotaDeRemisionDto notaDeRemisionDto)
        {
            try
            {
                var notaDeRemision = notaDeRemisionDto.ToNotaDeRemision();
                await _notaDeRemisionRepository.CreateAsync(notaDeRemision);
                return CreatedAtAction(nameof(GetById), new { id = notaDeRemision.Id }, notaDeRemision);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n\n\n\n\nError: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
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
        public async Task<IActionResult> GetSiguienteNumeroAsync()
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
