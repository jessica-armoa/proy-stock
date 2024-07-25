using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.MotivoPorTipoDeMovimiento;
using api.Dtos.Movimiento;
using api.Models;

namespace api.Mapper
{
    public static class MotivoPorTipoDeMovimientoMapper
    {
        public static MotivoPorTipoDeMovimientoDto ToMotivoPorTipoDeMovimientoDto(this MotivoPorTipoDeMovimiento motivoPorTipoDeMovimientoModel)
        {
            return new MotivoPorTipoDeMovimientoDto
            {
                Id = motivoPorTipoDeMovimientoModel.Id,
                Str_descripcion = motivoPorTipoDeMovimientoModel.Str_descripcion,
                Bool_borrado = motivoPorTipoDeMovimientoModel.Bool_borrado,
                MotivoId = motivoPorTipoDeMovimientoModel.MotivoId,
                TipodemovimientoId = motivoPorTipoDeMovimientoModel.TipodemovimientoId,
                Movimientos = new List<MovimientoDto>()
            };
        }
    }
}