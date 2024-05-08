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
        public int id_deposito { get; set; }
        public string str_nombre { get; set; } = String.Empty;
        public string str_direccion { get; set; } = String.Empty;
        public int? fk_ferreteria { get; set; }
        public List<MovimientoDto> movimientos { get; set; }
        public List<ProductoDto> productos { get; set; }
    }
}