using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace DominioAuth.Modelo
{
    public class Credenciales
    {
        [Required]
        public string Contrasena { get; set; }

        [Required]
        public string ConfirContrasena { get; set; }


        public bool CredencialesValidas()
        {
            return ( Contrasena.CompareTo(ConfirContrasena) == 0 );
        }
    }
}
