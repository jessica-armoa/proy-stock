using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class NotaDeRemision
    {
        public int Id { get; set; }
        public string Str_numero { get; set; } = string.Empty;
        public int TimbradoId { get; set; }
        public Timbrado? Timbrado { get; set; }
        public DateTime Date_fecha_de_expedicion { get; set; }
        public DateTime Date_fecha_de_vencimiento { get; set; }

        //Movimiento con productos
        public int? MovimientoId { get; set; }
        public Movimiento? Movimiento { get; set; }

        // Datos de Empresa
        public string EmpresaNombre { get; set; } = string.Empty;
        public string EmpresaDireccion { get; set; } = string.Empty;
        public string EmpresaTelefono { get; set; } = string.Empty;
        public string EmpresaSucursal { get; set; } = string.Empty;
        public string EmpresaActividad { get; set; } = string.Empty;

        // RUC
        public string Ruc { get; set; } = string.Empty;

        // Datos de Destinatario
        public string DestinatarioNombre { get; set; } = string.Empty;
        public string DestinatarioDocumento { get; set; } = string.Empty;

        // Datos de Punto
        public string PuntoPartida { get; set; } = string.Empty;
        public string PuntoLlegada { get; set; } = string.Empty;

        // Datos de Traslado
        public string TrasladoFechaInicio { get; set; } = string.Empty;
        public string TrasladoFechaFin { get; set; } = string.Empty;

        // Datos del Motivo
        public string Motivo { get; set; } = string.Empty;
        public string MotivoDescripcion { get; set; } = string.Empty;
        public string ComprobanteVenta { get; set; } = string.Empty;
    }
}