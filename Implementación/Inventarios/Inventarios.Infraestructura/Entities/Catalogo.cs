using Inventarios.Infraestructura.SettingsDB;
using System;

namespace Inventarios.Infraestructura.Entities
{
    [BsonCollection("Catalogos")]
    public class Catalogo : Document
    {

        public string Nombre { get; set; }

        public string CodigoCatalogo { get; set; }
        
        public Proveedor Proveedor { get; set; }

        public Multimedia Multimedia { get; set; }
            
        public string Descripcion { get; set; }

        public string[] Categorias { get; set; }

        public bool Estado { get; set; }

        public bool IndExterno { get; set; }

        public DateTime FechaInicio { get; set; }
        
        public DateTime FechaFin { get; set; }

        public string[] Sinonimos { get; set; }

        public double Calificacion { get; set; }

    }
}
