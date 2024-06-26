using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Deposito
{
    public class UpdateDepositoRequestDto
    {
        public string Str_nombre { get; set; } = string.Empty;
        public string Str_direccion { get; set; } = string.Empty;
        public string Str_telefono { get; set; } = string.Empty;
        public string EncargadoId { get; set; } = string.Empty;
        public bool Bool_borrado { get; set; } = false;
    }

}