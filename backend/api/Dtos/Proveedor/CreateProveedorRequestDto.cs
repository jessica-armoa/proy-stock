using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Proveedor
{
    public class CreateProveedorRequestDto
    {
        public string Str_nombre { get; set; } = String.Empty;
        public string Str_telefono { get; set; } = String.Empty;
        public string Str_direccion { get; set; } = String.Empty;
        public string Str_correo { get; set; } = String.Empty;
    }
}