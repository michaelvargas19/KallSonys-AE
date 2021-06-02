using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiClientes.Extensions;
using Clientes.Dominio.IUnitOfWorks;
using Clientes.Dominio.Models;
using Confluent.Kafka;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiClientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetodosPagoController : ControllerBase
    {
        private readonly IUnitOfWork<Ref_Metodos_Pago> _unitOfWork;
        private readonly IProducer<Null, string> _producer;
        EventBase<Ref_Metodos_Pago> _evento = new EventBase<Ref_Metodos_Pago>();



        public MetodosPagoController(IUnitOfWork<Ref_Metodos_Pago> unitOfWork, ProducerConfig producerConfig)
        {
            _unitOfWork = unitOfWork;
            _producer = new ProducerBuilder<Null, string>(producerConfig).Build();
        }

        [HttpPost]
        public async Task<ActionResult<Ref_Metodos_Pago>> AgregarMetodoPago(Ref_Metodos_Pago estado)
        {


            if (ModelState.IsValid)
            {
                var existeEstado = _unitOfWork.Service_Queries<Ref_Metodos_Pago>().FindOne(x => x.descripcion_metodo_pago == estado.descripcion_metodo_pago);
                if (existeEstado == null)
                {
                    await _unitOfWork.Service_Commands<Ref_Metodos_Pago>().InsertOneAsync(estado);
                    _evento.Data = estado;
                    _evento.Evento = "MetodoPagoAgregado";
                    _evento.Producir(_evento, _producer);
                }
                else
                {
                    return Ok("Codigo de Estado ya existe");
                }
            }

            return estado;
        }

        [HttpGet]
        public IQueryable<Ref_Metodos_Pago> ObtenerMetodosPago()
        {
            return _unitOfWork.Service_Queries<Ref_Metodos_Pago>().AsQueryable();
        }

        [Route("[action]/{codigo}")]

        [HttpGet]
        public ActionResult<Ref_Metodos_Pago> ObtenerMetodoPagoPorCodigo(string codigo)
        {
            return _unitOfWork.Service_Queries<Ref_Metodos_Pago>().FindOne(x => x.codigo_metodo_pago == codigo);
        }
    }
}
