using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaSocial.Models
{
    public class Usuario : IdentityUser
    {
        [Required]
        [Display(Name = "Run.")]
        public string Rut { get; set; }

        [Required]
        [Display(Name = "Nombre.")]
        public string Nombre { get; set; }


        [Display(Name = "Apellido.")]
        public string Apellido { get; set; }


        [Required]
        [Display(Name = "Tipo de Usuario.")]
        public int TipoUsuarioID { get; set; }
        public TipoUsuario TipoUsuario { get; set; }


        [Required]
        [Display(Name = "Profesion.")]
        public int ProfesionID { get; set; }
        public Profesion Profesion { get; set; }

        public string Imagen { get; set; }

        [NotMapped]
        [Display(Name = "Imagen (Opcional).")]
        public IFormFile archivoImagen { get; set; }


        [Display(Name = "Marca de Agua.")]
        public string Marca { get; set; }


        [Display(Name = "¿Posee Responsabilidad de Firma?")]
        public string Firma { get; set; }
    }
}
