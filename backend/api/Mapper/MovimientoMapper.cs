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

        public static Movimiento ToMovimientoFromCreate(this CreateMovimientoRequestDto movimientoDto, int tipodemovimientoId, int depositoOrigen, int depositoDestino)
        {
            return new Movimiento
            {
                Date_fecha = movimientoDto.Date_fecha,
                TipoDeMovimientoId = tipodemovimientoId,
                DepositoOrigenId = depositoOrigen,
                DepositoDestinoId = depositoDestino
            };
        }

        public static Movimiento ToMovimientoFromUpdate(this UpdateMovimientoRequestDto movimientoDto)
        {
            return new Movimiento
            {
                Date_fecha = movimientoDto.Date_fecha
            };
        }
    }
}