using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Autenticacion.Infraestructura.Entities
{
    [Table("AspNetTiposAutenticacion")]
    public class TipoAutenticacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTipo { get; set; }

        [Required]
        public string Autenticacion { get; set; }

        public bool EsDirectorioActivo { get; set; }

        public int? IdAD { get; set; }

        [ForeignKey("IdTipoAuth")]
        public ICollection<Usuario> Usuarios { get; set; }
    }
}
