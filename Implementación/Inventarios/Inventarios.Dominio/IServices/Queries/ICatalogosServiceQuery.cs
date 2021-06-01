using Inventarios.Dominio.Modelo.Queries;
using System.Collections.Generic;

namespace Inventarios.Dominio.IServices.Queries
{
    public interface ICatalogosServiceQuery
    {
        IEnumerable<CatalogoQuery> VerCatalogos();
        CatalogoQuery verCatalogoPorCodigo(string codigo);
        IEnumerable<CatalogoQuery> verPaginacion(int skip, int take);
    }
}
