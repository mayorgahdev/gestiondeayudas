using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaSocial.Models
{
    public class Cronicos
    {
        [Key]
        public int CronicosID { get; set; }

        [Display(Name = "N° de Crónicos de la Familia. ")]
        public int NCronicos { get; set; }

        [Display(Name = "N° de Embarazadas de la Familia.")]
        public int NEmbarazadas { get; set; }

        [Display(Name = "Registro Social de Hogares.")]
        public int RSHID { get; set; }
        public RSH RSH { get; set; } // <- INYECCION DE DEPENDENCIA

        [Display(Name = "Tramo.")]
        public int TramoID { get; set; }
        public Tramo Tramo { get; set; } // <- INYECCION DE DEPENDENCIA

        [Display(Name = "Observación.")]
        public string ObservacionGrupo { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "N° del Grupo Familiar:")]
        public int GrupoFamiliar { get; set; }
    }
}
