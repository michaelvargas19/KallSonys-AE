using Catalogos.Dominio.Modelo.Settings;
using Catalogos.Dominio.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalogos.Dominio.Modelo
{
    [BsonCollection("Catalogos")]
    public class Catalogo : Document
    {

        public string Nombre { get; set; }

    }
}
