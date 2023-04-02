using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Se requiere nombre de usuario")]
        [MinLength(3, ErrorMessage = "La longitud minima son 3 caracteres")]
        [MaxLength(20, ErrorMessage = "La longitud minima son 20 caracteres")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Se requiere contraseña")]
        [MinLength(8, ErrorMessage = "La longitud mínima son 8 caracteres")]
        [MaxLength(32, ErrorMessage = "La longitud minima son 32 caracteres")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }

        public string Validar { get; set; }
    }
}
