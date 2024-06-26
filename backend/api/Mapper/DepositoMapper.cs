using api.Dtos.Deposito;
using api.Dtos.Producto;
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
                Str_encargado = depositoModel.Str_encargado,
                Str_telefonoEncargado = depositoModel.Str_telefonoEncargado,
                FerreteriaId = depositoModel.FerreteriaId,
                Str_ferreteriaNombre = depositoModel.Ferreteria.Str_nombre,
                Str_ferreteriaTelefono = depositoModel.Ferreteria.Str_telefono,
                Bool_borrado = depositoModel.Bool_borrado,
                Movimientos = depositoModel.Movimientos
                    .Where(m => m.Bool_borrado != true)
                    .Select(m => m.ToMovimientoDto()).ToList(),
                Productos = depositoModel.Productos
                    .Where(p => p.Bool_borrado != true)
                    .Select(p => p.ToProductoDto()).ToList()
            };
        }

        public static Deposito ToDepositoFromCreate(this CreateDepositoRequestDto depositoDto, int ferreteriaId)
        {
            return new Deposito
            {
                Str_nombre = depositoDto.Str_nombre,
                Str_direccion = depositoDto.Str_direccion,
                Str_telefono = depositoDto.Str_telefono,
                Str_encargado = depositoDto.Str_encargado,
                Str_telefonoEncargado = depositoDto.Str_telefonoEncargado,
                FerreteriaId = ferreteriaId,
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
                Str_encargado = depositoDto.Str_encargado,
                Str_telefonoEncargado = depositoDto.Str_telefonoEncargado,
                Bool_borrado = depositoDto.Bool_borrado
            };
        }
    }
}