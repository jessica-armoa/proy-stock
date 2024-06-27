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
        public string Str_nombre { get; set; } = string.Empty;
        public string Str_direccion { get; set; } = string.Empty;
        public string Str_telefono { get; set; } = string.Empty;
        public bool Bool_borrado { get; set; } = false;
        public int? FerreteriaId { get; set; }
        public string? Str_ferreteriaNombre { get; set; }
        public string? Str_ferreteriaTelefono { get; set; }
        public string EncargadoUsername { get; set; } = string.Empty;
        public string EncargadoEmail { get; set; } = string.Empty;
        public List<MovimientoDto> Movimientos { get; set; } = new();
        public List<ProductoDto> Productos { get; set; } = new();
    }
}