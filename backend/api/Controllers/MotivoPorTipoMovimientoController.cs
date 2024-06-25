using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/motivo-por-tipo-de-movimiento")]
    [ApiController]
    public class MotivoPorTipoMovimientoController : ControllerBase
    {
        private readonly IMotivoPorTipoDeMovimientoRepository _motivoPorTipoMovimientoRepo;
        public MotivoPorTipoMovimientoController(IMotivoPorTipoDeMovimientoRepository motivoPorTipoMovimientoRepo)
        {
            _motivoPorTipoMovimientoRepo = motivoPorTipoMovimientoRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var motivosPorTipoDeMovimiento = await _motivoPorTipoMovimientoRepo.GetAllAsync();
            var motivosPorTipoDeMovimientoDto = motivosPorTipoDeMovimiento.Select(m => m.ToMotivoPorTipoDeMovimientoDto());
            return Ok(motivosPorTipoDeMovimientoDto);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var motivoPorTipoMovimiento = await _motivoPorTipoMovimientoRepo.GetByIdAsync(id);
            if(motivoPorTipoMovimiento == null) return NotFound();

            return Ok(motivoPorTipoMovimiento.ToMotivoPorTipoDeMovimientoDto());
        }
    }
}