using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaSocial.Models
{
    public class Prestaciones
    {
        [Key]
        public int PrestacionesID { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }

    }
}
