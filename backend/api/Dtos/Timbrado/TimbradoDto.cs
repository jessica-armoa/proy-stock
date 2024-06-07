using System;

namespace api.Dtos.Timbrado
{
    public class TimbradoDto
    {
        public int Id { get; set; }
        public string Str_timbrado { get; set; } = String.Empty;
        public DateTime Date_inicio_vigencia { get; set; }
        public DateTime Date_fin_vigencia { get; set; }
    }
}
