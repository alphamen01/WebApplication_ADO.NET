using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication_ADO.NET.Models
{
    public class Cliente
    {
        [Key]
        public int id_cliente {  get; set; }

        [Required]
        public string nombre { get; set; }

        [Required]
        public string direccion { get; set;}

        [Required]
        public string telefono { get; set;}
    }
}