using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaSocial.Models
{
    public class MedioAyuda
    {
        [Key]
        public int MedioID { get; set; }

        [Display(Name = "Nombre Usuario.")]
        public int ClientesID { get; set; }
        public Clientes Clientes { get; set; } // <- INYECCION DE DEPENDENCIA 

        [Display(Name = "Orden.")]
        public string Orden { get; set; }

        [Display(Name = "Monto.")]
        public int Monto { get; set; }

        [Display(Name = "Descripción.")]
        public string Descripcion { get; set; }

        [Display(Name = "N° Informe.")]
        public int NumInforme { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "N° de Cuenta.")]
        public int NumMedio { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "N° del Grupo Familiar:")]
        public int GrupoFamiliar { get; set; }
    }
}
