using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace DominioAuth.Modelo
{
    public class Usuario : IdentityUser<int>
    {
        [Required(ErrorMessage = "FirstName is required.")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "LastName is required.")]
        public string Apellidos { get; set; }

        public string? Identificacion { get; set; }

        [Required]
        [ForeignKey("TipoAutenticacion")]
        public int IdTipoAuth { get; set; }

        [JsonIgnore]
        public TipoAutenticacion TipoAutenticacion { get; set; }
        public string Organizacion { get; set; }

        public string Cargo { get; set; }

        public string Description { get; set; }

        public bool EsExterno { get; set; }

        //              [JSON]
        [Required]
        [NotMapped]
        public string UsuarioRequest { get; set; }
    }
}
