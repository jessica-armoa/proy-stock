using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Asiento
    {
        public int Id { get; set; }
        public int? movimientoId { get; set; }
        public Movimiento? Movimiento { get; set; }
        public string Str_cuenta { get; set; } = String.Empty;
        public string Str_concepto{ get; set; } = String.Empty;
        public string Str_descripcion { get; set; } = String.Empty;
        public decimal Dec_debe { get; set; }
        public decimal Dec_haber { get; set; }
        public bool Bool_borrado { get; set; }
    }
}