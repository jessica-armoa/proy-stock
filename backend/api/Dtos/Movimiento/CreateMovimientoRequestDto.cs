using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.DetalleDeMovimiento;

namespace api.Dtos.Movimiento
{
    public class CreateMovimientoRequestDto
    {
        public DateTime Date_fecha { get; set; }
        public bool Bool_borrado { get; set; } = false;

    }
}