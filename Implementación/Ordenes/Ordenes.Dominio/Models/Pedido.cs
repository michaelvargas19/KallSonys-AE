using Ordenes.Dominio.Modelo;
using Ordenes.Dominio.Modelo.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordenes.Dominio.Models
{
    [BsonCollection("Pedidos")]
    public class Pedido : Document
    {
        public List<string> id_orden { get; set; }
        public string id_estado_pedido { get; set; }
        public string detalles_pedido { get; set; }
        public DateTime? fecha_pedido { get; set; }
        public DateTime? fecha_actualizacion{ get; set; }

    }
}
