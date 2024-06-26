using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.MotivoPorTipoDeMovimiento;
using api.Dtos.Movimiento;

namespace api.Dtos.TipoDeMovimiento
{
    public class TipoDeMovimientoDto
    {
        public int Id { get; set; }
        public string Str_tipo { get; set; } = String.Empty;
        public bool Bool_operacion { get; set; }
        public bool Bool_borrado { get; set; } = false;
        public List<MotivoPorTipoDeMovimientoDto> MotivosPorTipoDeMovimiento { get; set; }
    }
}