using Catalogos.Infraestructura.Entities;

namespace Catalogos.Dominio.Modelo.Queries
{
    public class MultimediaQuery
    {
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string url { get; set; }

        public TIPO_MULTIMEDIA Tipo { get; set; }
        
        public string NombreTipo { get; set; }

    }
}
