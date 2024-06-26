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
                Id = detalleDeMovimientoModel.Id,
                Int_cantidad = detalleDeMovimientoModel.Int_cantidad,
                Dec_costo = detalleDeMovimientoModel.Dec_costo,
                MovimientoId = detalleDeMovimientoModel.MovimientoId,
                //Str_movimiento = detalleDeMovimientoModel.Movimiento.Str_descripcion,
                ProductoId = detalleDeMovimientoModel.ProductoId,
                Str_producto = detalleDeMovimientoModel.Producto.Str_nombre,
                Bool_borrado = detalleDeMovimientoModel.Bool_borrado
            };
        }

        public static DetalleDeMovimiento ToDetalleFromCreate(this CreateDetalleRequestDto detalleDto, int? movimientoId, int? productoId)
        {
            return new DetalleDeMovimiento
            {
                Int_cantidad = detalleDto.Int_cantidad,
                Dec_costo = detalleDto.Dec_costo,
                MovimientoId = movimientoId,
                ProductoId = productoId,
                Bool_borrado = false
            };
        }

        public static DetalleDeMovimiento ToDetalleFromUpdate(this UpdateDetalleRequestDto detalleDto)
        {
            return new DetalleDeMovimiento
            {
                Int_cantidad = detalleDto.Int_cantidad,
                Bool_borrado = detalleDto.Bool_borrado
            };
        }
    }
}