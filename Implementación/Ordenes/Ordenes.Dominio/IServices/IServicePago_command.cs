using Ordenes.Dominio.IUnitOfWorks;
using Ordenes.Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ordenes.Dominio.IServices
{
    public interface IServicePago_command
    {
        Task<List<ProductoResponse>> AgregarPago(Pago pago, IUnitOfWork<Pago> _unitOfWork,
            IUnitOfWork<Pedido> _unitOfWork_pedido,
            IUnitOfWork<Orden> _unitOfWork_orden);
        List<ProductoResponse> BuscarProductoPorOrden(Pago pago, IUnitOfWork<Pedido> _unitOfWork_pedido,
            IUnitOfWork<Orden> _unitOfWork_orden);
    }
}
