using Catalogos.Dominio.Modelo.Queries;
using System.Collections.Generic;

namespace Catalogos.Dominio.IServices.Queries
{
    public interface ICatalogosServiceQuery
    {
        IEnumerable<CatalogoQuery> VerCatalogos();
        CatalogoQuery verCatalogoPorCodigo(string codigo);
        IEnumerable<CatalogoQuery> verPaginacion(int skip, int take);
    }
}
