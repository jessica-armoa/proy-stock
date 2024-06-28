using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class MotivoPorTipoDeMovimiento
    {
        public int Id { get; set; }
        public string Str_descripcion { get; set; } = String.Empty;
        public bool Bool_borrado { get; set; }
        public int? MotivoId { get; set; }
        public Motivo? Motivo { get; set; }
        public int? TipodemovimientoId { get; set; }
        public TipoDeMovimiento? TipoDeMovimiento { get; set; }
        public List<Movimiento> Movimientos { get; set; } = [];
    }
}