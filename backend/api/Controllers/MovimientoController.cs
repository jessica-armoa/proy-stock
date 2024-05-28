using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Movimiento;
using api.Interfaces;
using api.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/movimiento")]
    [ApiController]
    public class MovimientoController : ControllerBase
    {
        private readonly IMovimientoRepository _movimientoRepo;
        private readonly IDepositoRepository _depositoRepo;
        public MovimientoController(IMovimientoRepository movimientoRepo, IDepositoRepository depositoRepo)
        {
            _movimientoRepo = movimientoRepo;
            _depositoRepo = depositoRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var movimientos = await _movimientoRepo.GetAllAsync();
            var movimientosDto = movimientos.Select(m => m.ToMovimientoDto());
            return Ok(movimientosDto);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var movimiento = await _movimientoRepo.GetByIdAsync(id);
            if(movimiento == null) return NotFound();

            return Ok(movimiento.ToMovimientoDto());
        }

        [HttpPost("{tipodemovimientoId:int}/{depositoOrigen:int}/{depositoDestino:int}")]
        public async Task<IActionResult> Post([FromRoute] int tipodemovimientoId, [FromRoute] int depositoOrigen, [FromRoute] int depositoDestino, CreateMovimientoRequestDto movimientoDto)
        {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);
            
            if(!await _depositoRepo.DepositoExists(depositoOrigen))
            {
                return BadRequest("El deposito origen ingresado no existe!!");
            }

            if(!await _depositoRepo.DepositoExists(depositoDestino))
            {
                return BadRequest("El deposito destino ingresado no existe!!");
            }

            if(depositoOrigen == depositoDestino)
            {
                return BadRequest("Los depositos origen y destino no pueden ser iguales!!");
            }

            var movimientoModel = movimientoDto.ToMovimientoFromCreate(tipodemovimientoId, depositoOrigen, depositoDestino);
            await _movimientoRepo.CreateAsync(movimientoModel);
            return CreatedAtAction(nameof(GetById), new{id = movimientoModel.Id}, movimientoModel.ToMovimientoDto());
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, UpdateMovimientoRequestDto movimientoDto)
        {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);

            var movimiento = await _movimientoRepo.UpdateAsync(id, movimientoDto.ToMovimientoFromUpdate());
            if(movimiento == null) return NotFound("El movimiento que desea modificar no existe!!");

            return Ok(movimiento.ToMovimientoDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);
            
            var movimientoModel = await _movimientoRepo.DeleteAsync(id);
            if(movimientoModel == null)
            {
                return NotFound("El movimiento que desea eliminar no existe!!");
            }

            return Ok(movimientoModel);
        }
    }
}