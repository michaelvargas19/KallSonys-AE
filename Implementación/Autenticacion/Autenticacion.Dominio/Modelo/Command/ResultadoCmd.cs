using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Autenticacion.Dominio.Modelo.Command
{
    public class ResultadoCmd
    {
        public bool Exitoso { get; set; }
        public string Proceso { get; set; }
        public string Mensaje { get; set; }

    }
}
