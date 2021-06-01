using Catalogos.Dominio.Modelo.Command;
using Catalogos.Dominio.Modelo.Queries;
using Catalogos.Infraestructura.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalogos.Dominio.Util
{
    public interface IUtils
    {

        //          [Catálogos]     -------------------------
        #region Catálogos

        Catalogo Convert_CatalogoCmd_To_Catalogo(CatalogoCmd catalogoCmd);


        IEnumerable<CatalogoQuery> ConvertList_Catalogo_To_Query(IEnumerable<Catalogo> catalogos);


        CatalogoQuery Convert_Catalogo_To_Query(Catalogo catalogo);

        #endregion



        //          [Productos]     -------------------------

        #region Productos

        Producto Convert_ProductoCmd_To_Producto(ProductoCmd productoCmd);

        IEnumerable<ProductoQuery> ConvertList_Producto_To_Query(IEnumerable<Producto> productos);


        ProductoQuery Convert_Producto_To_Query(Producto producto);

        #endregion


        //          [Multimedia]     -------------------------

        #region Multimedia

        MultimediaQuery Convert_Multimedia_To_MultimediaQuery(Multimedia multimedia);


        Multimedia Convert_MultimediaCmd_To_Multimedia(MultimediaCmd multimediaCmd);

        #endregion


        //          [Proveedor]     -------------------------

        public ProveedorQuery Convert_Proveedor_To_ProveedorQuery(Proveedor proveedor);


        public Proveedor Convert_ProveedorCmd_To_Proveedor(ProveedorCmd proveedorCmd);


        //          [Descuento]     -------------------------

        #region Descuento

        DescuentoQuery Convert_Descuento_To_DescuentoQuery(Descuento descuento);

        Descuento Convert_DescuentoCmd_To_Descuento(DescuentoCmd descuentoCmd);

        #endregion


    }
}
