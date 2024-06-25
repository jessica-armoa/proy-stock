using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.TipoDeMovimiento
{
    public class CreateTipoDeMovimientoRequestDto
    {
        public string Str_tipo { get; set; } = String.Empty;
        public bool Bool_operacion { get; set; }
        public bool Bool_borrado { get; set; } = false;
    }
}