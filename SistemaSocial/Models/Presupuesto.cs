using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaSocial.Models
{
    public class Presupuesto
    {
        [Key]
        public int PresupuestoID { get; set; }

        [Display(Name = "Presupuesto Inicial")]
        public int PresInicial { get; set; }

        [Display(Name = "Presupuesto Vigente")]
        public int PresVigente { get; set; }


        [Display(Name = "Cuenta")]
        public int CuentaID { get; set; }
        public Cuenta Cuenta { get; set; }
    }
}
