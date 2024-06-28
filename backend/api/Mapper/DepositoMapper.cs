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
                Str_telefono = depositoModel.Str_telefono,
                EncargadoUsername = depositoModel.Encargado?.UserName,
                EncargadoEmail = depositoModel.Encargado?.Email,
                FerreteriaId = depositoModel.FerreteriaId,
                Str_ferreteriaNombre = depositoModel.Ferreteria?.Str_nombre,
                Str_ferreteriaTelefono = depositoModel.Ferreteria?.Str_telefono,
                Bool_borrado = depositoModel.Bool_borrado,
                Movimientos = depositoModel.Movimientos
                    .Where(m => !m.Bool_borrado)
                    .Select(m => m.ToMovimientoDto()).ToList(),
                Productos = depositoModel.Productos
                    .Where(p => !p.Bool_borrado)
                    .Select(p => p.ToProductoDto()).ToList()
            };
        }

        public static Deposito ToDepositoFromCreate(this CreateDepositoRequestDto depositoDto, int ferreteriaId, string Encargado_Username, string? encargadoId)
        {
            return new Deposito
            {
                Str_nombre = depositoDto.Str_nombre,
                Str_direccion = depositoDto.Str_direccion,
                Str_telefono = depositoDto.Str_telefono,
                FerreteriaId = ferreteriaId,
                EncargadoUsername = Encargado_Username,
                EncargadoId = encargadoId,
                Bool_borrado = false
            };
        }

        public static Deposito ToDepositoFromUpdate(this UpdateDepositoRequestDto depositoDto)
        {
            return new Deposito
            {
                Str_nombre = depositoDto.Str_nombre,
                Str_direccion = depositoDto.Str_direccion,
                Str_telefono = depositoDto.Str_telefono,
                Bool_borrado = depositoDto.Bool_borrado
            };
        }
    }

}