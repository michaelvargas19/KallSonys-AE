using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Autenticacion.Dominio.Modelo.Command
{
    public class TokenJWT
    {
        [Required]
        [MaxLength(450)]
        public string IdAplicacion { get; set; }

        [Required]
        public string Token { get; set; }

        public bool TokenValido { get; set; }

    }
}
