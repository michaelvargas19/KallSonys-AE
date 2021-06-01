using Inventarios.Dominio.IServices.Queries;
using Inventarios.Dominio.IUnitOfWorks;
using Inventarios.Dominio.Modelo.Queries;
using Inventarios.Dominio.Util;
using Inventarios.Infraestructura.Entities;
using Inventarios.Infraestructura.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inventarios.Dominio.Services.Queries
{
    public class InventariosServiceQuery : IInventariosServiceQuery
    {
        private readonly IUnitOfWork<Producto> _ufwProductos;
        private readonly IUtils _utils;

        public InventariosServiceQuery(IUnitOfWork<Producto> ufwProductos,
                                     IUtils utils)
        {
            this._ufwProductos = ufwProductos;
            this._utils = utils;
        }
            

        public IEnumerable<ExistenciaProducto> ConsultarDisponibilidad(string[] sku)
        {
            List<ExistenciaProducto> existencias = null;

            try
            {
                existencias = _ufwProductos.Repository<Producto>().Find(new ProductoSKUSpecification(sku))
                                                        .Select(p=> new ExistenciaProducto() {
                                                                    Disponible = p.EstaEnAlmacen(),
                                                                    CantidadDisponibles = p.NivelInventario,
                                                                    CodigoProducto = p.CodigoProducto,
                                                                    sku = p.SKU
                                                                } ).ToList();

                if (existencias == null)
                {
                    throw new Exception("No se han encontrado los Productos");
                }


            }
            catch (Exception e)
            {
                throw e;
            }

            return existencias;
        }


        public ExistenciaProducto ConsultarDisponibilidadSKU(string sku)
        {
            ExistenciaProducto existencia = null;

            try
            {
                Producto producto = _ufwProductos.Repository<Producto>().Find(new ProductoSKUSpecification(sku)).FirstOrDefault();

                if (producto != null)
                {
                    existencia = new ExistenciaProducto();
                    existencia.Disponible = producto.EstaEnAlmacen();
                    existencia.CantidadDisponibles = producto.NivelInventario;
                    existencia.CodigoProducto = producto.CodigoProducto;
                    existencia.sku = producto.SKU;
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

            return existencia;
        }
    }
}
