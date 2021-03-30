using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace DominioAuth.Modelo
{
    [Table("AspNetAplicacion")]
    public class Aplicacion
    {

        [Key]
        [Required]
        [DataType(DataType.Text)]
        public string IdAplicacion { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailContacto { get; set; }

        [Required]
        public bool Estado { get; set; }

        [Required]
        public bool PermiteJWT { get; set; }

        [ForeignKey("Algoritmo")]
        public string AlgoritmoDeSeguridad { get; set; }

        [JsonIgnore]
        public AlgoritmoDeSeguridad Algoritmo { get; set; }

        [MinLength(16)]
        [StringLength(Int32.MaxValue, ErrorMessage = "El mínimo son 16 caracteres.", MinimumLength = 16)]
        public string LlaveSecreta { get; set; }

        public double? MinutosDeVida  { get; set; }

        public DateTime? FechaExpiracionLlave { get; set; }

        [Required]
        public bool EstadoLlave { get; set; }

        [ForeignKey("IdAplicacion")]
        //[JsonIgnore]
        public ICollection<Rol> Roles { get; set; }



        //              [JSON]
        [Required]
        [NotMapped]
        public string Usuario { get; set; }


    }
}
