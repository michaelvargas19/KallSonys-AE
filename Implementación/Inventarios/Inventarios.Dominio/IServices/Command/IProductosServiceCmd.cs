using Inventarios.Dominio.Modelo.Command;
using Inventarios.Dominio.Modelo.Queries;

namespace Inventarios.Dominio.IServices.Command
{
    public interface IProductosServiceCmd
    {
        ProductoQuery AgregarProducto(ProductoCmd productoCmd);
        
        ProductoQuery ActualizarProducto(ProductoCmd productoCmd);
    }
}
