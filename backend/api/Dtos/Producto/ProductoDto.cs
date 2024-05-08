using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.DetalleDeMovimiento;
using api.Models;

namespace api.Dtos.Producto
{
    public class ProductoDto
    {
        public int id_producto { get; set; }
        public string str_ruta_imagen { get; set; } = String.Empty;
        public string str_nombre { get; set; } = String.Empty;
        public string str_descripcion { get; set; } = String.Empty;
        public int? fk_deposito { get; set; }
        public int? fk_proveedor { get; set; }
        public int? fk_marca { get; set; }
        public int int_cantidad_actual { get; set; }
        public int int_cantidad_minima { get; set; }
        public decimal dec_costo_PPP { get; set; }
        public int int_iva { get; set; }
        public decimal dec_precio_mayorista { get; set; }
        public decimal dec_precio_minorista { get; set; }
        public List<DetalleDeMovimientoDto> detallesDeMovimientos { get; set; }
    }
}