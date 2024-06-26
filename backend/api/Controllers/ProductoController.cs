using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Producto;
using api.Interfaces;
using api.Mapper;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/producto")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoRepository _productoRepo;
        private readonly IDepositoRepository _depositoRepo;
        private readonly IProveedorRepository _proveedorRepo;
        private readonly IDetalleDeMovimientosRepository _detalleRepo;
        private readonly IMarcaRepository _marcaRepo;
        public ProductoController(
            IProductoRepository productoRepo,
            IDepositoRepository depositoRepo,
            IProveedorRepository proveedorRepo,
            IDetalleDeMovimientosRepository detalleRepo,
            IMarcaRepository marcaRepo)
        {
            _productoRepo = productoRepo;
            _depositoRepo = depositoRepo;
            _proveedorRepo = proveedorRepo;
            _detalleRepo = detalleRepo;
            _marcaRepo = marcaRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productos = await _productoRepo.GetAllAsync();
            var productosDto = productos.Select(p => p.ToProductoDto());
            return Ok(productosDto);
        }

        [HttpGet]
        [Route("deposito/{depositoId:int}")]
        public async Task<IActionResult> ObtenerProductosPorDeposito(int depositoId)
        {
            if (!await _depositoRepo.DepositoExists(depositoId))
            {
                return NotFound("Deposito ingresado no existe!!");
            }
            var productos = await _productoRepo.ObtenerProductosPorDepositoAsync(depositoId);
            var productosDto = productos.Select(p => p.ToProductoDto());
            return Ok(productosDto);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var producto = await _productoRepo.GetByIdAsync(id);
                if (producto == null)
                {
                    return NotFound("El producto no existe!!");
                }
                return Ok(producto.ToProductoDto());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{depositoId:int}/{proveedorId:int}/{marcaId:int}")]
        public async Task<IActionResult> Post([FromRoute] int depositoId, [FromRoute] int proveedorId, [FromRoute] int marcaId, CreateProductoRequestDto productoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _depositoRepo.DepositoExists(depositoId))
            {
                return BadRequest("El deposito ingresado no existe!");
            }

            if (!await _proveedorRepo.ProveedorExists(proveedorId))
            {
                return BadRequest("El proveedor ingresado no existe!");
            }

            if (!await _marcaRepo.MarcaExists(marcaId))
            {
                return BadRequest("La marca ingresada no existe!");
            }

            if (await _productoRepo.ProductoExistsName(productoDto.Str_nombre))
            {
                return BadRequest("El producto que desea ingresar ya existe!!");
            }
            var productoModel = productoDto.ToProductoFromCreate(depositoId, proveedorId, marcaId);
            await _productoRepo.CreateAsync(productoModel);

            var depositos = await _depositoRepo.GetAllAsync();
            if (depositos != null)
            {
                foreach (var deposito in depositos)
                {
                    if (deposito.Id != depositoId)
                    {
                        if (!deposito.Productos.Any(d => d.Str_nombre != productoDto.Str_nombre))
                        {
                            var productoEnDepositos = productoDto.ToProductoFromCreate(deposito.Id, proveedorId, marcaId);
                            await _productoRepo.CreateAsync(productoEnDepositos);
                        }
                    }
                }
            }
            else
            {
                return BadRequest("No existen depositos creados");
            }

            return CreatedAtAction(nameof(GetById), new { id = productoModel.Id }, productoModel.ToProductoDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProductoRequestDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var producto = await _productoRepo.UpdateAsync(id, updateDto.ToProductoFromUpdate());
            if (producto == null)
            {
                return NotFound("El producto que desea actualizar no existe");
            }

            return Ok(producto.ToProductoDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productoExistente = await _productoRepo.GetByIdAsync(id);
            if (productoExistente == null)
            {
                return NotFound("El producto que desea eliminar no existe!!");
            }

            foreach (var detalle in productoExistente.DetallesDeMovimientos)
            {
                await _detalleRepo.DeleteAsync(detalle.Id);
            };
            var productoModel = await _productoRepo.DeleteAsync(id);

            return Ok($"Se borró correctamente el producto: {productoModel.Str_nombre}"); //No es necesario traer algo, puede ser vacio
        }

        [HttpPost("actualizar-costo-ppp")]
        public async Task<IActionResult> ActualizarCostoPPP()
        {
            // Obtener todos los productos
            try
            {
                await _productoRepo.ActualizarCostoPPPAsync();
                return Ok("Costo PPP actualizado correctamente!!");
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
