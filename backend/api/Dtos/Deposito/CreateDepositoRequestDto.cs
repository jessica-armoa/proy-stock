using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Deposito
{
    public class CreateDepositoRequestDto
    {
        [Required]
        public string Str_nombre { get; set; } = String.Empty;
        [Required]
        public string Str_direccion { get; set; } = String.Empty;
        [Required]
        public string Str_telefono { get; set; } = String.Empty;
        [Required]
        public string Str_encargado { get; set; } = String.Empty;
        [Required]
        public string Str_telefonoEncargado { get; set; } = String.Empty;
    }
}