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
                Str_descripcion = tipoDeMovimientoModel.Str_descripcion,
                MotivoId = tipoDeMovimientoModel.MotivoId,
                Bool_borrado = tipoDeMovimientoModel.Bool_borrado,
                Movimientos = tipoDeMovimientoModel.Movimientos
                    .Where(m => m.Bool_borrado != true)
                    .Select(m => m.ToMovimientoDto()).ToList()
            };
        }
    }
}