using System;
using System.Collections.Generic;
using System.Text;

namespace Autenticacion.Dominio.Modelo.Command
{
    public class LogCmd
    {

        public string Tipo { get; set; }

        public string Usuario { get; set; }

        public string Request { get; set; }

        public string Response { get; set; }

        public string Aplicacion { get; set; }

        public string Metodo { get; set; }

        public string Entidad { get; set; }

        public bool EsExcepcion { get; set; }

        public string Mensaje { get; set; }

        public string Parametros { get; set; }

    }
}
