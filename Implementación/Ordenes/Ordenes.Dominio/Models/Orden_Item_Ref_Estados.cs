using Ordenes.Dominio.Modelo;
using Ordenes.Dominio.Modelo.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordenes.Dominio.Models
{
    [BsonCollection("Orden_Item_Ref_Estados")]
    public class Orden_Item_Ref_Estados : Document
    {
        public string codigo_estado_item { get; set; }
        public string descripcion_estado_item { get; set; }
    }
}
