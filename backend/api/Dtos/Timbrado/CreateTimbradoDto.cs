using System;

namespace api.Dtos.Timbrado{

  public class CreateTimbradoDto
  {
    public string Str_timbrado { get; set; } = string.Empty;
    public DateTime Date_fecha_de_inicio { get; set; }
    public DateTime Date_fecha_de_fin { get; set; }
  }
}
