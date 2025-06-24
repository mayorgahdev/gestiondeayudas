using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaSocial.Models
{
    public class Acreditacion
    {
        [Key]
        public int AcreditacionID { get; set; }
        public string Nombre { get; set; }

        public int DiscapacidadID { get; set; }
        public Discapacidad Discapacidad { get; set; }
    }
}
