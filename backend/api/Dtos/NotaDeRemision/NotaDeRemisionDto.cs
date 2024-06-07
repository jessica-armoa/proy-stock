using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.DetalleDeMovimiento;

namespace api.Dtos.NotaDeRemision
{
    public class NotaDeRemisionDto
    {
        public int Id { get; set; }
        public string Str_numero { get; set; }
        public string Str_timbrado { get; set; }
        public string Str_numero_de_comprobante_inicial { get; set; } = String.Empty;
        public string Str_numero_de_comprobante_final { get; set; } = String.Empty;
        public string Str_numero_de_comprobante_actual { get; set; } = String.Empty;
        public DateTime Date_fecha_de_expedicion { get; set; }
        public DateTime Date_fecha_de_vencimiento { get; set; }
        public int? MovimientoId { get; set; }
    }
}