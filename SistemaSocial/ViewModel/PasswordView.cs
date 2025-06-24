using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaSocial.ViewModel
{
    public class PasswordView
    {
        [Required(ErrorMessage = "Correo Obligatorio")]
        [EmailAddress]
        public string Correo { get; set; }
    }
}
