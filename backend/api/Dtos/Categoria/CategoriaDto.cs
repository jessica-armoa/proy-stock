using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Categoria
{
    public class CategoriaDto
    {
        public int id_categoria { get; set; }
        public string str_descripcion { get; set; } = String.Empty;
        public int? fk_proveedor { get; set; }
    }
}