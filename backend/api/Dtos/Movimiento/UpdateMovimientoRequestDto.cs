using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.DetalleDeMovimiento;

namespace api.Dtos.Movimiento
{
    public class UpdateMovimientoRequestDto
    {
        public DateTime Date_fecha { get; set; } = DateTime.Now;
        public bool Bool_borrado { get; set; }
        public List<UpdateDetalleRequestDto> DetallesDeMovimientos { get; set; }
    }
}