using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Proveedor
{
    public class CreateProveedorRequestDto
    {
        [Required]
        public string Str_nombre { get; set; } = String.Empty;
        [Required]
        public string Str_telefono { get; set; } = String.Empty;
        [Required]
        public string Str_direccion { get; set; } = String.Empty;
        [Required]
        public string Str_correo { get; set; } = String.Empty;
        public bool Bool_borrado { get; set; } = false;
    }
}