using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Producto
{
    public class UpdateProductoRequestDto
    {
        /*
            Ya que es para editar, no le pasamos el id y tampoco la lista de detalles de movimientos
            Tampoco las foreign keys
        */
        public string str_ruta_imagen { get; set; } = String.Empty;
        public string str_nombre { get; set; } = String.Empty;
        public string str_descripcion { get; set; } = String.Empty;
        public int int_cantidad_actual { get; set; }
        public int int_cantidad_minima { get; set; }
        public decimal dec_costo_PPP { get; set; }
        public int int_iva { get; set; }
        public decimal dec_precio_mayorista { get; set; }
        public decimal dec_precio_minorista { get; set; }
    }
}