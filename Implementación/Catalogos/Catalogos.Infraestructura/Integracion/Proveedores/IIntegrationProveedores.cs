using Catalogos.Infraestructura.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalogos.Infraestructura.Integracion.Proveedores
{
    public interface IIntegrationProveedores
    {
        Producto consultarProducto(string sku);

        IEnumerable<Producto> consultarProductosProveedor(string idProveedor);
    }
}
