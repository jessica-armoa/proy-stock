using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Movimiento
{
    public class CreateMovimientoRequestDto
    {
        public DateTime Date_fecha { get; set; } = DateTime.Now;
    }
}