using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.TipoDeMovimiento;
using api.Models;

namespace api.Mapper
{
    public static class TipoDeMovimientoMapper
    {
        public static TipoDeMovimientoDto ToTipoDeMovimientoDto(this TipoDeMovimiento tipoDeMovimientoModel)
        {
            return new TipoDeMovimientoDto
            {
                Id = tipoDeMovimientoModel.Id,
                Str_tipo = tipoDeMovimientoModel.Str_tipo,
                Bool_operacion = tipoDeMovimientoModel.Bool_operacion,
                Bool_borrado = tipoDeMovimientoModel.Bool_borrado,
                MotivosPorTipoDeMovimiento = tipoDeMovimientoModel.MotivosPorTipoDeMovimiento
                    .Where(m => m.Bool_borrado != true)
                    .Select(m => m.ToMotivoPorTipoDeMovimientoDto()).ToList()
            };
        }

        public static TipoDeMovimiento ToTipoDeMovimientoFromCreate(this CreateTipoDeMovimientoRequestDto tipoDeMovimientoDto)
        {
            return new TipoDeMovimiento
            {
                Str_tipo = tipoDeMovimientoDto.Str_tipo,
                Bool_operacion = tipoDeMovimientoDto.Bool_operacion,
                Bool_borrado = false
            };
        }

        public static TipoDeMovimiento ToTipoDeMovimientoFromUpdate(this UpdateTipoDeMovimientoRequestDto tipoDeMovimientoDto)
        {
            return new TipoDeMovimiento
            {
                Str_tipo = tipoDeMovimientoDto.Str_tipo,
                Bool_operacion = tipoDeMovimientoDto.Bool_operacion,
                Bool_borrado = false
            };
        }

    }
}