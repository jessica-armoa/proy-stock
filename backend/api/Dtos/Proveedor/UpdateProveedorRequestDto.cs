using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Proveedor
{
    public class UpdateProveedorRequestDto
    {
        public string str_nombre { get; set; } = String.Empty;
        public string str_telefono { get; set; } = String.Empty;
        public string str_direccion { get; set; } = String.Empty;
        public string str_correo { get; set; } = String.Empty;
    }
}