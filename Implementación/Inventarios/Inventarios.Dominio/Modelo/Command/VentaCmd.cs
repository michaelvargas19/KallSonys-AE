using System;
using System.Collections.Generic;
using System.Text;

namespace Inventarios.Dominio.Modelo.Command
{
    public class VentaCmd
    {

        public string Referencia { get; set; }

        public string SKU { get; set; }
        
        public int Unidades { get; set; }


    }
}
