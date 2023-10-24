using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication_ADO.NET.Models
{
    public class Area
    {
        [Key]
        public int id_area { get; set; }

        [Required]
        public string descripcion { get; set; }

        [Required]
        public int id_cliente { get; set; }

        [Required]
        public char enu_estado_registro { get; set; }

        public string usuario_creacion { get; set; }

        public DateTime fecha_creacion { get; set; }

        public string usuario_modificacion { get; set; }

        public DateTime fecha_modificacion { get; set; }

        public string nombre_cliente { get; set; }
    }
}