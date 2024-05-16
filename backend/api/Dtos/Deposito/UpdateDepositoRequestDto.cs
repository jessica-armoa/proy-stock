using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Deposito
{
    public class UpdateDepositoRequestDto
    {
        public string Str_nombre { get; set; } = String.Empty;
        public string Str_direccion { get; set; } = String.Empty;
    }
}