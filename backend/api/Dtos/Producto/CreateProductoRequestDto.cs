using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Producto
{
    public class CreateProductoRequestDto
    {
        [Required]
        [MaxLength(200000, ErrorMessage = "La ruta de imagen no puede ser mayor de 200000 caracteres")]
        public string Str_ruta_imagen { get; set; } = String.Empty;

        [Required]
        [MaxLength(60, ErrorMessage = "El nombre del producto no puede ser mayor de 60 caracteres")]
        public string Str_nombre { get; set; } = String.Empty;

        [Required]
        [MaxLength(200, ErrorMessage = "La descripcion del producto no puede ser mayor de 200 caracteres")]
        public string Str_descripcion { get; set; } = String.Empty;

        [Required]
        [Range(1, 100)]
        public int Int_cantidad_minima { get; set; }
        [Required]
        public decimal Dec_costo { get; set; } = 0;
        public decimal Dec_costo_PPP { get; set; }
        [Required]
        public int Int_iva { get; set; }
        public decimal Dec_precio_mayorista { get; set; }
        public decimal Dec_precio_minorista { get; set; }
        public bool Bool_borrado { get; set; } = false;
    }
}