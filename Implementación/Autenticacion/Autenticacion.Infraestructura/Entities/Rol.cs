using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Autenticacion.Infraestructura.Entities
{
    public class Rol : IdentityRole<int>
    {
        [Required]
        public string Display { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        [ForeignKey("Aplicacion")]
        public string IdAplicacion { get; set; }
        
        public Aplicacion Aplicacion { get; set; }

    }

}
