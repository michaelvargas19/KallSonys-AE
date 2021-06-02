using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiOrdenes.Extensions;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using Ordenes.Dominio.IUnitOfWorks;
using Ordenes.Dominio.Models;

namespace ApiOrdenes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnvioController : ControllerBase
    {
        private readonly IUnitOfWork<Envio> _unitOfWork;
        private readonly IUnitOfWork<Pedido> _unitOfWork_pedidos;
        private readonly IUnitOfWork<Orden> _unitOfWork_orden;

        private readonly IProducer<Null, string> _producer;
        EventBase<Envio> _evento = new EventBase<Envio>();



        public EnvioController(IUnitOfWork<Envio> unitOfWork, IUnitOfWork<Pedido> unitOfWork_pedidos, IUnitOfWork<Orden> unitOfWork_orden
             , ProducerConfig producerConfig)
        {
            _unitOfWork = unitOfWork;
            _unitOfWork_pedidos = unitOfWork_pedidos;
            _unitOfWork_orden = unitOfWork_orden;
            _producer = new ProducerBuilder<Null, string>(producerConfig).Build();
        }

        //Crear Envio
        [HttpPost]
        public async Task<ActionResult<Envio>> NuevoEnvio(Envio nvoEnvio)
        {
            bool ordenExiste = false;
            var pedidoExiste = _unitOfWork_pedidos.Service_Queries<Pedido>().FindById(nvoEnvio.id_pedido);
            if(pedidoExiste == null)
            {
                return NotFound("Pedido no existe");
            }
            foreach(var item in pedidoExiste.id_orden)
            {
                if(nvoEnvio.id_orden == item)
                {
                    ordenExiste = true;
                }
            }

            if (!ordenExiste)
            {
                return NotFound("Orden no existe");
            }

            if (ModelState.IsValid && ordenExiste)
            {
                nvoEnvio.Id = nvoEnvio.GetInternalId("ENV");
                nvoEnvio.fecha_envio = System.DateTime.Now;
                await _unitOfWork.Service_Commands<Envio>().InsertOneAsync(nvoEnvio);

              
            }
            return nvoEnvio;

        }


        //Mostrar envios
        [HttpGet]
        public IQueryable<Envio> ObtenerEnvios()
        {
            return _unitOfWork.Service_Queries<Envio>().AsQueryable();
        }


        //Buscar Envio por pedido
        [Route("[action]/{numPedido}")]
        [HttpGet]
        public Envio ObtenerEnvioPorPedido(string numPedido)
        {
            var envio = _unitOfWork.Service_Queries<Envio>().FindOne(x=> x.id_pedido ==  numPedido);
            return envio;
        }

        //Buscar Orden por proveedor
        [Route("[action]/{idProveedor}")]
        [HttpGet]
        public ActionResult<List<Envio>> ObtenerEnvioPorProveedor(string idProveedor)
        {
            List<Envio> resultado = new List<Envio>();
            List<string> ordenes = new List<string>();
            var orden = _unitOfWork_orden.Service_Queries<Orden>().AsQueryable().Where(x => x.id_proveedor == idProveedor).ToList();
            if (orden != null)
            {
                foreach (var itemOrden in orden)
                {
                    ordenes.Add(itemOrden.Id);
                }
                foreach (var item in ordenes)
                {
                    var env = _unitOfWork.Service_Queries<Envio>().FindOne(x => x.id_orden == item);
                    if (env != null)
                    {
                        resultado.Add(env);
                    }
                }
                if (resultado.Count() == 0)
                {
                    return NotFound("Envio no encontrado");
                }

            }
            else
            {
                return NotFound("Orden no encontrada");
            }
            return resultado;
        }

        //Buscar envio por cliente
        [Route("[action]/{idCliente}")]
        [HttpGet]
        public ActionResult<List<Envio>> ObtenerEnvioPorCliente(string idCliente)
        {
            List<Envio> resultado = new List<Envio>();
            List<string> ordenes = new List<string>();
            var orden = _unitOfWork_orden.Service_Queries<Orden>().AsQueryable().Where(x => x.id_cliente == idCliente).ToList();
            if(orden != null)
            {
                foreach (var itemOrden in orden)
                {
                    ordenes.Add(itemOrden.Id);
                }
                foreach (var item in ordenes)
                {
                    var env = _unitOfWork.Service_Queries<Envio>().FindOne(x => x.id_orden == item);
                    if(env != null)
                    {
                        resultado.Add(env);
                    }
                }
                if (resultado.Count() == 0)
                {
                    return NotFound("Envio no encontrado");
                }
            }
            else
            {
                return NotFound("Orden no encontrada");
            }
            return resultado;
        }

      


        //Buscar envio por id
        [Route("[action]/{idEnvio}")]
        [HttpGet]
        public Envio ObtenerEnvioPorId(string idEnvio)
        {
            var envio = _unitOfWork.Service_Queries<Envio>().FindById(idEnvio);
            return envio;
        }

        //*Buscar envio por orden
        [Route("[action]/{idOrden}")]
        [HttpGet]
        public List<Envio> ObtenerEnvioPorOrden(string idOrden)
        {
            var pedido = _unitOfWork.Service_Queries<Envio>().AsQueryable().Where(x => x.id_orden == idOrden).ToList();
            return pedido;
        }

    }
}
