using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.DetalleDeMovimiento
{
    public class CreateDetalleRequestDto
    {
        public int Int_cantidad { get; set; }
        public bool Bool_borrado { get; set; } = false;
        // public string Str_encargado { get; set; } = String.Empty;
    }
}