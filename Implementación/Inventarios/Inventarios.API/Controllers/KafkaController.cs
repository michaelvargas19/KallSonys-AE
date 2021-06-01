using Confluent.Kafka;
using Inventarios.Dominio.Modelo;
using Inventarios.Dominio.Modelo.Command;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Inventarios.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KafkaController : ControllerBase
    {
        private readonly IProducer<Null,string> _producer;

        public KafkaController(ProducerConfig producerConfig)
        {
            this._producer = new ProducerBuilder<Null,string>(producerConfig).Build();
        }

        // POST: Catalogos
        /// <summary>Crear un catálogo</summary>
        /// <param name="sku">SKU del producto</param>
        /// <param name="cantidad">Datos de la solicitud</param>
        /// <returns>Catalogo Creado</returns>
        /// <response code="200">Solicitud procesada</response>
        /// <response code="202">Solicitud no procesada</response>
        /// <response code="400">Problemas con la solicitud</response>
        /// <response code="401">Falta de permisos</response>
        /// <response code="500">Error Interno</response>

        [HttpPost("/test/venta")]
        public ActionResult<bool> ProbarKafka(EventBase<List<VentaCmd>> evento)
        {

            try
            {
                evento.Evento = "VentaProductos";
                evento.Fecha = DateTime.Now;
                evento.Origen = "api-inventarios";
                evento.Topico = "T-Venta";
                
                var json = JsonConvert.SerializeObject(evento);

                this._producer.Produce("TP_Venta", new Message<Null, string> { Value = json });


            }
            catch (Exception e)
            {
                //response.message = e.Message;
            }

            //response.date = DateTime.Now;


            //return StatusCode(response.code, response);
            return StatusCode(200, true);
        }

    }
}
