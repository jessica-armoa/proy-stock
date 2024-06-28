using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Marca;
using api.Dtos.Producto;
using api.Models;

namespace api.Mapper
{
    public static class MarcaMapper
    {
        public static MarcaDto ToMarcaDto(this Marca marcaModel)
        {
            return new MarcaDto
            {
                Id = marcaModel.Id,
                Str_nombre = marcaModel.Str_nombre,
                ProveedorId = marcaModel.ProveedorId,
                Bool_borrado = marcaModel.Bool_borrado,
                Productos = marcaModel.Productos
                    .Where(m => m.Bool_borrado != true)
                    .Select(p => p.ToProductoDtoFromMarca()).ToList()
            };
        }

        public static MarcaDto ToOnlyMarcaDto(this Marca marcaModel)
        {
            return new MarcaDto
            {
                Id = marcaModel.Id,
                Str_nombre = marcaModel.Str_nombre,
                ProveedorId = marcaModel.ProveedorId,
                Bool_borrado = marcaModel.Bool_borrado,
                Productos = new List<ProductoDto>()
            };
        }

        public static Marca ToMarcaFromCreate(this CreateMarcaRequestDto marcaDto, int proveedorId)
        {
            return new Marca
            {
                Str_nombre = marcaDto.Str_nombre,
                ProveedorId = proveedorId,
                Bool_borrado = false
            };
        }

        public static Marca ToMarcaFromUpdate(this UpdateMarcaRequestDto marcaDto)
        {
            return new Marca
            {
                Str_nombre = marcaDto.Str_nombre,
                Bool_borrado = marcaDto.Bool_borrado
            };
        }
    }
}