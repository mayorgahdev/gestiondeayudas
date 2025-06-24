using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaSocial.Models
{
    public class InformeSocial
    {
        [Key]
        public int InformeSocialID { get; set; }


        [Display(Name = "Nombre Usuario.")]
        public int ClientesID { get; set; }
        public Clientes Clientes { get; set; } // <- INYECCION DE DEPENDENCIA

        //PROFESIONAL ASIGNADO
        [Display(Name = "Usuario.")]
        public string UsuarioID { get; set; }
        public Usuario Usuario { get; set; }

        [Display(Name = "N° Informe.")]
        public int NumInforme { get; set; }

        [Display(Name = "Fecha de Elaboración.")]
        public DateTime FechaElaboracion { get; set; }

        [Display(Name = "Síntesis y Opinión del Profesional.")]
        public string Sintesis { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "N° del Grupo Familiar:")]
        public int GrupoFamiliar { get; set; }
    }
}
