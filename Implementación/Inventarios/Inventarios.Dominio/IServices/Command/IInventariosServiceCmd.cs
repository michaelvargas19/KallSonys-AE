using Inventarios.Dominio.Modelo;
using Inventarios.Dominio.Modelo.Command;
using System.Collections.Generic;

namespace Inventarios.Dominio.IServices.Command
{
    public interface IInventariosServiceCmd
    {
        void ProcesarVenta(EventBase<List<VentaCmd>> EventoVenta);
    }
}
