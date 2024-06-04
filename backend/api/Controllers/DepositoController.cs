using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Deposito;
using api.Interfaces;
using api.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/deposito")]
    [ApiController]
    public class DepositoController : ControllerBase
    {
        private readonly IDepositoRepository _depositoRepo;
        private readonly IFerreteriaRepository _ferreteriaRepo;
        public DepositoController(IDepositoRepository depositoRepo, IFerreteriaRepository ferreteriaRepo)
        {
            _depositoRepo = depositoRepo;
            _ferreteriaRepo = ferreteriaRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var depositos = await _depositoRepo.GetAllAsync();
            return Ok(depositos);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            /*
                if(!ModelState.IsValid) 
                return BadRequest(ModelState);
            */
            var deposito = await _depositoRepo.GetByIdAsync(id);
            if(deposito == null) return NotFound();

            return Ok(deposito.ToDepositoDto());
        }

        [HttpPost]
        [Route("{ferreteriaId:int}")]
        public async Task<IActionResult> Post([FromRoute] int ferreteriaId, CreateDepositoRequestDto depositoDto)
        {
            /*
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);
            */
            if(!await _ferreteriaRepo.FerreteriaExists(ferreteriaId))
            {
                return BadRequest("La ferreteria ingresada no existe!");
            }
        
            var depositoModel = depositoDto.ToDepositoFromCreate(ferreteriaId);
            await _depositoRepo.CreateAsync(depositoModel);
            return CreatedAtAction(nameof(GetById), new{id = depositoModel.Id}, depositoModel.ToDepositoDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, UpdateDepositoRequestDto updateDto)
        {
            /*
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);
            */
            var deposito = await _depositoRepo.UpdateAsync(id, updateDto);
            if(deposito == null) return NotFound("El deposito que desea actualizar no existe!!");

            return Ok(deposito.ToDepositoDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            /*
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);
            */
            var depositoModel = await _depositoRepo.DeleteAsync(id);
            if(depositoModel == null) return NotFound("El deposito que desea eliminar no existe!!");

            return Ok(depositoModel);
        }
    }
}