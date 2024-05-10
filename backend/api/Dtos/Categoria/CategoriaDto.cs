using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Categoria
{
    public class CategoriaDto
    {
        public int Id { get; set; }
        public string Str_descripcion { get; set; } = String.Empty;
        public int? ProveedorId { get; set; }
    }
}