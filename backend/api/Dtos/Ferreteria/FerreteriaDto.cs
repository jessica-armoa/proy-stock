using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Deposito;

namespace api.Dtos.Ferreteria
{
    public class FerreteriaDto
    {
        public int Id { get; set; }
        public string Str_nombre{ get; set; } = String.Empty;
        public string Str_ruc { get; set; } = String.Empty;
        public string Str_telefono { get; set; } = String.Empty;
        public bool Bool_borrado { get; set; } = false;
        public List<DepositoDto> Depositos { get; set; }
    }
}