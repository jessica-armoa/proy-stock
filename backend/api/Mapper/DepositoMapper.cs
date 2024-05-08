using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Deposito;
using api.Models;

namespace api.Mapper
{
    public static class DepositoMapper
    {
        public static DepositoDto ToDepositoDto(this Deposito depositoModel)
        {
            return new DepositoDto
            {
                id_deposito = depositoModel.Id,
                str_nombre = depositoModel.Str_nombre,
                str_direccion = depositoModel.Str_direccion,
                fk_ferreteria = depositoModel.Fk_ferreteria,
                movimientos = depositoModel.Movimientos.Select(s => s.ToMovimientoDto()).ToList(),
                productos = depositoModel.Productos.Select(p => p.ToProductoDto()).ToList()
            };
        }

        public static Deposito ToDepositoFromCreate(this CreateDepositoDto depositoDto, int ferreteriaId)
        {
            return new Deposito
            {
                Str_nombre = depositoDto.str_nombre,
                Str_direccion = depositoDto.str_direccion,
                Fk_ferreteria = ferreteriaId
            };
        }

        public static Deposito ToDepositoFromUpdate(this UpdateDepositoRequestDto depositoDto)
        {
            return new Deposito
            {
                Str_nombre = depositoDto.str_nombre,
                Str_direccion = depositoDto.str_direccion,
            };
        }
    }
}