using Catalogos.Infraestructura.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalogos.Dominio.Modelo.Queries
{
    public class DescuentoQuery
    {

        public string Nombre { get; set; }

        public double Porcentaje { get; set; }

        public string Descripcion { get; set; }

        public List<MEDIO_PAGO> MediosDePago { get; set; }

    }

}
