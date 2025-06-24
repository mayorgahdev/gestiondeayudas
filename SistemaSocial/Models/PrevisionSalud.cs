using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaSocial.Models
{
    public class PrevisionSalud
    {
        [Key]
        public int PrevisionSaludID { get; set; }
        public string Nombre { get; set; }
    }
}
