using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaSocial.ViewModel
{
    public class LoginViewModel
    {

        [Required]
        [Display(Name = "Correo Electrónico")]
        public string Correo { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Contraseña { get; set; }
    }
}
