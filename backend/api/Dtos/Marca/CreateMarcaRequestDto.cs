using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Marca
{
    public class CreateMarcaRequestDto
    {
        public string Str_nombre { get; set; } = String.Empty;
        public bool Bool_borrado { get; set; } = false;
    }
}