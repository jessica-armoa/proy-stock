using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/motivo")]
    [ApiController]
    public class MotivoController : ControllerBase
    {
        private readonly IMotivoRepository _motivoRepo;
        public MotivoController(IMotivoRepository motivoRepo)
        {
            _motivoRepo = motivoRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var motivos = await _motivoRepo.GetAllAsync();
            var motivosDto = motivos.Select(m => m.ToMotivoDto());
            return Ok(motivosDto);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var motivo = await _motivoRepo.GetByIdAsync(id);
            if (motivo == null) return NotFound();

            return Ok(motivo.ToMotivoDto());
        }
    }
}