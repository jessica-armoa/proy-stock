using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Proveedor;
using api.Models;

namespace api.Mapper
{
    public static class ProveedorMapper
    {
        public static ProveedorDto ToProveedorDto(this Proveedor proveedorModel)
        {
            return new ProveedorDto
            {
                Id = proveedorModel.Id,
                Str_nombre = proveedorModel.Str_nombre,
                Str_telefono = proveedorModel.Str_telefono,
                Str_direccion = proveedorModel.Str_direccion,
                Str_correo = proveedorModel.Str_correo,
                Bool_borrado = proveedorModel.Bool_borrado,
                Productos = proveedorModel.Productos
                    .Where(p => p.Bool_borrado != true)
                    .Select(p => p.ToProductoDto()).ToList(),
                Categorias = proveedorModel.Categorias
                    .Select(c => c.ToCategoriaDto()).ToList(),
                Marcas = proveedorModel.Marcas
                    .Where(m => m.Bool_borrado != true)
                    .Select(m => m.ToMarcaDto()).ToList()
            };
        }

        public static Proveedor ToProveedorFromCreate(this CreateProveedorRequestDto proveedorDto)
        {
            return new Proveedor
            {
                Str_nombre = proveedorDto.Str_nombre,
                Str_telefono = proveedorDto.Str_telefono,
                Str_direccion = proveedorDto.Str_direccion,
                Str_correo = proveedorDto.Str_correo,
                Bool_borrado = false
            };
        }

        public static Proveedor ToDepositoFromUpdate(this UpdateProveedorRequestDto proveedorDto)
        {
            return new Proveedor
            {
                Str_nombre = proveedorDto.Str_nombre,
                Str_telefono = proveedorDto.Str_telefono,
                Str_direccion = proveedorDto.Str_direccion,
                Str_correo = proveedorDto.Str_correo,
                Bool_borrado = false
            };
        }
    }
}