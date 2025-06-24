using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaSocial.Models
{
    public class Clientes
    {
        [Key]
        public int ClientesID { get; set; }


        [Display(Name = "Tipo de Usuario.")] // <- (Requirente - Solicitante - Ambos - Otro)
        public string TipoDeCliente { get; set; }


        [Display(Name = "Run.")]
        public string Rut { get; set; }


        [Required]
        [Display(Name = "Nombre.")]
        public string Nombres { get; set; }


        [Required]
        [Display(Name = "Apellido Paterno.")]
        public string ApellidoPaterno { get; set; }


        [Display(Name = "Apellido Materno.")]
        public string ApellidoMaterno { get; set; }


        [Display(Name = "Fecha de Nacimiento.")]
        public DateTime FechaNacimiento { get; set; }


        [Display(Name = "Edad.")]
        public int Edad { get; set; }


        [Required]
        [Display(Name = "Nacionalidad.")]
        public int NacionalidadID { get; set; }
        public Nacionalidad Nacionalidad { get; set; }


        [Required]
        [Display(Name = "Dirección.")]
        public string Direccion { get; set; }


        [Display(Name = "Teléfono.")]
        public string Telefono { get; set; }


        [EmailAddress(ErrorMessage = "Dirección de Correo Electrónico Inválida.")]
        [Display(Name = "Correo.")]
        public string Correo { get; set; }


        [Display(Name = "Estado Civil.")]
        public int EstadoCivilID { get; set; }
        public EstadoCivil EstadoCivil { get; set; }


        [Display(Name = "Previsión Social.")]
        public int PrevisionSocialID { get; set; }
        public PrevisionSocial PrevisionSocial { get; set; }


        [Display(Name = "Previsión Salud.")]
        public int PrevisionSaludID { get; set; }
        public PrevisionSalud PrevisionSalud { get; set; }


        [Display(Name = "Índice de Escolaridad.")]
        public int IndiceEscolaridadID { get; set; }
        public IndiceEscolaridad IndiceEscolaridad { get; set; }


        [Required]
        [Display(Name = "Parentesco.")]
        public int ParentescoID { get; set; }
        public Parentesco Parentesco { get; set; }


        [Display(Name = "Ocupación.")]
        public string Ocupacion { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "N° del Grupo Familiar:")]
        public int GrupoFamiliar { get; set; }
    }
}
