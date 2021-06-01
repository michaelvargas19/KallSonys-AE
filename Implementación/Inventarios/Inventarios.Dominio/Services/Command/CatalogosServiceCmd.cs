using Inventarios.Dominio.IServices.Command;
using Inventarios.Dominio.IUnitOfWorks;
using Inventarios.Dominio.Modelo.Command;
using Inventarios.Dominio.Modelo.Queries;
using Inventarios.Dominio.Util;
using Inventarios.Infraestructura.Entities;
using Inventarios.Infraestructura.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inventarios.Dominio.Services.Command
{
    public class CatalogosServiceCmd : ICatalogosServiceCmd
    {
        private readonly IUnitOfWork<Catalogo> _ufwCatalogos;
        private readonly IUnitOfWork<Producto> _ufwProductos;
        private readonly IUtils _utils;

        public CatalogosServiceCmd(IUnitOfWork<Catalogo> ufwCatalogos,
                                   IUnitOfWork<Producto> ufwProductos,
                                     IUtils utils)
        {
            this._ufwCatalogos = ufwCatalogos;
            this._ufwProductos = ufwProductos;
            this._utils = utils;
        }


        public CatalogoQuery AgregarCatalogo(CatalogoCmd catalogoCmd)
        {
            CatalogoQuery catalogoQ = null;

            try
            {
                catalogoCmd.Validar();

                Catalogo catalogo = this._utils.Convert_CatalogoCmd_To_Catalogo(catalogoCmd);
                catalogo.CodigoCatalogo = this.GenerarCodigoCatalogo(catalogo.IndExterno);
                _ufwCatalogos.Repository<Catalogo>().InsertOne(catalogo);
                catalogoQ = this._utils.Convert_Catalogo_To_Query(catalogo);

                //_ufwCatalogos.Commit();

            }
            catch (Exception e)
            {
                throw e;
            }

            return catalogoQ;
        }



        public CatalogoQuery ActualizarCatalogo(CatalogoCmd catalogoCmd)
        {
            CatalogoQuery catalogoQ = null;

            try
            {
                catalogoCmd.Validar();

                Catalogo doc = _ufwCatalogos.Repository<Catalogo>().Find(new CatalogoSpecification(catalogoCmd.CodigoCatalogo)).FirstOrDefault();

                if ( doc != null )
                {
                    Catalogo catalogo = this._utils.Convert_CatalogoCmd_To_Catalogo(catalogoCmd);
                    catalogo.Id = doc.Id;
                    _ufwCatalogos.Repository<Catalogo>().ReplaceOne(catalogo);
                    

                    foreach( Producto p in _ufwProductos.Repository<Producto>().Find(new ProductoSpecification(catalogo.CodigoCatalogo) ))
                    {
                        p.Proveedor = catalogo.Proveedor;
                        _ufwProductos.Repository<Producto>().ReplaceOne(p);
                    }

                    catalogoQ = this._utils.Convert_Catalogo_To_Query(catalogo);

                    //_ufwCatalogos.Commit();

                }

                

            }
            catch (Exception e)
            {
                throw e;
            }

            return catalogoQ;
        }



        
        //Generar código para un nuevo catálogo
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
