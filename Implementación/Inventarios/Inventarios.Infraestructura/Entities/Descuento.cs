using Inventarios.Infraestructura.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventarios.Infraestructura.Entities
{
    public class Descuento
    {
        public string Nombre { get; set; }

        public double Porcentaje { get; set; }

        public string Descripcion { get; set; }

        public List<MEDIO_PAGO> MediosDePago { get; set; }


    }
}
