using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaSocial.Models
{
    public class Cuenta
    {
        [Key]
        public int CuentaID { get; set; }
        public string Codigo { get; set; }

        [Display(Name = "Programa")]
        public int ProgramaID { get; set; }
        public Programa Programa { get; set; }
    }
}
