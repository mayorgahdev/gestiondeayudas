using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaSocial.Models
{
    public class Ingresos
    {
        [Key]
        public int IngresosID { get; set; }

        [Display(Name = "Nombre Usuario.")]
        public int ClientesID { get; set; }
        public Clientes Clientes { get; set; } // <- INYECCION DE DEPENDENCIA


        [Display(Name = "Total de Ingresos mensuales.")]
        public int IngresosMensuales { get; set; }


        [Display(Name = "Observación.")]
        public string Descripcion { get; set; }


        [Display(Name = "Fecha de Elaboración.")]
        public DateTime FechaElaboracion { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "N° del Grupo Familiar:")]
        public int GrupoFamiliar { get; set; }
    }
}
