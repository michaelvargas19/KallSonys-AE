using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Autenticacion.Dominio.Modelo.Queries
{
    public class AplicacionQuery
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
        
        public string NombreAlgoritmo { get; set; }

        public double? MinutosDeVida { get; set; }

        public DateTime? FechaExpiracionLlave { get; set; }

        public ICollection<RolQuery> Roles { get; set; }



    }
}
