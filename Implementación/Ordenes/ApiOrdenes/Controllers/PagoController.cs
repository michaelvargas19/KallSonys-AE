using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiOrdenes.Extensions;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using Ordenes.Dominio.IUnitOfWorks;
using Ordenes.Dominio.Models;
using Ordenes.Infraestructura.Services;

namespace ApiOrdenes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagoController : ControllerBase
    {
        private readonly IUnitOfWork<Pago> _unitOfWork;
        private readonly IUnitOfWork<Pedido> _unitOfWork_pedido;
        private readonly IUnitOfWork<Orden> _unitOfWork_orden;

        private readonly IProducer<string, string> _producer;
        EventBase<ProductoResponse> _evento = new EventBase<ProductoResponse>();
        ServicePago_command pago_cmd = new ServicePago_command();



        public PagoController(IUnitOfWork<Pago> unitOfWork, IUnitOfWork<Pedido> unitOfWork_pedido,
             ProducerConfig producerConfig, IUnitOfWork<Orden> unitOfWork_orden)
        {
            _unitOfWork = unitOfWork;
            _unitOfWork_pedido = unitOfWork_pedido;
            
            _producer = new ProducerBuilder<string, string>(producerConfig).Build();
            _unitOfWork_orden = unitOfWork_orden;
        }

        [HttpPost]
        public async Task<ActionResult<Pago>> AgregarPago(Pago pago)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var listaData = await pago_cmd.AgregarPago(pago,_unitOfWork,_unitOfWork_pedido,_unitOfWork_orden);
                    _evento.Data = listaData;
                    _evento.Evento = "VentaProductos";

                    _evento.Producir(_evento, _producer);
                }
            }
            catch(Exception error)
            {
                throw new Exception(error.InnerException + error.Message);
            }

            return pago;
        }


        [HttpGet]
        public IQueryable<Pago> ObtenerPagos()
        {
            return _unitOfWork.Service_Queries<Pago>().AsQueryable();
        }


        //Buscar pago por pedido
        [Route("[action]/{idPedido}")]
        [HttpGet]
        public Pago ObtenerPagoPorPedido(string idPedido)
        {
            return _unitOfWork.Service_Queries<Pago>().FindOne(x => x.numero_pedido == idPedido);
        }

      


    }
}
