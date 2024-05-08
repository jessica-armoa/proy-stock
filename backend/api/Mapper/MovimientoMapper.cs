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
                id_movimiento = movimientoModel.Id,
                date_fecha = movimientoModel.Date_fecha,
                fk_tipo_de_movimiento = movimientoModel.Fk_tipo_de_movimiento,
                fk_deposito_origen = movimientoModel.Fk_deposito_origen,
                fk_deposito_destino = movimientoModel.Fk_deposito_destino,
                detalles_de_movimientos = movimientoModel.Detalles_de_movimientos.Select(m => m.ToDetalleDeMovimientoDto()).ToList()
            };
        }
    }
}