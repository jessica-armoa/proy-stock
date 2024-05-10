using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Proveedor;
using api.Interfaces;
using api.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/proveedor")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly IProveedorRepository _proveedorRepo;
        public ProveedorController(IProveedorRepository proveedorRepo)
        {
            _proveedorRepo = proveedorRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var proveedores = await _proveedorRepo.GetAllAsync();
            var proveedoresDto = proveedores.Select(p => p.ToProveedorDto());
            return Ok(proveedoresDto);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        { 
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);
 
                var stock = await _proveedorRepo.GetByIdAsync(id);
                if(stock == null) return NotFound();
                return Ok(stock.ToProveedorDto());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProveedorRequestDto proveedorDto)
        {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);

                var proveedorModel = proveedorDto.ToProveedorFromCreate();
                await _proveedorRepo.CreateAsync(proveedorModel);
                return CreatedAtAction(nameof(GetById), new {id = proveedorModel.Id}, proveedorModel.ToProveedorDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProveedorRequestDto updateDto)
        {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);
                
            var proveedor = await _proveedorRepo.UpdateAsync(id, updateDto.ToDepositoFromUpdate());
            if(proveedor == null) return NotFound("El comentario que desea actualizar no existe");

            return Ok(proveedor.ToProveedorDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);

            var productoModel = await _proveedorRepo.DeleteAsync(id);
            if(productoModel == null) return NotFound("El producto que desea eliminar no existe!!");
            
            return Ok(productoModel); //No es necesario traer algo, puede ser vacio
        }
    }
}