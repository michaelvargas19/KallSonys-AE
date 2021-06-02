using MongoDB.Bson;
using Ordenes.Dominio.Modelo;
using Ordenes.Dominio.Modelo.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordenes.Dominio.Models
{
    [BsonCollection("Orden_Items")]
    public class Orden_Items : Document
    {
        //public ObjectId _id { get; set; }
        public string id_producto { get; set; }
        //public string id_orden { get; set; }
        public string estado_item { get; set; }
        public int cantidad { get; set; }
        public int precio_unitario { get; set; }
    }
}
