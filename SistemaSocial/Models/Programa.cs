using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaSocial.Models
{
    public class Programa
    {
        [Key]
        public int ProgramaID { get; set; }
        public string Nombre { get; set; }


        [Display(Name = "Área de Gestión")]
        public int AreaGestionID { get; set; }
        public AreaGestion AreaGestion { get; set; }

    }
}
