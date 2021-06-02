using Ordenes.Dominio.Modelo;
using Ordenes.Dominio.Modelo.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ordenes.Dominio.Models
{
    [BsonCollection("Envios")]
    public class Envio : Document
    {
        [Required(ErrorMessage = "Valor obligatorio")]
        public string id_orden { get; set; }
        [Required(ErrorMessage = "Valor obligatorio")]
        public string id_pedido { get; set; }

        [Required(ErrorMessage = "Valor obligatorio")]
        [Range(0.0, Double.MaxValue, ErrorMessage = "Width must be a valid number")]
        public int numero_rastreo { get; set; }
        public DateTime? fecha_envio { get; set; }
        public DateTime? fecha_actualizacion { get; set; }
        public string detalles_envio { get; set; }
    }
}
