using Inventarios.Infraestructura.Entities;
using System.Collections.Generic;

namespace Inventarios.Dominio.Modelo.Queries
{
    public class DescuentoQuery
    {

        public string Nombre { get; set; }

        public double Porcentaje { get; set; }

        public string Descripcion { get; set; }

        public List<MEDIO_PAGO> MediosDePago { get; set; }

    }

}
