using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.DetalleDeMovimiento
{
    public class CreateDetalleRequestDto
    {
        [Required]
        public int Int_cantidad { get; set; }
        [Required]
        public decimal Dec_costo { get; set; }
        [Required]
        public int? ProductoId { get; set; }
        public bool Bool_borrado { get; set; } = false;
    }
}