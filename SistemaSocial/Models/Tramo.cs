using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaSocial.Models
{
    public class Tramo
    {
        [Key]
        public int TramoID { get; set; }
        public string Nombre { get; set; }

        public int RSHID { get; set; }
        public RSH RSH { get; set; }
    }
}
