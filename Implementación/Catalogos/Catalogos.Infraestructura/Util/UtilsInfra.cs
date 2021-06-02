using Catalogos.Infraestructura.Entities;
using Catalogos.Infraestructura.Entities.Integration;
using Catalogos.Infraestructura.Util;
using System.Collections.Generic;

namespace Catalogos.Dominio.Util
{
    public class UtilsInfra : IUtilsInfra
    {



        //          [Productos]     -------------------------

        #region Productos

        public IEnumerable<Producto> ConvertList_ProductoInt_To_Producto(IEnumerable<ProductoInt> productos)
        {
            List<Producto> q = new List<Producto>();

            foreach (ProductoInt p in productos)
            {
                q.Add(Convert_ProductoInt_To_Producto(p));
            }

            return q;
        }


        public Producto Convert_ProductoInt_To_Producto(ProductoInt productoInt)
        {
            Producto p = new Producto();

            //availability
            //brand_id

            p.Marca = productoInt.brand_name;
            p.CodigoProducto = productoInt.code;
            p.Descripcion = productoInt.description + ". Condición: " + productoInt.condition;
            p.EnAlmacen = (productoInt.availability.CompareTo("available") == 0) ? true : false;
            p.Estado = (productoInt.availability.CompareTo("available")==0)? true : false;
            p.Nombre = productoInt.name;
            p.NivelInventario = productoInt.order_quantity_maximum;
            p.NivelAdvertencia = productoInt.order_quantity_minimum;
            p.ValorUnitario = productoInt.price;
            p.SKU = productoInt.SKU;
            p.TipoProducto = productoInt.type;

            if (productoInt.is_free_shipping)
            {
                p.Descripcion = p.Descripcion + ". El envío es gratis.";
            }

            if ( ((productoInt.price - productoInt.sale_price) > 0 ) )
            {
                p.Descuento = new Descuento {
                    Nombre = "Proveedor",
                    Descripcion = "Dada por el Proveedor",
                    Porcentaje = 100.0 - ((productoInt.sale_price * 100.0)/(productoInt.price))
                };
            }
            
            return p;
        }

        #endregion



    }
}
