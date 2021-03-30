using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DominioAuth.Request
{
    public class LoginRequest
    {
        [Required]
        [MaxLength(450)]
        public string IdAplicacion { get; set; }

        [Required]
        public string Usuario { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string Contrasena { get; set; }

        public bool datosParaLogin()
        {

            if ((IdAplicacion.Length > 0) && (Usuario.Length > 0) && (Contrasena.Length > 0))
            {
                return true;
            }

            return false;
        }

    }
}
