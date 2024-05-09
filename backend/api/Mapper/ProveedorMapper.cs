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
                id_proveedor = proveedorModel.Id,
                str_nombre = proveedorModel.Str_nombre,
                str_telefono = proveedorModel.Str_telefono,
                str_direccion = proveedorModel.Str_direccion,
                str_correo = proveedorModel.Str_correo,
                productos = proveedorModel.Productos.Select(p => p.ToProductoDto()).ToList(),
                categorias = proveedorModel.Categorias.Select(c => c.ToCategoriaDto()).ToList()
            };
        }

        public static Proveedor ToProveedorFromCreate(this CreateProveedorDto proveedorDto)
        {
            return new Proveedor
            {
                Str_nombre = proveedorDto.str_nombre,
                Str_telefono = proveedorDto.str_telefono,
                Str_direccion = proveedorDto.str_direccion,
                Str_correo = proveedorDto.str_correo
            };
        }

        public static Proveedor ToDepositoFromUpdate(this UpdateProveedorRequestDto proveedorDto)
        {
            return new Proveedor
            {
                Str_nombre = proveedorDto.str_nombre,
                Str_telefono = proveedorDto.str_telefono,
                Str_direccion = proveedorDto.str_direccion,
                Str_correo = proveedorDto.str_correo
            };
        }
    }
}