using System;

namespace api.Dtos.Timbrado
{
    public class TimbradoDto
    {
        public int Id { get; set; }
        public string Str_timbrado { get; set; } = string.Empty;
        public DateTime Date_inicio_vigencia { get; set; }
        public DateTime Date_fin_vigencia { get; set; }
        public string Codigo_establecimiento { get; set; } = string.Empty;
        public string Punto_de_expedicion { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public int Secuencia_actual { get; set; }
    }
}