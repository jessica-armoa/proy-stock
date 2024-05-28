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
        public DateTime Date_fecha { get; set; } = DateTime.Now;
        public int? TipoDeMovimientoId { get; set; }
        public int? DepositoOrigenId{ get; set; }
        public int? DepositoDestinoId { get; set; }
        public List<DetalleDeMovimientoDto> DetallesDeMovimientos { get; set; }
    }
}