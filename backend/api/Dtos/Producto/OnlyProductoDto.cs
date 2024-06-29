using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Producto
{
    public class OnlyProductoDto
    {
        public int Id { get; set; }
        public string Str_nombre { get; set; } = String.Empty;
        public string Str_descripcion { get; set; } = String.Empty;
        public int? DepositoId { get; set; }
        public string DepositoNombre { get; set; } = String.Empty;
        public int? ProveedorId { get; set; }
        public string ProveedorNombre { get; set; } = String.Empty;
        public int? MarcaId { get; set; }
        public string MarcaNombre { get; set; } = String.Empty;
        public int Int_cantidad_actual { get; set; }
        public int Int_cantidad_minima { get; set; }
        public decimal Dec_costo { get; set; }
        public decimal Dec_costo_PPP { get; set; }
        public int Int_iva { get; set; }
        public decimal Dec_precio_mayorista { get; set; }
        public decimal Dec_precio_minorista { get; set; }
        public bool Bool_borrado { get; set; } = false;        
    }
}