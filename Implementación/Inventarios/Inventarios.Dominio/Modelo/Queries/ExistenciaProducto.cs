using System;
using System.Collections.Generic;
using System.Text;

namespace Inventarios.Dominio.Modelo.Queries
{
    public class ExistenciaProducto
    {
        public string CodigoProducto { get; set; }
        
        public string sku { get; set; }

        public bool Disponible { get; set; }

        public int CantidadDisponibles { get; set; }
        

    }
}
