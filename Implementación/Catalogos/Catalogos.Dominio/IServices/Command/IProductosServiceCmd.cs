using Catalogos.Dominio.Modelo;
using Catalogos.Dominio.Modelo.Command;
using Catalogos.Dominio.Modelo.Queries;

namespace Catalogos.Dominio.IServices.Command
{
    public interface IProductosServiceCmd
    {
        ProductoQuery AgregarProducto(ProductoCmd productoCmd);

        void ProcesarEstadoProducto(EventBase<EstadoProductoCmd> EventoEstado);
    }
}
