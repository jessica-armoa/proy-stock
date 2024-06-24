using api.Dtos.Movimiento;

namespace api.Dtos.MotivoPorTipoDeMovimiento
{
    public class MotivoPorTipoDeMovimientoDto
    {
        public int Id { get; set; }
        public string Str_descripcion { get; set; } = String.Empty;
        public bool Bool_borrado { get; set; }
        public int? MotivoId { get; set; }
        public int? TipodemovimientoId { get; set; }
        public List<MovimientoDto> Movimientos { get; set; }
    }
}