
using Catalogos.Infraestructura.Entities;
using Catalogos.Infraestructura.Entities.Integration;
using System.Collections.Generic;

namespace Catalogos.Infraestructura.Util
{
    public interface IUtilsInfra
    {


        //          [Productos]     -------------------------

        #region Productos

        IEnumerable<Producto> ConvertList_ProductoInt_To_Producto(IEnumerable<ProductoInt> productos);


        Producto Convert_ProductoInt_To_Producto(ProductoInt productoInt);

        #endregion




    }
}
