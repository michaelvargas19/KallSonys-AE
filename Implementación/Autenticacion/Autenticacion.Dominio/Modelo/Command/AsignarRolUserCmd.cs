using System;
using System.Collections.Generic;
using System.Text;

namespace Autenticacion.Dominio.Modelo.Command
{
    public class AsignarRolUserCmd
    {
        public int IdRol { get; set; }
        
        public string Usuario { get; set; }

    }
}
