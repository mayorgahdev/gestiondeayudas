using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaSocial.Models
{
    public class Socioeconomico
    {
        [Key]
        public int SocioeconomicoID { get; set; }


        [Display(Name = "Nombre Usuario.")]
        public int ClientesID { get; set; }
        public Clientes Clientes { get; set; } // <- INYECCION DE DEPENDENCIA 


        [Display(Name = "Actividad.")]
        public string Actividad { get; set; }


        [Display(Name = "Ingreso de Actividad.")]
        public int IngresoActividad { get; set; }


      /*|||||||||||||||||||||||||||||||||||||||||||||*/
        [Display(Name = "Otros Ingresos.")]
        public int OtrosIngresos1 { get; set; }
        [Display(Name = "Tipo de Ingreso.")]
        public string TipoIngreso1 { get; set; }

      /*|||||||||||||||||||||||||||||||||||||||||||||*/
        [Display(Name = "Otros Ingresos.")]
        public int OtrosIngresos2 { get; set; }
        [Display(Name = "Tipo de Ingreso.")]
        public string TipoIngreso2 { get; set; }
      /*|||||||||||||||||||||||||||||||||||||||||||||*/


        [Display(Name = "Tipo de Pensión.")]
        public int TipoPensionID { get; set; }
        public TipoPension TipoPension { get; set; } // <- INYECCION DE DEPENDENCIA 


        [Display(Name = "Otros Ingresos.")]
        public int OtrosIngresos3 { get; set; }



        [Display(Name = "Tipo de Subsidio.")]
        public int TipoSubsidioID { get; set; }
        public TipoSubsidio TipoSubsidio { get; set; } // <- INYECCION DE DEPENDENCIA 


        [Display(Name = "Otros Ingresos.")]
        public int OtrosIngresos4 { get; set; }


        [Display(Name = "Total de Ingresos: ")]
        public int TotalIngresos { get; set; }


        [Display(Name = "Observación.")]
        public string Observacion { get; set; }



        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "N° del Grupo Familiar:")]
        public int GrupoFamiliar { get; set; }
    }
}
