using Ordenes.Dominio.Modelo;
using Ordenes.Dominio.Modelo.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordenes.Dominio.Models
{
    [BsonCollection("Orden_Ref_Estados")]
    public class Orden_Ref_Estados :Document
    {
        public string codigo_estado_orden { get; set; }
        public string descripcion_estado_orden { get; set; }
    }
}
