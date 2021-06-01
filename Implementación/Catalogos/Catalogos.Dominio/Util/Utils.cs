using Catalogos.Dominio.Modelo.Command;
using Catalogos.Dominio.Modelo.Queries;
using Catalogos.Infraestructura.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalogos.Dominio.Util
{
    public class Utils : IUtils
    {

        //          [Catálogos]     -------------------------

        #region Catálogos

        public Catalogo Convert_CatalogoCmd_To_Catalogo(CatalogoCmd catalogoCmd)
        {
            Catalogo q = new Catalogo();
            q.Nombre = catalogoCmd.Nombre;
            q.CodigoCatalogo = catalogoCmd.CodigoCatalogo;
            q.Proveedor = this.Convert_ProveedorCmd_To_Proveedor(catalogoCmd.Proveedor);
            q.Multimedia = this.Convert_MultimediaCmd_To_Multimedia(catalogoCmd.Multimedia);
            q.Descripcion = catalogoCmd.Descripcion;
            q.Categorias = catalogoCmd.Categorias;
            q.Estado = catalogoCmd.Estado;
            q.IndExterno = catalogoCmd.IndExterno;
            q.FechaInicio = catalogoCmd.FechaInicio;
            q.FechaFin = catalogoCmd.FechaFin;
            q.Sinonimos = catalogoCmd.Sinonimos;

            return q;
        }

        public IEnumerable<CatalogoQuery> ConvertList_Catalogo_To_Query(IEnumerable<Catalogo> catalogos)
        {
            List<CatalogoQuery> q = new List<CatalogoQuery>();

            foreach (Catalogo c in catalogos)
            {
                q.Add(this.Convert_Catalogo_To_Query(c));
            }

            return q;
        }


        public CatalogoQuery Convert_Catalogo_To_Query(Catalogo catalogo)
        {
            CatalogoQuery q = new CatalogoQuery();

            q.Id = catalogo.Id;
            q.CodigoCatalogo = catalogo.CodigoCatalogo;
            q.Nombre = catalogo.Nombre;
            q.Multimedia = this.Convert_Multimedia_To_MultimediaQuery(catalogo.Multimedia);
            q.Proveedor = this.Convert_Proveedor_To_ProveedorQuery(catalogo.Proveedor);
            q.Descripcion = catalogo.Descripcion;
            q.FechaFin = catalogo.FechaFin;
            q.Calificacion = catalogo.Calificacion;
            q.IndExterno = catalogo.IndExterno;

            return q;
        }

        #endregion



        //          [Productos]     -------------------------

        #region Productos

        public Producto Convert_ProductoCmd_To_Producto(ProductoCmd productoCmd)
        {
            Producto p = new Producto();
            p.CodigoCatalogo = productoCmd.CodigoCatalogo;
            p.Nombre = productoCmd.Nombre;
            p.Descripcion = productoCmd.Descripcion;
            p.TipoProducto = productoCmd.TipoProducto;
            p.Proveedor = this.Convert_ProveedorCmd_To_Proveedor(null);
            p.Prioridad = productoCmd.Prioridad;
            p.SKU = productoCmd.SKU;
            p.CodigoProducto = productoCmd.CodigoProducto;
            p.iva = productoCmd.iva;
            p.PesoKg = productoCmd.PesoKg;
            p.ValorUnitario = productoCmd.ValorUnitario;
            p.TipoSeguimiento = productoCmd.TipoSeguimiento;
            p.NivelInventario = productoCmd.NivelInventario;
            p.NivelAdvertencia = productoCmd.NivelAdvertencia;
            p.Descuento = this.Convert_DescuentoCmd_To_Descuento(productoCmd.Descuentos);
            p.Multimedia = this.Convert_MultimediaCmd_To_Multimedia(productoCmd.Multimedia);
            p.Marca = productoCmd.Marca;
            p.Estado = productoCmd.Estado;
            p.EnAlmacen = productoCmd.EnAlmacen();
            p.Sinonimos = productoCmd.Sinonimos;
            p.Calificacion = 0;
            p.Sinonimos = p.Sinonimos.ConvertAll(d => d.ToUpper());


            return p;
        }

        public IEnumerable<ProductoQuery> ConvertList_Producto_To_Query(IEnumerable<Producto> productos)
        {
            List<ProductoQuery> q = new List<ProductoQuery>();

            foreach (Producto p in productos)
            {
                q.Add(this.Convert_Producto_To_Query(p));
            }

            return q;
        }


        public ProductoQuery Convert_Producto_To_Query(Producto producto)
        {
            ProductoQuery p = new ProductoQuery();

            p.Id = producto.Id;
            p.CodigoCatalogo = producto.CodigoCatalogo;
            p.Nombre = producto.Nombre;
            p.Descripcion = producto.Descripcion;
            p.TipoProducto = producto.TipoProducto;
            p.Proveedor = this.Convert_Proveedor_To_ProveedorQuery(producto.Proveedor);
            p.sku = producto.SKU;
            p.CodigoProducto = producto.CodigoProducto;
            p.iva = producto.iva;
            p.PesoKg = producto.PesoKg;
            p.ValorUnitario = producto.ValorUnitario;
            p.NivelInventario = producto.NivelInventario;
            p.Descuentos = this.Convert_Descuento_To_DescuentoQuery(producto.Descuento);
            p.Multimedia = this.Convert_Multimedia_To_MultimediaQuery(producto.Multimedia);
            p.Marca = producto.Marca;
            p.EnAlmacen = producto.EnAlmacen;
            p.IndExterno = producto.IndExterno;
            p.Calificacion = producto.Calificacion;

            if (producto.Descuento != null)
            {
                p.Descuentos = this.Convert_Descuento_To_DescuentoQuery(producto.Descuento);
            }

            if (producto.Multimedia != null)
            {
                p.Multimedia = this.Convert_Multimedia_To_MultimediaQuery(producto.Multimedia);
            }

            
            return p;
        }

        #endregion



        //          [Multimedia]     -------------------------

        #region Multimedia

        public MultimediaQuery Convert_Multimedia_To_MultimediaQuery(Multimedia multimedia)
        {
            MultimediaQuery m = null;
            
            if (multimedia != null) { 
                m = new MultimediaQuery();
                m.Nombre = multimedia.Nombre;
                m.Descripcion = multimedia.Descripcion;
                m.Tipo = multimedia.Tipo;
                m.NombreTipo = multimedia.Tipo.ToString();
                m.url = multimedia.url;
            }

            return m;
        }


        public Multimedia Convert_MultimediaCmd_To_Multimedia(MultimediaCmd multimediaCmd)
        {
            Multimedia m = null;

            if (multimediaCmd != null)
            {

                if ( !(Enum.IsDefined(typeof(TIPO_MULTIMEDIA), multimediaCmd.Tipo)) )
                {
                    throw new Exception("El tipo de multimedia es inválido");
                }

                m = new Multimedia();
                m.Nombre = multimediaCmd.Nombre;
                m.Descripcion = multimediaCmd.Descripcion;
                m.Tipo = multimediaCmd.Tipo;
                m.url = multimediaCmd.url;
            }

            return m;
        }

        #endregion



        //          [Proveedor]     -------------------------

        #region Proveedor

        public ProveedorQuery Convert_Proveedor_To_ProveedorQuery(Proveedor proveedor)
        {
            ProveedorQuery p = null;

            if (proveedor != null)
            {
                p = new ProveedorQuery();
                p.IdProveedor = proveedor.IdProveedor;
                p.Nombre = proveedor.Nombre;
            }

            return p;
        }


        public Proveedor Convert_ProveedorCmd_To_Proveedor(ProveedorCmd proveedorCmd)
        {
            Proveedor p = null;

            if (proveedorCmd != null)
            {
                p = new Proveedor();
                p.IdProveedor = proveedorCmd.IdProveedor;
                p.Nombre = proveedorCmd.Nombre;
            }

            return p;
        }

        #endregion






        //          [Descuento]     -------------------------

        #region Descuento

        public DescuentoQuery Convert_Descuento_To_DescuentoQuery(Descuento descuento)
        {
            DescuentoQuery d = null;

            if (descuento != null)
            {
                d = new DescuentoQuery();
                d.Nombre = descuento.Nombre;
                d.Descripcion = descuento.Descripcion;
                d.Porcentaje = descuento.Porcentaje;
                d.MediosDePago = descuento.MediosDePago;
            }

            return d;
        }


        public Descuento Convert_DescuentoCmd_To_Descuento(DescuentoCmd descuentoCmd)
        {
            Descuento d = null;

            if (descuentoCmd != null)
            {
                d = new Descuento();
                d.Nombre = descuentoCmd.Nombre;
                d.Descripcion = descuentoCmd.Descripcion;
                d.Porcentaje = descuentoCmd.Porcentaje;
                d.MediosDePago = descuentoCmd.MediosDePago;
            }

            return d;
        }

        #endregion

    }
}
