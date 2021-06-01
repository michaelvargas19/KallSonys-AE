using Catalogos.Dominio.IServices.Command;
using Catalogos.Dominio.IUnitOfWorks;
using Catalogos.Dominio.Modelo.Command;
using Catalogos.Dominio.Modelo.Queries;
using Catalogos.Dominio.Util;
using Catalogos.Infraestructura.Entities;
using Catalogos.Infraestructura.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalogos.Dominio.Services.Command
{
    public class ProductosServiceCmd : IProductosServiceCmd
    {
        private readonly IUnitOfWork<Producto> _ufwProductos;
        private readonly IUtils _utils;

        public ProductosServiceCmd(IUnitOfWork<Producto> ufwProductos,
                                     IUtils utils)
        {
            this._ufwProductos = ufwProductos;
            this._utils = utils;
        }


        public ProductoQuery AgregarProducto(ProductoCmd productoCmd)
        {
            ProductoQuery productoQ = null;

            try
            {

                Producto producto = this._utils.Convert_ProductoCmd_To_Producto(productoCmd);
                producto.CodigoProducto = this.GenerarCodigoProducto(producto.IndExterno);
                _ufwProductos.Repository<Producto>().InsertOne(producto);
                productoQ = this._utils.Convert_Producto_To_Query(producto);

            }
            catch (Exception e)
            {
                throw e;
            }

            return productoQ;
        }




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
