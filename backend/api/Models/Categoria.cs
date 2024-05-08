using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Str_descripcion { get; set; } = String.Empty;
        public int? Fk_proveedor { get; set; }
        public Proveedor? Proveedor { get; set; }
    }
}