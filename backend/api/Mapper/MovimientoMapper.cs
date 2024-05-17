using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Movimiento;
using api.Models;

namespace api.Mapper
{
    public static class MovimientoMapper
    {
        public static MovimientoDto ToMovimientoDto(this Movimiento movimientoModel)
        {
            return new MovimientoDto
            {
                Id = movimientoModel.Id,
                Date_fecha = movimientoModel.Date_fecha,
                TipoDeMovimientoId = movimientoModel.TipoDeMovimientoId,
                DepositoOrigenId = movimientoModel.DepositoOrigenId,
                DepositoDestinoId= movimientoModel.DepositoDestinoId,
                DetallesDeMovimientos = movimientoModel.DetallesDeMovimientos.Select(m => m.ToDetalleDeMovimientoDto()).ToList()
            };
        }
    }
}