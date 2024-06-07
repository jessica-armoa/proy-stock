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
                MovimientoId = detalleDeMovimientoModel.MovimientoId,
                ProductoId = detalleDeMovimientoModel.ProductoId,
                Bool_borrado = detalleDeMovimientoModel.Bool_borrado
            };
        }

        public static DetalleDeMovimiento ToDetalleFromCreate(this CreateDetalleRequestDto detalleDto, int? movimientoId, int? productoId)
        {
            return new DetalleDeMovimiento
            {
                Int_cantidad = detalleDto.Int_cantidad,
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