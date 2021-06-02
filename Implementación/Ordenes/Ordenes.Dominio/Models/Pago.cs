using Ordenes.Dominio.Modelo;
using Ordenes.Dominio.Modelo.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ordenes.Dominio.Models
{
    [BsonCollection("Pagos")]
    public class Pago : Document
    {
        [Required(ErrorMessage = "Valor obligatorio")]
        public string numero_pedido { get; set; }
        public DateTime? fecha_pago { get; set; }

        [Required(ErrorMessage = "Valor obligatorio")]
        [Range(0.0, Double.MaxValue, ErrorMessage = "Width must be a valid number")]
        public double valor_pagado { get; set; }
    }
}
