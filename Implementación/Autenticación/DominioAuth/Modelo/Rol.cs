using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DominioAuth.Modelo
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

        //              [JSON]
        [Required]
        [NotMapped]
        public string Usuario { get; set; }

    }

}
