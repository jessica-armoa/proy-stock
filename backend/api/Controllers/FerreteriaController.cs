using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Ferreteria;
using api.Interfaces;
using api.Mapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/ferreteria")]
    [ApiController]
    public class FerreteriaController : ControllerBase
    {
        private readonly IFerreteriaRepository _ferreteriaRepo;
        public FerreteriaController(IFerreteriaRepository ferreteriaRepo, ApplicationDbContext context)
        {
            _ferreteriaRepo = ferreteriaRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ferreterias = await _ferreteriaRepo.GetAllAsync();
            var ferreteriaDto = ferreterias.Select(f => f.ToFerreteriaDto());
            return Ok(ferreteriaDto);
        } 

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);

            var ferreteria = await _ferreteriaRepo.GetByIdAsync(id);
            if(ferreteria == null) return NotFound();

            return Ok(ferreteria);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateFerreteriaRequestDto ferreteriaDto)
        {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);

            var ferreteriaModel = ferreteriaDto.ToFerreteriaFromCreate();
            await _ferreteriaRepo.CreateAsync(ferreteriaModel);
            return CreatedAtAction(nameof(GetById), new{id = ferreteriaModel.Id}, ferreteriaModel.ToFerreteriaDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateFerreteriaRequestDto updateDto)
        {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);

            var ferreteriaModel = await _ferreteriaRepo.UpdateAsync(id, updateDto.ToFerreteriaFromUpdate());
            if(ferreteriaModel == null) return NotFound();

            return Ok(ferreteriaModel.ToFerreteriaDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);
                
                var ferreteriaModel = await _ferreteriaRepo.DeleteAsync(id);
                if (ferreteriaModel == null)
                {
                    return NotFound("La ferreteria que desea eliminar no existe!!");
                }

                return Ok(ferreteriaModel);
        }
    }
}