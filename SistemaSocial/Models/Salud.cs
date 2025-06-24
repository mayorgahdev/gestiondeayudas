using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaSocial.Models
{
    public class Salud
    {
        [Key]
        public int SaludID { get; set; }


        [Display(Name = "Nombre Usuario.")]
        public int ClientesID { get; set; }
        public Clientes Clientes { get; set; }


        [Display(Name = "Institución de Control.")]
        public int ProcedenciaID { get; set; }
        public Procedencia Procedencia { get; set; }

        [Display(Name = "Discapacidad.")]
        public int DiscapacidadID { get; set; }
        public Discapacidad Discapacidad { get; set; } // <- INYECCION DE DEPENDENCIA

        [Display(Name = "Acreditación.")]
        public int AcreditacionID { get; set; }
        public Acreditacion Acreditacion { get; set; } // <- INYECCION DE DEPENDENCIA

        [Display(Name = "Diagnóstico.")]
        public string Diagnostico { get; set; }

        [Display(Name = "Observación Asociada.")]
        public string Observacion { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "N° del Grupo Familiar:")]
        public int GrupoFamiliar { get; set; }
    }
}
