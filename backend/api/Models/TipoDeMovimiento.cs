using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class TipoDeMovimiento
    {
        public int Id { get; set; }
        public string Str_descripcion { get; set; } = String.Empty;
        public int? MotivoId { get; set; }
        public Motivos? Motivo { get; set; }
        public bool Bool_borrado { get; set; } = false;
        public List<Movimiento> Movimientos { get; set; } = [];
    }
}