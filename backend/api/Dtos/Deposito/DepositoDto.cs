using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Movimiento;
using api.Dtos.Producto;

namespace api.Dtos.Deposito
{
    public class DepositoDto
    {
        public int Id { get; set; }
        public string Str_nombre { get; set; } = String.Empty;
        public string Str_direccion { get; set; } = String.Empty;
        public string Str_telefono { get; set; } = String.Empty;
        public string Str_encargado { get; set; } = String.Empty;
        public string Str_telefonoEncargado { get; set; } = String.Empty;
        public int? FerreteriaId { get; set; }
        public String Str_ferreteriaNombre { get; set; } = String.Empty;
        public String Str_ferreteriaTelefono { get; set; } = String.Empty;
        public List<MovimientoDto> Movimientos { get; set; }
        public List<ProductoDto> Productos { get; set; }
    }
}