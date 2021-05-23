using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Autenticacion.Dominio.Modelo.Command
{
    public class LoginResponse
    {
        [Required]
        public bool Autenticacion { get; set; }

        public TokenJWT TokenJWT { get; set; }

        [Required]
        public string Mensaje { get; set; }

        [Required]
        public bool Bloqueado { get; set; }

        public string URLdesbloqueo { get; set; }

    }

}
