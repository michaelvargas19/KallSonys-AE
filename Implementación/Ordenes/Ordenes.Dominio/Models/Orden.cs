using Ordenes.Dominio.Modelo;
using Ordenes.Dominio.Modelo.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ordenes.Dominio.Models
{
    [BsonCollection("Orden")]
    public class Orden : Document
    {
        [Required(ErrorMessage = "Valor obligatorio")]
        public string id_cliente { get; set; }
        public string estado_orden { get; set; }

        [Required(ErrorMessage = "Valor obligatorio")]
        public string id_proveedor { get; set; }
        public string detalles_orden { get; set; }
        public DateTime? fecha_orden { get; set; }
        public DateTime? fecha_actualizacion { get; set; }
        

        [Required(ErrorMessage = "Valor obligatorio")]
        public List<Orden_Items> items { get; set; }
     
    }
}
