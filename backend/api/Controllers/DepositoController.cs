using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Deposito;
using api.Interfaces;
using api.Mapper;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/deposito")]
    [ApiController]
    public class DepositoController : ControllerBase
    {
        private readonly IDepositoRepository _depositoRepo;
        private readonly IFerreteriaRepository _ferreteriaRepo;
        private readonly IProductoRepository _productoRepo;
        private readonly IDetalleDeMovimientosRepository _detalleRepo;
        private readonly UserManager<Usuarios> _userManager;
        private readonly ApplicationDbContext _context;

        public DepositoController(IDepositoRepository depositoRepo, IFerreteriaRepository ferreteriaRepo, IProductoRepository productoRepo, IDetalleDeMovimientosRepository detalleRepo, UserManager<Usuarios> userManager, ApplicationDbContext context)
        {
            _depositoRepo = depositoRepo;
            _ferreteriaRepo = ferreteriaRepo;
            _productoRepo = productoRepo;
            _detalleRepo = detalleRepo;
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var depositos = await _depositoRepo.GetAllAsync();
            var depositosDto = depositos.Select(d => d.ToOnlyDepositoDto());
            return Ok(depositosDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var deposito = await _depositoRepo.GetByIdAsync(id);
            if (deposito == null) return NotFound();

            return Ok(deposito.ToDepositoDto());
        }

        [HttpPost("{ferreteriaId:int}")]
        public async Task<IActionResult> Post([FromRoute] int ferreteriaId, CreateDepositoRequestDto depositoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _ferreteriaRepo.FerreteriaExists(ferreteriaId))
            {
                return BadRequest("La ferreteria ingresada no existe!");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                Usuarios encargado = null;

                if (!string.IsNullOrEmpty(depositoDto.EncargadoUsername))
                {
                    encargado = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == depositoDto.EncargadoUsername);
                    if (encargado == null)
                    {
                        return BadRequest("El usuario encargado no existe.");
                    }
                }

                var depositoModel = depositoDto.ToDepositoFromCreate(ferreteriaId, encargado?.UserName, encargado?.Id ?? null);
                await _depositoRepo.CreateAsync(depositoModel);
                await transaction.CommitAsync();

                var depositoDtoResult = depositoModel.ToDepositoDto();
                return CreatedAtAction(nameof(GetById), new { id = depositoModel.Id }, depositoDtoResult);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, UpdateDepositoRequestDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Usuarios encargado = null;

            if (!string.IsNullOrEmpty(updateDto.EncargadoUsername))
            {
                encargado = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == updateDto.EncargadoUsername);
                if (encargado == null)
                {
                    return BadRequest("El usuario encargado no existe.");
                }
            }

            var depositoExistente = await _depositoRepo.GetByIdAsync(id);
            if (depositoExistente == null) return NotFound("El deposito que desea actualizar no existe!!");

            depositoExistente.Str_nombre = updateDto.Str_nombre;
            depositoExistente.Str_direccion = updateDto.Str_direccion;
            depositoExistente.Str_telefono = updateDto.Str_telefono;
            depositoExistente.EncargadoId = encargado?.Id ?? depositoExistente.EncargadoId;
            depositoExistente.Bool_borrado = false;

            await _context.SaveChangesAsync();

            return Ok(depositoExistente.ToDepositoDto());
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var depositoExistente = await _depositoRepo.GetByIdAsync(id);
            if (depositoExistente == null)
            {
                return NotFound("El deposito que desea eliminar no existe!!");
            }

            foreach (var producto in depositoExistente.Productos)
            {
                foreach (var detalle in producto.DetallesDeMovimientos)
                {
                    await _detalleRepo.DeleteAsync(detalle.Id);
                };
                await _productoRepo.DeleteAsync(producto.Id);
            };
            var depositoModel = await _depositoRepo.DeleteAsync(id);
            if (depositoModel == null)
            {
                return NotFound("El deposito que desea eliminar no existe!!");
            }

            return Ok(depositoModel.ToDepositoDto());
        }
    }
}
