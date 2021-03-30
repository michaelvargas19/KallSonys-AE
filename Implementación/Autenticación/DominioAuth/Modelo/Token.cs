using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DominioAuth.Modelo
{
    [Table("AspNetTokensValidacion")]
    public class Token : IdentityUserToken<int>
    {
        [Required]
        [ForeignKey("Aplicacion")]
        [DataType(DataType.Text)]
        public string IdAplicacion { get; set; }

        public Aplicacion Aplicacion { get; set; }


        [Required]
        public int LongitudToken { get; set; }

        [Required]
        public int MinutosDeVida { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        [Required]
        public DateTime FechaExpiracion { get; set; }


        [Required]
        public string FirmaJWT { get; set; }

        public bool tokenVigente()
        {
            if (FechaExpiracion <= DateTime.Now)
            {
                return false;
            }
            return true;


        }
    }

}
