using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.DetalleDeMovimiento;
using api.Dtos.Deposito;
using api.Dtos.Deposito.api.Dtos.Deposito;

namespace api.Dtos.Movimiento
{
    public class MovimientoDto
    {
        public int Id { get; set; }
        public DateTime Date_fecha { get; set; } = DateTime.Now;
        //public int Int_cantidad { get; set; }
        public int? TipoDeMovimientoId { get; set; }
        //public string Str_tipoDeMovimiento { get; set; } = String.Empty;
        //public string Str_motivo { get; set; } = String.Empty;
        public int? DepositoOrigenId { get; set; }
        //public string Str_depositoOrigen { get; set; } = String.Empty;
        public int? DepositoDestinoId { get; set; }
        //public string Str_depositoDestino { get; set; } = String.Empty;
        public bool Bool_borrado { get; set; } = false;
        public List<DetalleDeMovimientoDto> DetallesDeMovimientos { get; set; }
        public DepositoDto DepositoOrigen { get; set; }
        public DepositoDto DepositoDestino { get; set; }
    }
}