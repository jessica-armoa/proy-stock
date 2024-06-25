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
        public int Id { get; set; }
        public int Int_cantidad { get; set; }
        public decimal Dec_costo { get; set; }
        public int? MovimientoId { get; set; }
        public Movimiento? Movimiento { get; set; }
        public int? ProductoId { get; set; }
        public Producto? Producto { get; set; }
        public bool Bool_borrado { get; set; } = false;
    }
}