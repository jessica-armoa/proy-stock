using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Ferreteria;
using api.Models;

namespace api.Mapper
{
    public static class FerreteriaMapper
    {
        public static FerreteriaDto ToFerreteriaDto(this Ferreteria ferreteriaModel)
        {
            return new FerreteriaDto
            {
                id_ferreteria = ferreteriaModel.Id,
                str_nombre = ferreteriaModel.Str_nombre,
                str_ruc = ferreteriaModel.Str_ruc,
                str_telefono = ferreteriaModel.Str_telefono
            };
        }
    }
}