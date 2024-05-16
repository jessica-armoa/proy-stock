using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Categoria;
using api.Models;

namespace api.Mapper
{
    public static class CategoriaMapper
    {
        public static CategoriaDto ToCategoriaDto(this Categoria categoriaModel)
        {
            return new CategoriaDto
            {
                Id = categoriaModel.Id,
                Str_descripcion = categoriaModel.Str_descripcion,
                ProveedorId = categoriaModel.ProveedorId
            };
        }
    }
}