using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaSocial.Models
{
    public class Peticion
    {  
        [Key]
        public int PeticionID { get; set; }

        [Display(Name = "Petición")]
        public string Nombre { get; set; }
    }
}
