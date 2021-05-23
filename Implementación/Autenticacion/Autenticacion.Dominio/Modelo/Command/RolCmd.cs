using System;
using System.Collections.Generic;
using System.Text;

namespace Autenticacion.Dominio.Modelo.Command
{
    public class RolCmd
    {
        public string? Id { get; set; }
        public string Nombre { get; set; }
        public string NomNormalizado { get; set; }
        public string Display { get; set; }
        public string Descripcion { get; set; }
        public string IdAplicacion { get; set; }

    }
}
