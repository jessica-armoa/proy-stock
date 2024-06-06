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
        //public int Int_cantidad { get; set; }
        public int? TipoDeMovimientoId { get; set; }
        public TipoDeMovimiento? TipoDeMovimiento{ get; set; }
        public int? DepositoOrigenId{ get; set; }
        public Deposito? DepositoOrigen { get; set; }
        public int? DepositoDestinoId { get; set; }
        public Deposito? DepositoDestino { get; set; }
        public bool Bool_borrado { get; set; } = false;
        public List<DetalleDeMovimiento> DetallesDeMovimientos { get; set; } = [];
    }
}