using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.DetalleDeMovimiento;

namespace api.Dtos.Movimiento
{
    public class MovimientoDto
    {
        public int id_movimiento { get; set; }
        public DateTime date_fecha { get; set; } = DateTime.Now;
        public int? fk_tipo_de_movimiento { get; set; }
        public int? fk_deposito_origen{ get; set; }
        public int? fk_deposito_destino { get; set; }
        public List<DetalleDeMovimientoDto> detalles_de_movimientos { get; set; }
    }
}