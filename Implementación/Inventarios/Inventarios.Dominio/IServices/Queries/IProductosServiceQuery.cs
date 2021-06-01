using Inventarios.Dominio.Modelo.Queries;
using System.Collections.Generic;

namespace Inventarios.Dominio.IServices.Queries
{
    public interface IProductosServiceQuery
    {
        IEnumerable<ProductoQuery> verPaginacion(int skip, int take);

        IEnumerable<ProductoQuery> verRankingFullText(string texto, int skip, int take);

        IEnumerable<ProductoQuery> verRankingCatalogo(string codigoCatalogo, int skip, int take);

        IEnumerable<ProductoQuery> verRankingMarca(string marca, int skip, int take);

        ProductoQuery verProductoPorCodigo(string codigo);

        ProductoQuery verProductoPorSKU(string codigo);

    }
}
