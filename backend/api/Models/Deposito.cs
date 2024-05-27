using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Deposito
    {
        //[Key]
        public int Id { get; set; }
        public string Str_nombre { get; set; } = String.Empty;
        public string Str_direccion { get; set; } = String.Empty;
        public string Str_telefono { get; set; } = String.Empty;
        public string Str_encargado { get; set; } = String.Empty;
        public string Str_telefonoEncargado { get; set; } = String.Empty;
        //[ForeignKey("Ferreteria")]
        public int? FerreteriaId { get; set; }
        public Ferreteria? Ferreteria { get; set; }
        public List<Movimiento> Movimientos { get; set; } = [];
        public List<Producto> Productos { get; set; } = [];
    }
}