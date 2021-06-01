using System;
using System.Collections.Generic;
using System.Text;

namespace Inventarios.Dominio.Modelo.Queries
{
    public class CatalogoQuery
    {
        public string Id { get; set; }

        public string CodigoCatalogo { get; set; }

        public string Nombre { get; set; }
        
        public ProveedorQuery Proveedor { get; set; }

        public MultimediaQuery Multimedia { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaFin { get; set; }

        public double Calificacion { get; set; }

        public bool IndExterno { get; set; }

    }
}
