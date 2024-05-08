using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class DetalleDeMovimiento
    {
        public int Id { get; set; }
        public int Int_cantidad { get; set; }
        public int? Fk_movimiento { get; set; }
        public Movimiento? Movimiento { get; set; }
        public int? Fk_producto { get; set; }
        public Producto? Producto { get; set; }
    }
}