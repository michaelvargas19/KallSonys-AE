using Catalogos.Dominio.IServices.Command;
using Catalogos.Dominio.IUnitOfWorks;
using Catalogos.Dominio.Modelo.Command;
using Catalogos.Dominio.Modelo.Queries;
using Catalogos.Dominio.Util;
using Catalogos.Infraestructura.Entities;
using Catalogos.Infraestructura.Specification;
using System;

namespace Catalogos.Dominio.Services.Command
{
    public class CatalogosServiceCmd : ICatalogosServiceCmd
    {
        private readonly IUnitOfWork<Catalogo> _ufwCatalogos;
        private readonly IUtils _utils;

        public CatalogosServiceCmd(IUnitOfWork<Catalogo> ufwCatalogos,
                                     IUtils utils)
        {
            this._ufwCatalogos = ufwCatalogos;
            this._utils = utils;
        }

        public CatalogoQuery AgregarCatalogo(CatalogoCmd catalogoCmd)
        {
            CatalogoQuery catalogoQ = null;

            try
            {
                Catalogo catalogo = this._utils.Convert_CatalogoCmd_To_Catalogo(catalogoCmd);
                catalogo.CodigoCatalogo = this.GenerarCodigoCatalogo(catalogo.IndExterno);
                _ufwCatalogos.Repository<Catalogo>().InsertOne(catalogo);
                catalogoQ = this._utils.Convert_Catalogo_To_Query(catalogo);
            }
            catch (Exception e)
            {
                throw e;
            }

            return catalogoQ;
        }





        private string GenerarCodigoCatalogo(bool EsExterno)
        {
            int numero = 0;

            string codigo = (EsExterno) ? "CATEX" : "CAT";
            numero = (_ufwCatalogos.Repository<Catalogo>().Count(new CatalogoSpecification()) + 1);

            if (_ufwCatalogos.Repository<Catalogo>().Contains(new CatalogoSpecification(codigo + numero)))
            {
                numero++;
            }
            
            codigo = codigo + numero;

            return codigo;
        }
    }
}
