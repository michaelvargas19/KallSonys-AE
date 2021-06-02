using System;
using System.Collections.Generic;
using System.Text;

namespace Ordenes.Dominio.Models
{
    public class ProductoResponse
    {
        public string Referencia { get; set; }

        public string SKU { get; set; }

        public int Unidades { get; set; }
    }
}
