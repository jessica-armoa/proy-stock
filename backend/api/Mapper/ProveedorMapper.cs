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
                Productos = proveedorModel.Productos.Select(p => p.ToProductoDto()).ToList(),
                Categorias = proveedorModel.Categorias.Select(c => c.ToCategoriaDto()).ToList()
            };
        }

        public static Proveedor ToProveedorFromCreate(this CreateProveedorRequestDto proveedorDto)
        {
            return new Proveedor
            {
                Str_nombre = proveedorDto.Str_nombre,
                Str_telefono = proveedorDto.Str_telefono,
                Str_direccion = proveedorDto.Str_direccion,
                Str_correo = proveedorDto.Str_correo
            };
        }

        public static Proveedor ToDepositoFromUpdate(this UpdateProveedorRequestDto proveedorDto)
        {
            return new Proveedor
            {
                Str_nombre = proveedorDto.Str_nombre,
                Str_telefono = proveedorDto.Str_telefono,
                Str_direccion = proveedorDto.Str_direccion,
                Str_correo = proveedorDto.Str_correo
            };
        }
    }
}