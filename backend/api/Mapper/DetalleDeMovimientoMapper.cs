using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.DetalleDeMovimiento;
using api.Models;

namespace api.Mapper
{
    public static class DetalleDeMovimientoMapper
    {
        public static DetalleDeMovimientoDto ToDetalleDeMovimientoDto(this DetalleDeMovimiento detalleDeMovimientoModel)
        {
            return new DetalleDeMovimientoDto
            {
                id_detalle_de_movimiento = detalleDeMovimientoModel.Id,
                int_cantidad = detalleDeMovimientoModel.Int_cantidad,
                fk_movimiento = detalleDeMovimientoModel.Fk_movimiento,
                fk_producto = detalleDeMovimientoModel.Fk_producto
            };
        }
    }
}