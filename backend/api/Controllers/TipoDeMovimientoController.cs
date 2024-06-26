using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/tipo-de-movimiento")]
    [ApiController]
    public class TipoDeMovimientoController : ControllerBase
    {
        private readonly ITipoDeMovimientoRepository _tipoDeMovimientoRepo;
        public TipoDeMovimientoController(ITipoDeMovimientoRepository tipoDeMovimientoRepo)
        {
            _tipoDeMovimientoRepo = tipoDeMovimientoRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tiposDeMovimientos = await _tipoDeMovimientoRepo.GetAllAsync();
            var tiposDeMovimientosDto = tiposDeMovimientos.Select(t => t.ToTipoDeMovimientoDto());
            return Ok(tiposDeMovimientosDto);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var tipoDeMovimiento = await _tipoDeMovimientoRepo.GetByIdAsync(id);
            if(tipoDeMovimiento == null) return NotFound();

            return Ok(tipoDeMovimiento.ToTipoDeMovimientoDto());
        }
    }


}