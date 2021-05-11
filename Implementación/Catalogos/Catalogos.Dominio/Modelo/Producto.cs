using Catalogos.Dominio.Modelo.Settings;
using Catalogos.Dominio.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalogos.Dominio.Modelo
{
    [BsonCollection("Productos")]
    public class Producto : Document
    {
        public string Nombre { get; set; }
        public string SKU { get; set; }
    }
}
