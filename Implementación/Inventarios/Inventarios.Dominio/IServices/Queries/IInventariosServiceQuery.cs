using Inventarios.Dominio.Modelo.Queries;
using System.Collections.Generic;

namespace Inventarios.Dominio.IServices.Queries
{
    public interface IInventariosServiceQuery
    {

        IEnumerable<ExistenciaProducto> ConsultarDisponibilidad(string[] sku);

        ExistenciaProducto ConsultarDisponibilidadSKU(string sku);

    }
}
