using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.MotivoPorTipoDeMovimiento;

namespace api.Dtos.Motivo
{
    public class MotivoDto
    {
        public int Id { get; set; }
        public string Str_motivo { get; set; } = String.Empty;
        public bool Bool_perdida{ get; set; }
        public bool Bool_borrado { get; set; } = false;
        public List<MotivoPorTipoDeMovimientoDto> MotivosPorTipoDeMovimiento { get; set; }
    }
}