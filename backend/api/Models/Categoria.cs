using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Categoria
    {
        //[Key]
        public int Id { get; set; }
        public string Str_descripcion { get; set; } = String.Empty;
        //[ForeignKey("Proveedor")]
        public int? ProveedorId { get; set; }
        public Proveedor? Proveedor { get; set; }
    }
}