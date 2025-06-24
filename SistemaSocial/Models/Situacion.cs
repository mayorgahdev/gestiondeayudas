using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaSocial.Models
{
    public class Situacion
    {
        [Key]
        public int SituacionID { get; set; }

        [Display(Name = "Nombre Usuario.")]
        public int ClientesID { get; set; }
        public Clientes Clientes { get; set; }

        [Display(Name = "Situación Actual.")]
        public string SituacionActual { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "N° del Grupo Familiar:")]
        public int GrupoFamiliar { get; set; }
    }
}
