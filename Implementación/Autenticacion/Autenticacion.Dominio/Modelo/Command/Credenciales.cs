using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Autenticacion.Dominio.Modelo.Command
{
    public class Credenciales
    {
        [Required]
        public string Contrasena { get; set; }

        [Required]
        public string ConfirContrasena { get; set; }


        public bool CredencialesValidas()
        {
            return (Contrasena.CompareTo(ConfirContrasena) == 0);
        }
    }
}
