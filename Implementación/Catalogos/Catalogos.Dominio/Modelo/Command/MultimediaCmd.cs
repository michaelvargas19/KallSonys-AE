using Catalogos.Infraestructura.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalogos.Dominio.Modelo.Command
{
    public class MultimediaCmd
    {
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string url { get; set; }

        public TIPO_MULTIMEDIA Tipo { get; set; }


    }
}
