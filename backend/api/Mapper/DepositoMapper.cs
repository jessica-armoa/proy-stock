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
                Id = depositoModel.Id,
                Str_nombre = depositoModel.Str_nombre,
                Str_direccion = depositoModel.Str_direccion,
                FerreteriaId = depositoModel.FerreteriaId,
                Str_ferreteriaNombre = depositoModel.Ferreteria?.Str_nombre,
                Str_ferreteriaTelefono = depositoModel.Ferreteria?.Str_telefono,
                Movimientos = depositoModel.Movimientos.Select(m => m.ToMovimientoDto()).ToList(),
                Productos = depositoModel.Productos.Select(p => p.ToProductoDto()).ToList()
            };
        }

        public static Deposito ToDepositoFromCreate(this CreateDepositoRequestDto depositoDto, int ferreteriaId)
        {
            return new Deposito
            {
                Str_nombre = depositoDto.Str_nombre,
                Str_direccion = depositoDto.Str_direccion,
                FerreteriaId = ferreteriaId
            };
        }

        public static Deposito ToDepositoFromUpdate(this UpdateDepositoRequestDto depositoDto)
        {
            return new Deposito
            {
                Str_nombre = depositoDto.Str_nombre,
                Str_direccion = depositoDto.Str_direccion,
            };
        }
    }
}