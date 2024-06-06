using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Deposito
{
    public class CreateDepositoRequestDto
    {
        public string Str_nombre { get; set; } = String.Empty;
        public string Str_direccion { get; set; } = String.Empty;
        public string Str_telefono { get; set; } = String.Empty;
        public string Str_encargado { get; set; } = String.Empty;
        public string Str_telefonoEncargado { get; set; } = String.Empty;
        public bool Bool_borrado { get; set; } = false;
    }
}