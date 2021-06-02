using Catalogos.Dominio.IServices.Command;
using Catalogos.Dominio.IUnitOfWorks;
using Catalogos.Dominio.Modelo;
using Catalogos.Dominio.Modelo.Command;
using Catalogos.Dominio.Modelo.Queries;
using Catalogos.Dominio.Util;
using Catalogos.Infraestructura.Entities;
using Catalogos.Infraestructura.Specification;
using Inventarios.Infraestructura.Specification;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Reflection;

namespace Catalogos.Dominio.Services.Command
{
    public class ProductosServiceCmd : IProductosServiceCmd
    {
        private readonly IUnitOfWork<Producto> _ufwProductos;
        private readonly IUnitOfWork<_AuditoriaCatalogos> _ufwLog;
        private readonly IUtils _utils;

        public ProductosServiceCmd(IUnitOfWork<Producto> ufwProductos,
                                     IUnitOfWork<_AuditoriaCatalogos> ufwLog,
                                     IUtils utils)
        {
            this._ufwProductos = ufwProductos;
            this._ufwLog = ufwLog;
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



        public void ProcesarEstadoProducto(EventBase<EstadoProductoCmd> EventoEstado)
        {
            bool EsError = true;
            var jrq = JsonConvert.SerializeObject(EventoEstado);
            var jrp = "";

            if (!(_ufwLog.Repository<_AuditoriaCatalogos>().Contains(new LogCatalogosSpecification(EventoEstado.Data.SKU, jrq))))
            {
                try
                {

                    Producto producto = _ufwProductos.Repository<Producto>().Find(new ProductoSKUSpecification(EventoEstado.Data.SKU)).FirstOrDefault();

                    if ((producto != null))
                    {
                        producto.Estado = EventoEstado.Data.Estado;
                        producto.EnAlmacen = EventoEstado.Data.EnAlmacen;
                        producto.NivelInventario = EventoEstado.Data.NivelInventario;

                        //Persistencia
                        _ufwProductos.Repository<Producto>().ReplaceOne(producto);
                        EsError = false;

                        jrp = JsonConvert.SerializeObject(producto);
                    }
                    else
                    {
                        throw new Exception("No se ha encontrado el producto");
                    }

                }
                catch (Exception e)
                {
                    jrp = JsonConvert.SerializeObject(e);
                }


                //Auditoría


                _AuditoriaCatalogos log = new _AuditoriaCatalogos("CambioEstadoProducto", EventoEstado.Data.SKU, EsError, EventoEstado.Usuario, MethodInfo.GetCurrentMethod().Name, this.ToString(), jrq, jrp, "", "SKU: " + EventoEstado.Data.SKU + "   NivelInventario: " + EventoEstado.Data.NivelInventario.ToString() + "");

                _ufwLog.Repository<_AuditoriaCatalogos>().InsertOne(log);

            }
            else
            {
                //Ya fue ejecutada
            }

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
