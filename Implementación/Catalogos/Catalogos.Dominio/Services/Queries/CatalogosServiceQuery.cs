using Catalogos.Dominio.IServices.Queries;
using Catalogos.Dominio.IUnitOfWorks;
using Catalogos.Dominio.Modelo.Queries;
using Catalogos.Dominio.Util;
using Catalogos.Infraestructura.Entities;
using Catalogos.Infraestructura.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogos.Dominio.Services.Queries
{
    public class CatalogosServiceQuery : ICatalogosServiceQuery
    {
        private readonly IUnitOfWork<Catalogo> _ufwCatalogos;
        private readonly IUtils _utils;

        public CatalogosServiceQuery(IUnitOfWork<Catalogo> ufwCatalogos,
                                     IUtils utils)
        {
            this._ufwCatalogos = ufwCatalogos;
            this._utils = utils;
        }

        public IEnumerable<CatalogoQuery> VerCatalogos()
        {
            IEnumerable<CatalogoQuery> catalogosQ = null;

            try
            {
                IEnumerable<Catalogo> catalogos = _ufwCatalogos.Repository<Catalogo>().Find(new CatalogoSpecification()).AsEnumerable();

                catalogosQ = this._utils.ConvertList_Catalogo_To_Query(catalogos);

            }
            catch (Exception e)
            {
                throw e;
            }

            return catalogosQ;
        }

        public CatalogoQuery verCatalogoPorCodigo(string codigo)
        {
            CatalogoQuery catalogoQ = null;

            try
            {
                Catalogo catalogo = _ufwCatalogos.Repository<Catalogo>().Find(new CatalogoSpecification(codigo, true)).FirstOrDefault();

                if(catalogo != null)
                {
                    catalogoQ = this._utils.Convert_Catalogo_To_Query(catalogo);
                }
                else
                {
                    throw new Exception("No se ha encontrado el Catálogo");
                }
                

            }
            catch (Exception e)
            {
                throw e;
            }

            return catalogoQ;
        }



        public IEnumerable<CatalogoQuery> verPaginacion(int skip, int take)
        {
            if (take <= 0) { throw new Exception("La variable take debe tener valor positivo"); }

            IEnumerable<CatalogoQuery> catalogosQ = null;

            try
            {
                IEnumerable<Catalogo> catalogos = _ufwCatalogos.Repository<Catalogo>().Find(new PagingSpecification<Catalogo>(skip, take)).AsEnumerable();

                catalogosQ = this._utils.ConvertList_Catalogo_To_Query(catalogos);

            }
            catch (Exception e)
            {
                throw e;
            }

            return catalogosQ;

        }

    }
}
