using Ordenes.Dominio.Modelo;
using Ordenes.Dominio.Modelo.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordenes.Dominio.Models
{
    [BsonCollection("Pedido_Ref_Estados")]
    public class Pedido_Ref_Estados : Document
    {
        public string codigo_estado_pedido { get; set; }
        public string descripcion_estado_pedido { get; set; }
    }
}
