using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Dtos.DetalleDeMovimiento
{
    public class DetalleDeMovimientoDto
    {
        public int Id { get; set; }
        public int Int_cantidad { get; set; }
        public decimal Dec_costo { get; set; }
        public int? MovimientoId { get; set; }
        //public string Str_movimiento { get; set; } = String.Empty;
        public int? ProductoId { get; set; }
        public string Str_producto { get; set; } = String.Empty;
        public bool Bool_borrado { get; set; } = false;
    }
}