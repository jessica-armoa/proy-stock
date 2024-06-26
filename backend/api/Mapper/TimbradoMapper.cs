using api.Models;
using api.Dtos.Timbrado;

namespace api.Mapper
{
  public static class TimbradoMapper
  {
    public static TimbradoDto ToTimbradoDto(this Timbrado timbrado)
    {
      return new TimbradoDto
      {
        Id = timbrado.Id,
        Str_timbrado = timbrado.Str_timbrado,
        Date_inicio_vigencia = timbrado.Date_inicio_vigencia,
        Date_fin_vigencia = timbrado.Date_fin_vigencia,
        Codigo_establecimiento = timbrado.Codigo_establecimiento,
        Punto_de_expedicion = timbrado.Punto_de_expedicion,
        Cantidad = timbrado.Cantidad,
        Secuencia_actual = timbrado.Secuencia_actual
      };
    }

    public static Timbrado ToTimbrado(this TimbradoDto timbradoDto)
    {
      return new Timbrado
      {
        Id = timbradoDto.Id,
        Str_timbrado = timbradoDto.Str_timbrado,
        Date_inicio_vigencia = timbradoDto.Date_inicio_vigencia,
        Date_fin_vigencia = timbradoDto.Date_fin_vigencia,
        Codigo_establecimiento = timbradoDto.Codigo_establecimiento,
        Punto_de_expedicion = timbradoDto.Punto_de_expedicion,
        Cantidad = timbradoDto.Cantidad,
        Secuencia_actual = timbradoDto.Secuencia_actual
      };
    }

    public static Timbrado ToTimbrado(this CreateTimbradoDto createTimbradoDto)
    {
      return new Timbrado
      {
        Str_timbrado = createTimbradoDto.Str_timbrado,
        Date_inicio_vigencia = createTimbradoDto.Date_fecha_de_inicio,
        Date_fin_vigencia = createTimbradoDto.Date_fecha_de_fin,
        Codigo_establecimiento = createTimbradoDto.Codigo_establecimiento,
        Punto_de_expedicion = createTimbradoDto.Punto_de_expedicion,
        Cantidad = createTimbradoDto.Cantidad,
        Secuencia_actual = createTimbradoDto.Secuencia_actual
      };
    }
  }
}