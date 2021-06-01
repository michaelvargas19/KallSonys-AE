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
    public class ProductosServiceQuery : IProductosServiceQuery
    {

        private readonly IUnitOfWork<Producto> _ufwProductos;
        private readonly IUtils _utils;

        public ProductosServiceQuery(IUnitOfWork<Producto> ufwProductos,
                                     IUtils utils)
        {
            this._ufwProductos = ufwProductos;
            this._utils = utils;
        }

        public IEnumerable<ProductoQuery> verPaginacion(int skip, int take)
        {
            if (take <= 0) { throw new Exception("La variable take debe tener valor positivo"); }

            IEnumerable<ProductoQuery> productosQ = null;

            try
            {
                IEnumerable<Producto> productos = _ufwProductos.Repository<Producto>().Find(new PagingSpecification<Producto>(skip, take)).AsEnumerable();

                productosQ = this._utils.ConvertList_Producto_To_Query(productos);

            }
            catch (Exception e)
            {
                throw e;
            }

            return productosQ;
        }

        public ProductoQuery verProductoPorCodigo(string codigo)
        {
            ProductoQuery productoQ = null;

            try
            {
                Producto producto = _ufwProductos.Repository<Producto>().Find(new ProductoCodigoSpecification(codigo)).FirstOrDefault();

                if (producto != null)
                {
                    productoQ = this._utils.Convert_Producto_To_Query(producto);
                }
                else
                {
                    throw new Exception("No se ha encontrado el Producto");
                }

            }
            catch (Exception e)
            {
                throw e;
            }

            return productoQ;
        }

        public ProductoQuery verProductoPorSKU(string sku)
        {
            ProductoQuery productoQ = null;

            try
            {
                Producto producto = _ufwProductos.Repository<Producto>().Find(new ProductoSKUSpecification(sku)).FirstOrDefault();

                if (producto != null)
                {
                    productoQ = this._utils.Convert_Producto_To_Query(producto);
                }
                else
                {
                    throw new Exception("No se ha encontrado el Producto");
                }

            }
            catch (Exception e)
            {
                throw e;
            }

            return productoQ;
        }

        public IEnumerable<ProductoQuery> verRankingCatalogo(string codigoCatalogo, int skip, int take)
        {
            if (take <= 0) { throw new Exception("La variable take debe tener valor positivo"); }

            IEnumerable<ProductoQuery> productosQ = null;

            try
            {
                IEnumerable<Producto> productos = _ufwProductos.Repository<Producto>().Find(new ProductoSpecification(codigoCatalogo, true)).AsEnumerable();
                
                productos = productos.OrderByDescending(p => p.Calificacion).Skip(skip).Take(take);

                productosQ = this._utils.ConvertList_Producto_To_Query(productos);

            }
            catch (Exception e)
            {
                throw e;
            }

            return productosQ;

        }

        public IEnumerable<ProductoQuery> verRankingFullText(string texto, int skip, int take)
        {
            if (take <= 0) { throw new Exception("La variable take debe tener valor positivo"); }

            IEnumerable<ProductoQuery> productosQ = null;

            try
            {
                IEnumerable<Producto> productos = _ufwProductos.Repository<Producto>().Find(new ProductoFullTextSpecification(texto, true)).AsEnumerable();

                productos = productos.OrderByDescending(p => p.Prioridad).Skip(skip).Take(take);

                productosQ = this._utils.ConvertList_Producto_To_Query(productos);

            }
            catch (Exception e)
            {
                throw e;
            }

            return productosQ;
        }

        public IEnumerable<ProductoQuery> verRankingMarca(string marca, int skip, int take)
        {
            if (take <= 0) { throw new Exception("La variable take debe tener valor positivo"); }

            IEnumerable<ProductoQuery> productosQ = null;

            try
            {
                IEnumerable<Producto> productos = _ufwProductos.Repository<Producto>().Find(new ProductoMarcaSpecification(marca, true)).AsEnumerable();

                productos = productos.OrderByDescending(p => p.Calificacion).Skip(skip).Take(take);

                productosQ = this._utils.ConvertList_Producto_To_Query(productos);

            }
            catch (Exception e)
            {
                throw e;
            }

            return productosQ;
        }
    }
}
