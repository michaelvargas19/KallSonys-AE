using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DominioAuth.Modelo
{
    public class Configuracion
    {
        [Required]
        [MaxLength(450)]
        public string IdAplicacion { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Algoritmo { get; set; }

        [MinLength(16)]
        [StringLength(Int32.MaxValue, ErrorMessage = "La longitud mínima es 16.", MinimumLength = 16)]
        public string LlaveSecreta { get; set; }

        [Required]
        public double MinutosDeVida { get; set; }

        [Required]
        public string Issuer { get; set; }

        [Required]
        public string Audience { get; set; }

        [Required]
        [DefaultValue(nameof(TimeSpan.Zero))]
        public TimeSpan ClockSkew { get; set; }

        [Required]
        public ICollection<string> Roles { get; set; }
    }
}
