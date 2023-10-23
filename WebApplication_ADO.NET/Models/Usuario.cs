using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication_ADO.NET.Models
{
    public class Usuario
    {
        public int id_usuario { get; set; }

        public string correo { get; set; }

        public string clave { get; set; }

        public string confirmar_clave { get; set; }
    }
}