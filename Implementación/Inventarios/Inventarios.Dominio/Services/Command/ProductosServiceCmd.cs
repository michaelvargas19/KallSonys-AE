using Inventarios.Dominio.IServices.Command;
using Inventarios.Dominio.IUnitOfWorks;
using Inventarios.Dominio.Modelo.Command;
using Inventarios.Dominio.Modelo.Queries;
using Inventarios.Dominio.Util;
using Inventarios.Infraestructura.Entities;
using Inventarios.Infraestructura.Specification;
using System;
using System.Linq;

namespace Inventarios.Dominio.Services.Command
{
    public class ProductosServiceCmd : IProductosServiceCmd
    {
        private readonly IUnitOfWork<Producto> _ufwProductos;
        private readonly IUnitOfWork<Catalogo> _ufwCatalogos;
        private readonly IUtils _utils;

        public ProductosServiceCmd(IUnitOfWork<Producto> ufwProductos,
                                   IUnitOfWork<Catalogo> ufwCatalogos,
                                     IUtils utils)
        {
            this._ufwProductos = ufwProductos;
            this._ufwCatalogos = ufwCatalogos;
            this._utils = utils;
        }


        public ProductoQuery AgregarProducto(ProductoCmd productoCmd)
        {
            ProductoQuery productoQ = null;

            try
            {
                Catalogo catalogo = _ufwCatalogos.Repository<Catalogo>().Find(new CatalogoSpecification(productoCmd.CodigoCatalogo)).FirstOrDefault();

                if (catalogo != null)
                {

                    if(_ufwProductos.Repository<Producto>().Contains(new ProductoSKUSpecification(productoCmd.SKU)) )
                    {
                        throw new Exception("Ya existe un producto con el SKU");
                    }

                    Producto producto = this._utils.Convert_ProductoCmd_To_Producto(productoCmd);
                    producto.IndExterno = catalogo.IndExterno;
                    producto.Proveedor = catalogo.Proveedor;
                    producto.CodigoProducto = this.GenerarCodigoProducto(producto.IndExterno);
                    _ufwProductos.Repository<Producto>().InsertOne(producto);
                    productoQ = this._utils.Convert_Producto_To_Query(producto);
                    
                    //_ufwCatalogos.Commit();

                }
                else
                {
                    throw new Exception("El catálogo es inváldo");
                }

            }
            catch (Exception e)
            {
                throw e;
            }

            return productoQ;
        }


        public ProductoQuery ActualizarProducto(ProductoCmd productoCmd)
        {
            ProductoQuery productoQ = null;

            try
            {
                Catalogo catalogo = _ufwCatalogos.Repository<Catalogo>().Find(new CatalogoSpecification(productoCmd.CodigoCatalogo)).FirstOrDefault();

                if (catalogo != null)
                {
                    Producto doc = _ufwProductos.Repository<Producto>().Find(new ProductoCodigoSpecification(productoCmd.CodigoProducto)).FirstOrDefault();

                    if (doc != null)
                    {
                        Producto producto = this._utils.Convert_ProductoCmd_To_Producto(productoCmd);
                        producto.Id = doc.Id;
                        producto.IndExterno = catalogo.IndExterno;
                        producto.Proveedor = catalogo.Proveedor;
                        _ufwProductos.Repository<Producto>().ReplaceOne(producto);
                        productoQ = this._utils.Convert_Producto_To_Query(producto);


                    }
                    else
                    {
                        throw new Exception("El producto es inváldo");
                    }

                }
                else
                {
                    throw new Exception("El catálogo es inváldo");
                }

            }
            catch (Exception e)
            {
                throw e;
            }

            return productoQ;
        }



        //Generar código para un nuevo producto
        private string GenerarCodigoProducto(bool EsExterno)
        {
            int numero = 0;

            string codigo = (EsExterno) ? "PROEX" : "PRO";
            numero = (_ufwProductos.Repository<Producto>().Count(new ProductoSpecification()) + 1);

            if (_ufwProductos.Repository<Producto>().Contains(new ProductoCodigoSpecification(codigo + numero)))
            {
                numero++;
            }

            codigo = codigo + numero;

            return codigo;

        }
    }
}
