using api.Dtos.Categoria;
using api.Dtos.Marca;
using api.Dtos.Producto;

namespace api.Dtos.Proveedor
{
    public class ProveedorDto
    {
        public int Id { get; set; }
        public string Str_nombre { get; set; } = String.Empty;
        public string Str_telefono { get; set; } = String.Empty;
        public string Str_direccion { get; set; } = String.Empty;
        public string Str_correo { get; set; } = String.Empty;
        public List<ProductoDto> Productos { get; set; }
        public List<CategoriaDto> Categorias { get; set; } 
        public List<MarcaDto> Marcas { get; set; }
    }
}