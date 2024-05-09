using api.Dtos.Categoria;
using api.Dtos.Producto;

namespace api.Dtos.Proveedor
{
    public class ProveedorDto
    {
        public int id_proveedor { get; set; }
        public string str_nombre { get; set; } = String.Empty;
        public string str_telefono { get; set; } = String.Empty;
        public string str_direccion { get; set; } = String.Empty;
        public string str_correo { get; set; } = String.Empty;
        public List<ProductoDto> productos { get; set; }
        public List<CategoriaDto> categorias { get; set; } 
    }
}