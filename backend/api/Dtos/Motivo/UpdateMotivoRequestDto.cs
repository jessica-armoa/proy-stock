using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Motivo
{
    public class UpdateMotivoRequestDto
    {
        public string Str_motivo { get; set; } = String.Empty;
        public bool Bool_perdida{ get; set; }
        public bool Bool_borrado { get; set; } = false;
    }
}