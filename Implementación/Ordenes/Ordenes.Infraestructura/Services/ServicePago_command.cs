using Ordenes.Dominio.IServices;
using Ordenes.Dominio.IUnitOfWorks;
using Ordenes.Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ordenes.Infraestructura.Services
{
    public class ServicePago_command : IServicePago_command
    {
        public async Task<List<ProductoResponse>> AgregarPago(Pago pago, IUnitOfWork<Pago> _unitOfWork,
            IUnitOfWork<Pedido> _unitOfWork_pedido,
            IUnitOfWork<Orden> _unitOfWork_orden)
        {

            List<ProductoResponse> respuesta = new List<ProductoResponse>();

            pago.fecha_pago = System.DateTime.Now;
            var existePedido = _unitOfWork_pedido.Service_Queries<Pedido>().FindOne(x => x.Id == pago.numero_pedido);
            if (existePedido != null)
            {
                pago.Id = pago.GetInternalId("PAG");
                await _unitOfWork.Service_Commands<Pago>().InsertOneAsync(pago);
                respuesta = BuscarProductoPorOrden(pago, _unitOfWork_pedido, _unitOfWork_orden);


            }
            else
            {
                throw new Exception("Pedido no encontrado");
            }
            return respuesta;
        }

     
        public List<ProductoResponse> BuscarProductoPorOrden(Pago pago, IUnitOfWork<Pedido> _unitOfWork_pedido,
            IUnitOfWork<Orden> _unitOfWork_orden)
        {
            List<ProductoResponse> respuesta = new List<ProductoResponse>();
            var existePedido = _unitOfWork_pedido.Service_Queries<Pedido>().FindOne(x => x.Id == pago.numero_pedido);
            if (existePedido != null)
            {
                
                foreach(var item in existePedido.id_orden)
                {
                    var ordenes = _unitOfWork_orden.Service_Queries<Orden>().FindOne(x => x.Id == item);
                    foreach(var producto in ordenes.items)
                    {
                        ProductoResponse objResponse = new ProductoResponse();
                        objResponse.Referencia = pago.numero_pedido;
                        objResponse.SKU = producto.id_producto;
                        objResponse.Unidades = producto.cantidad;
                        respuesta.Add(objResponse);
                    }
                }
            }
            return respuesta;
        }
    }
}
