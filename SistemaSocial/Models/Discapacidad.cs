using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaSocial.Models
{
    public class Discapacidad
    {
        [Key]
        public int DiscapacidadID { get; set; }
        public string Nombre { get; set; }
    }
}
