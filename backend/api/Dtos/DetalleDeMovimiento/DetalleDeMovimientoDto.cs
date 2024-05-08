using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Dtos.DetalleDeMovimiento
{
    public class DetalleDeMovimientoDto
    {
        public int id_detalle_de_movimiento { get; set; }
        public int int_cantidad { get; set; }
        public int? fk_movimiento{ get; set; }
        public int? fk_producto { get; set; }
    }
}