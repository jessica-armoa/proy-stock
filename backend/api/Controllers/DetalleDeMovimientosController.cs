using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.DetalleDeMovimiento;
using api.Dtos.Producto;
using api.Interfaces;
using api.Mapper;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/detalle")]
    [ApiController]
    public class DetalleDeMovimientosController : ControllerBase
    {
        private readonly IDetalleDeMovimientosRepository _detalleRepo;
        public DetalleDeMovimientosController(IDetalleDeMovimientosRepository detalleRepo)
        {
            _detalleRepo = detalleRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var detalles = await _detalleRepo.GetAllAsync();
            var detallesDto = detalles.Select(d => d.ToDetalleDeMovimientoDto());
            return Ok(detalles);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var detalle = await _detalleRepo.GetByIdAsync(id);
            if (detalle == null) return NotFound();

            return Ok(detalle.ToDetalleDeMovimientoDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateDetalleRequestDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var detalleModel = await _detalleRepo.UpdateAsync(id, updateDto.ToDetalleFromUpdate());
            if (detalleModel == null) return NotFound();

            return Ok(detalleModel.ToDetalleDeMovimientoDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var detalleModel = await _detalleRepo.DeleteAsync(id);
            if(detalleModel == null)
            {
                return NotFound();
            }

            return Ok(detalleModel.ToDetalleDeMovimientoDto());
        }
    }
}