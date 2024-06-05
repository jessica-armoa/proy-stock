using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Movimiento;

namespace api.Dtos.TipoDeMovimiento
{
    public class TipoDeMovimientoDto
    {
        public int Id { get; set; }
        public string Str_descripcion { get; set; } = String.Empty;
        public int? MotivoId { get; set; }
        public List<MovimientoDto> Movimientos { get; set; }
    }
}