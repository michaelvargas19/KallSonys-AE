using Confluent.Kafka;
using Inventarios.Dominio.IUnitOfWorks;
using Inventarios.Dominio.Modelo;
using Inventarios.Dominio.Modelo.Command;
using Inventarios.Infraestructura.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;

namespace Inventarios.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KafkaController : ControllerBase
    {
        private readonly IProducer<string, string> _producer;
        private readonly ILogger _logger;
        private readonly IUnitOfWork<_AuditoriaInventarios> _ufwLog;

        public KafkaController(ILogger<KafkaController> logger,
                               ProducerConfig producerConfig,
                               IUnitOfWork<_AuditoriaInventarios> ufwLog)
        {
            this._producer = new ProducerBuilder<string,string>(producerConfig).Build();
            this._logger = logger;
            this._ufwLog = ufwLog;
        }

        // POST: Kafka
        /// <summary>Probar evento de Venta</summary>
        /// <param name="evento">Datos de la solicitud</param>
        /// <returns>---</returns>
        /// <response code="200">Solicitud procesada</response>
        /// <response code="202">Solicitud no procesada</response>
        /// <response code="400">Problemas con la solicitud</response>
        /// <response code="401">Falta de permisos</response>
        /// <response code="500">Error Interno</response>

        [HttpPost("/test/venta")]
        public ActionResult<string> ProbarKafka(EventBase<List<VentaCmd>> evento)
        {
            bool esError = true;
            string response = "Descripción del evento enviado por el servicio Kafka/test/venta";
            try
            {
                evento.Evento = "VentaProductos";
                evento.Fecha = DateTime.Now;
                evento.Origen = "api-inventarios";
                evento.Topico = "TP_Venta";
                
                var json = JsonConvert.SerializeObject(evento);

                this._producer.Produce("TP_Venta", new Message<string, string> { Key = "VentaProductos", Value = json });
                esError = false;

            }
            catch (Exception e)
            {
                Console.WriteLine($"MENSAJE:  '{e.Message}'  STACKTRACE:  '{e.StackTrace}' \n\n");
                _logger.LogError($"MENSAJE:  '{e.Message}'  STACKTRACE:  '{e.StackTrace}' \n\n\n");
                response = e.Message;
            }

            //response.date = DateTime.Now;

            //Auditoría
            var jrq = JsonConvert.SerializeObject(evento);
            var jrp = JsonConvert.SerializeObject(response);
            var host = "";
            try
            {
                var remoteIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                host = Dns.GetHostEntry(remoteIpAddress).HostName;
            }
            catch (Exception e) { }

            _AuditoriaInventarios log = new _AuditoriaInventarios("ProbarVenta", "", esError, "", MethodInfo.GetCurrentMethod().Name, this.ToString(), jrq, jrp, "Host: " + host, "");
            _ufwLog.Repository<_AuditoriaInventarios>().InsertOne(log);
            //

            return StatusCode(200, response);
        }



        // POST: Kafka
        /// <summary>Probar evento de Venta</summary>
        /// <param name="evento">Datos de la solicitud</param>
        /// <returns>---</returns>
        /// <response code="200">Solicitud procesada</response>
        /// <response code="202">Solicitud no procesada</response>
        /// <response code="400">Problemas con la solicitud</response>
        /// <response code="401">Falta de permisos</response>
        /// <response code="500">Error Interno</response>

        [HttpPost("/test/producto/estado")]
        public ActionResult<string> ProbarKafka(EventBase<EstadoProductoCmd> evento)
        {
            bool esError = true;
            string response = "Descripción del evento enviado por el servicio Kafka/test/producto/estado";
            try
            {
                evento.Evento = "EstadoActualizado";
                evento.Fecha = DateTime.Now.Date;
                evento.Origen = "api-inventarios";
                evento.Topico = "TP_Inventario";

                var json = JsonConvert.SerializeObject(evento);

                this._producer.Produce("TP_Inventario", new Message<string, string> { Key = "EstadoActualizado", Value = json });
                esError = false;

            }
            catch (Exception e)
            {
                Console.WriteLine($"MENSAJE:  '{e.Message}'  STACKTRACE:  '{e.StackTrace}' \n\n");
                _logger.LogError($"MENSAJE:  '{e.Message}'  STACKTRACE:  '{e.StackTrace}' \n\n\n");
                response = e.Message;
            }

            //response.date = DateTime.Now;

            //Auditoría
            var jrq = JsonConvert.SerializeObject(evento);
            var jrp = JsonConvert.SerializeObject(response);
            var host = "";
            try
            {
                var remoteIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                host = Dns.GetHostEntry(remoteIpAddress).HostName;
            }
            catch (Exception e) { }

            _AuditoriaInventarios log = new _AuditoriaInventarios("ProbarEstadoProducto", "", esError, "", MethodInfo.GetCurrentMethod().Name, this.ToString(), jrq, jrp, "Host: " + host, "");
            _ufwLog.Repository<_AuditoriaInventarios>().InsertOne(log);
            //

            return StatusCode(200, response);
        }



    }
}
