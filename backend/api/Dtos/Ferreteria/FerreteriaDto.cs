using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Deposito;
using api.Models;

namespace api.Dtos.Ferreteria
{
    public class FerreteriaDto
    {
        public int id_ferreteria { get; set; }
        public string str_nombre{ get; set; } = String.Empty;
        public string str_ruc { get; set; } = String.Empty;
        public string str_telefono { get; set; } = String.Empty;
        public List<DepositoDto> depositos { get; set; }
    }
}