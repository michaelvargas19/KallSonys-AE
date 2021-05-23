using Autenticacion.Dominio.Modelo.Queries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Autenticacion.Dominio.Modelo.Command
{
    public class AplicacionCmd
    {
        public string IdAplicacion { get; set; }

        public string Nombre { get; set; }

        [EmailAddress]
        public string EmailContacto { get; set; }

        public bool PermiteJWT { get; set; }
        
        [MinLength(16)]
        [StringLength(Int32.MaxValue, ErrorMessage = "El mínimo son 16 caracteres.", MinimumLength = 16)]
        public string LlaveSecreta { get; set; }

        public string AlgoritmoDeSeguridad { get; set; }

        public double? MinutosDeVida { get; set; }

        public DateTime? FechaExpiracionLlave { get; set; }

        public ICollection<RolQuery> Roles { get; set; }



        public bool EsValido()
        {
            if (PermiteJWT)
            {
                if ( (AlgoritmoDeSeguridad != null) && (LlaveSecreta != null) && 
                     (MinutosDeVida != null) && (FechaExpiracionLlave != null) )
                {
                    return true;
                }

                return false;
            }

            return true;
        }

    }
}
