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
        public string Str_nombre { get; set; } = string.Empty;
        [Required]
        public string Str_direccion { get; set; } = string.Empty;
        [Required]
        public string Str_telefono { get; set; } = string.Empty;

        [Required]
        public string EncargadoUsername { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string EncargadoEmail { get; set; } = string.Empty;
        [Required]
        public string EncargadoPassword { get; set; } = string.Empty;
    }


}