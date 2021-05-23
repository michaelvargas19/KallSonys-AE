using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Autenticacion.Infraestructura.Entities
{
    public class Usuario : IdentityUser<int>
    {
        [Required(ErrorMessage = "Nombres requeridos.")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Apellidos requeridos.")]
        public string Apellidos { get; set; }

        public string? Identificacion { get; set; }

        [Required]
        [ForeignKey("TipoAutenticacion")]
        public int IdTipoAuth { get; set; }

        public TipoAutenticacion TipoAutenticacion { get; set; }
        public string Organizacion { get; set; }

        public string Cargo { get; set; }

        public string Description { get; set; }

        public bool EsExterno { get; set; }

        [NotMapped]
        public ICollection<Rol> Roles { get; set; }


    }
}
