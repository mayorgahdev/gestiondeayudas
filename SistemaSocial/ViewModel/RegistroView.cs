using Microsoft.AspNetCore.Http;
using SistemaSocial.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaSocial.ViewModel
{
    public class RegistroView
    {
        [Required]
        [Display(Name = "Run.")]
        public string Rut { get; set; }


        [Required]
        [Display(Name = "Nombre.")]
        public string Nombre { get; set; }


        [Required]
        [Display(Name = "Apellidos.")]
        public string Apellido { get; set; }


        [Required]
        [Display(Name = "Tipo de Profesional.")]
        public int TipoUsuarioID { get; set; }
        public TipoUsuario TipoUsuario { get; set; }


        [Required]
        [Display(Name = "Correo Electrónico.")]
        [EmailAddress(ErrorMessage = "Dirección de Correo Electrónico Inválida.")]
        public string Correo { get; set; }


        [Required]
        [Display(Name = "Nombre de Usuario.")]
        public string Usuario { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña.")]
        public string Contraseña { get; set; }


        [Required]
        [Display(Name = "Profesión.")]
        public int ProfesionID { get; set; }
        public Profesion Profesion { get; set; }

        [Required]
        [Display(Name = "Teléfono.")]
        public string Telefono { get; set; }

        public string Imagen { get; set; }

        [NotMapped]
        [Display(Name = "Imagen.")]
        public IFormFile archivoImagen { get; set; }

        [Display(Name = "Marca de Agua.")]
        public string Marca { get; set; }

        [Display(Name = "¿Posee Responsabilidad de Firma?")]
        public string Firma { get; set; }
    }
}
