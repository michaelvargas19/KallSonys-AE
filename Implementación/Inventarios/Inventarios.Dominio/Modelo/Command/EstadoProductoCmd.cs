using System;
using System.Collections.Generic;
using System.Text;

namespace Inventarios.Dominio.Modelo.Command
{
    public class EstadoProductoCmd
    {

        public string SKU { get; set; }

        public int NivelInventario { get; set; }

        public bool Estado { get; set; }

        public bool EnAlmacen { get; set; }


    }
}
