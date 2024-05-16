using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Producto;

namespace api.Dtos.Marca
{
    public class MarcaDto
    {
        public int Id { get; set; }
        public string Str_nombre { get; set; } = String.Empty;
        public int? ProveedorId { get; set; }
        public List<ProductoDto> Productos { get; set; } 
    }
}