using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Autenticacion.Infraestructura.Entities
{
    [Table("AspNetAlgoritmosDeSeguridad")]
    public class AlgoritmoDeSeguridad
    {
        [Key]
        [Required]
        public string Algoritmo { get; set; }

        [Required]
        public string Valor { get; set; }

        [ForeignKey("AlgoritmoDeSeguridad")]
        public ICollection<Aplicacion> Aplicaciones { get; set; }
    }
}
