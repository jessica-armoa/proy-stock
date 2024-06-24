using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Movimiento
    {
        public int Id { get; set; }
        public DateTime Date_fecha { get; set; } = DateTime.Now;
        public bool Bool_borrado { get; set; } = false;
        public int? MotivoPorTipodeMovimientoId { get; set; }
        public MotivoPorTipoDeMovimiento? MotivoPorTipoDeMovimiento { get; set; }
        public int? DepositoOrigenId{ get; set; }
        public Deposito? DepositoOrigen { get; set; }
        public int? DepositoDestinoId { get; set; }
        public Deposito? DepositoDestino { get; set; }
        public List<DetalleDeMovimiento> DetallesDeMovimientos { get; set; } = [];
    }
}