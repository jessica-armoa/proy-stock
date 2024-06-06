using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class DetalleDeMovimiento
    {
        //[Key]
        public int Id { get; set; }
        public int Int_cantidad { get; set; }
       // public string Str_encargado { get; set; } = String.Empty;
       //[ForeignKey("Movimiento")]
        public int? MovimientoId { get; set; }
        public Movimiento? Movimiento { get; set; }
        //[ForeignKey("Producto")]
        public int? ProductoId { get; set; }
        public Producto? Producto { get; set; }
        public NotaDeRemision NotaDeRemision { get; set; }
    }
}