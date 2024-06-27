using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Ferreteria;
using api.Dtos.NotaDeRemision;
using api.Models;

namespace api.Mapper
{
  public static class NotaDeRemisionMapper
  {
    public static NotaDeRemisionDto ToNotaDeRemisionDto(this NotaDeRemision notaDeRemision)
    {
      return new NotaDeRemisionDto
      {
        Id = notaDeRemision.Id,
        Str_numero = notaDeRemision.Str_numero,
        TimbradoId = notaDeRemision.TimbradoId,
        Date_fecha_de_expedicion = notaDeRemision.Date_fecha_de_expedicion,
        Date_fecha_de_vencimiento = notaDeRemision.Date_fecha_de_vencimiento,
        MovimientoId = notaDeRemision.MovimientoId,
        EmpresaNombre = notaDeRemision.EmpresaNombre,
        EmpresaDireccion = notaDeRemision.EmpresaDireccion,
        EmpresaTelefono = notaDeRemision.EmpresaTelefono,
        EmpresaSucursal = notaDeRemision.EmpresaSucursal,
        EmpresaActividad = notaDeRemision.EmpresaActividad,
        Ruc = notaDeRemision.Ruc,
        DestinatarioNombre = notaDeRemision.DestinatarioNombre,
        DestinatarioDocumento = notaDeRemision.DestinatarioDocumento,
        PuntoPartida = notaDeRemision.PuntoPartida,
        PuntoLlegada = notaDeRemision.PuntoLlegada,
        TrasladoFechaInicio = notaDeRemision.TrasladoFechaInicio,
        TrasladoFechaFin = notaDeRemision.TrasladoFechaFin,
        Motivo = notaDeRemision.Motivo,
        MotivoDescripcion = notaDeRemision.MotivoDescripcion,
        ComprobanteVenta = notaDeRemision.ComprobanteVenta
      };
    }

    public static NotaDeRemision ToNotaDeRemision(this NotaDeRemisionDto notaDeRemisionDto)
    {
      return new NotaDeRemision
      {
        TimbradoId = notaDeRemisionDto.TimbradoId,
        Str_numero = notaDeRemisionDto.Str_numero,
        Date_fecha_de_expedicion = notaDeRemisionDto.Date_fecha_de_expedicion,
        Date_fecha_de_vencimiento = notaDeRemisionDto.Date_fecha_de_vencimiento,
        MovimientoId = notaDeRemisionDto.MovimientoId,
        EmpresaNombre = notaDeRemisionDto.EmpresaNombre,
        EmpresaDireccion = notaDeRemisionDto.EmpresaDireccion,
        EmpresaTelefono = notaDeRemisionDto.EmpresaTelefono,
        EmpresaSucursal = notaDeRemisionDto.EmpresaSucursal,
        EmpresaActividad = notaDeRemisionDto.EmpresaActividad,
        Ruc = notaDeRemisionDto.Ruc,
        DestinatarioNombre = notaDeRemisionDto.DestinatarioNombre,
        DestinatarioDocumento = notaDeRemisionDto.DestinatarioDocumento,
        PuntoPartida = notaDeRemisionDto.PuntoPartida,
        PuntoLlegada = notaDeRemisionDto.PuntoLlegada,
        TrasladoFechaInicio = notaDeRemisionDto.TrasladoFechaInicio,
        TrasladoFechaFin = notaDeRemisionDto.TrasladoFechaFin,
        Motivo = notaDeRemisionDto.Motivo,
        MotivoDescripcion = notaDeRemisionDto.MotivoDescripcion,
        ComprobanteVenta = notaDeRemisionDto.ComprobanteVenta
      };
    }


    public static NotaDeRemision ToNotaDeRemision(this CreateNotaDeRemisionDto createDto)
    {
      return new NotaDeRemision
      {
        Str_numero = createDto.Str_numero,
        TimbradoId = createDto.TimbradoId,
        Date_fecha_de_expedicion = createDto.Date_fecha_de_expedicion,
        Date_fecha_de_vencimiento = createDto.Date_fecha_de_vencimiento,
        EmpresaNombre = createDto.EmpresaNombre,
        EmpresaDireccion = createDto.EmpresaDireccion,
        EmpresaTelefono = createDto.EmpresaTelefono,
        EmpresaSucursal = createDto.EmpresaSucursal,
        EmpresaActividad = createDto.EmpresaActividad,
        Ruc = createDto.Ruc,
        DestinatarioNombre = createDto.DestinatarioNombre,
        DestinatarioDocumento = createDto.DestinatarioDocumento,
        PuntoPartida = createDto.PuntoPartida,
        PuntoLlegada = createDto.PuntoLlegada,
        TrasladoFechaInicio = createDto.TrasladoFechaInicio,
        TrasladoFechaFin = createDto.TrasladoFechaFin,
        Motivo = createDto.Motivo,
        MotivoDescripcion = createDto.MotivoDescripcion,
        ComprobanteVenta = createDto.ComprobanteVenta
      };
    }
  }
}