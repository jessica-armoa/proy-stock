using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Deposito
{
    public class UpdateDepositoRequestDto
    {
        public string str_nombre { get; set; } = String.Empty;
        public string str_direccion { get; set; } = String.Empty;
    }
}