using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Deposito;
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
                Id = ferreteriaModel.Id,
                Str_nombre = ferreteriaModel.Str_nombre,
                Str_ruc = ferreteriaModel.Str_ruc,
                Str_telefono = ferreteriaModel.Str_telefono,
                Bool_borrado = ferreteriaModel.Bool_borrado,
                Depositos = ferreteriaModel.Depositos
                    .Where(d => d.Bool_borrado != true)
                    .Select(d => d.ToDepositoDto()).ToList()
            };
        }

        public static OnlyFerreteriaDto ToOnlyFerreteriaDto(this Ferreteria ferreteriaModel)
        {
            return new OnlyFerreteriaDto
            {
                Id = ferreteriaModel.Id,
                Str_nombre = ferreteriaModel.Str_nombre,
                Str_ruc = ferreteriaModel.Str_ruc,
                Str_telefono = ferreteriaModel.Str_telefono,
                Bool_borrado = ferreteriaModel.Bool_borrado
            };
        }

        public static Ferreteria ToFerreteriaFromCreate(this CreateFerreteriaRequestDto ferreteriaDto)
        {
            return new Ferreteria
            {
                Str_nombre = ferreteriaDto.Str_nombre,
                Str_ruc = ferreteriaDto.Str_ruc,
                Str_telefono = ferreteriaDto.Str_telefono,
                Bool_borrado = false
            };
        }

        public static Ferreteria ToFerreteriaFromUpdate(this UpdateFerreteriaRequestDto ferreteriaDto)
        {
            return new Ferreteria
            {
                Str_nombre = ferreteriaDto.Str_nombre,
                Str_ruc = ferreteriaDto.Str_ruc,
                Str_telefono = ferreteriaDto.Str_telefono,
                Bool_borrado = ferreteriaDto.Bool_borrado
            };
        }
    }
}