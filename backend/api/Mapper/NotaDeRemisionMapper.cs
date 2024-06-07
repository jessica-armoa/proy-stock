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
    public static NotaDeRemisionDto ToNotaDeRemisionDto(this NotaDeRemision notaDeRemisionModel)
    {
      return new NotaDeRemisionDto
      {
        Id = notaDeRemisionModel.Id,
        Str_numero = notaDeRemisionModel.Str_numero,
        Str_timbrado = notaDeRemisionModel.Str_timbrado,
        Str_numero_de_comprobante_inicial = notaDeRemisionModel.Str_numero_de_comprobante_inicial,
        Str_numero_de_comprobante_final = notaDeRemisionModel.Str_numero_de_comprobante_final,
        Str_numero_de_comprobante_actual = notaDeRemisionModel.Str_numero_de_comprobante_actual,
        Date_fecha_de_expedicion = notaDeRemisionModel.Date_fecha_de_expedicion,
        Date_fecha_de_vencimiento = notaDeRemisionModel.Date_fecha_de_vencimiento,
        MovimientoId = notaDeRemisionModel.MovimientoId
      };
    }
  }
}