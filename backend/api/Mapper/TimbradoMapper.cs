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
        Date_fin_vigencia = timbrado.Date_fin_vigencia
      };
    }

    public static Timbrado ToTimbrado(this TimbradoDto timbradoDto)
    {
      return new Timbrado
      {
        Id = timbradoDto.Id,
        Str_timbrado = timbradoDto.Str_timbrado,
        Date_inicio_vigencia = timbradoDto.Date_inicio_vigencia,
        Date_fin_vigencia = timbradoDto.Date_fin_vigencia
      };
    }

    public static Timbrado ToTimbrado(this CreateTimbradoDto createTimbradoDto)
    {
      return new Timbrado
      {
        Str_timbrado = createTimbradoDto.Str_timbrado,
        Date_inicio_vigencia = createTimbradoDto.Date_fecha_de_inicio,
        Date_fin_vigencia = createTimbradoDto.Date_fecha_de_fin
      };
    }
  }
}
