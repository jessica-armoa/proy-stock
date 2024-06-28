using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.DetalleDeMovimiento;

namespace api.Dtos.NotaDeRemision
{
  public class CreateNotaDeRemisionDto
  {

    public int TimbradoId { get; set; }
    public string Str_numero { get; set; } = string.Empty;
    public DateTime Date_fecha_de_expedicion { get; set; }
    public DateTime Date_fecha_de_vencimiento { get; set; }
    public int MovimientoId { get; set; }

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
    public string TrasladoVehiculo { get; set; } = string.Empty;
    public string TrasladoRua { get; set; } = string.Empty;

    // Datos del Transportista
    public string TransportistaNombre { get; set; } = string.Empty;
    public string TransportistaRuc { get; set; } = string.Empty;

    // Datos del Conductor
    public string ConductorNombre { get; set; } = string.Empty;
    public string ConductorDocumento { get; set; } = string.Empty;
    public string ConductorDireccion { get; set; } = string.Empty;

    // Datos del Motivo
    public string Motivo { get; set; } = string.Empty;
    public string MotivoDescripcion { get; set; } = string.Empty;
    public string ComprobanteVenta { get; set; } = string.Empty;
  }
}