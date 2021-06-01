using Catalogos.Dominio.Modelo.Command;
using Catalogos.Dominio.Modelo.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalogos.Dominio.IServices.Command
{
    public interface IProductosServiceCmd
    {
        ProductoQuery AgregarProducto(ProductoCmd productoCmd);
    }
}
