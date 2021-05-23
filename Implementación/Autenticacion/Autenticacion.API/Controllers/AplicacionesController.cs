using Autenticacion.Dominio.IServices.Command;
using Autenticacion.Dominio.IServices.Queries;
using Autenticacion.Dominio.Modelo;
using Autenticacion.Dominio.Modelo.Command;
using Autenticacion.Dominio.Modelo.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace Autenticacion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AplicacionesController : ControllerBase
    {
        private readonly IAplicacionesServiceCmd _aplicacionesService;
        private readonly IAplicacionesServiceQuery _aplicacionesServiceQuery;
        private readonly ILogServiceCmd _logServiceCmd;
        

        public AplicacionesController(IAplicacionesServiceCmd aplicacionesService,
                                      IAplicacionesServiceQuery aplicacionesServiceQuery,
                                      ILogServiceCmd logServiceCmd)
        {
            this._aplicacionesService = aplicacionesService;
            this._aplicacionesServiceQuery = aplicacionesServiceQuery;
            this._logServiceCmd = logServiceCmd;

        }


        // GET: Aplicaciones/{idAplicacion}
        /// <summary>Consultar aplicación</summary>
        /// <param name="idAplicacion">Identificador de aplicación</param>
        /// <returns>Datos de la aplicación</returns>
        /// <response code="200">Solicitud procesada</response>
        /// <response code="400">Problemas con la solicitud</response>
        /// <response code="401">Falta de permisos</response>
        /// <response code="500">Error Interno</response>

        [HttpGet("{idAplicacion}")]
        public ActionResult<ResponseBase<AplicacionQuery>> VerRoles(string idAplicacion)
        {
            ResponseBase<AplicacionQuery> response = new ResponseBase<AplicacionQuery>();
            response.code = 500;

            try
            {
                response.data = _aplicacionesServiceQuery.consultarAplicacion(idAplicacion);
            }
            catch (Exception e)
            {
                response.message = e.Message;
            }

            if (response.data == null)
            {
                response.code = 202;
            }
            else
            {
                response.code = 200;
            }

            return StatusCode(response.code, response);
        }


        // POST: Aplicaciones
        /// <summary>Crear nueva aplicación</summary>
        /// <param name="request)">Datos de la Aplicación</param>
        /// <returns>Aplicación creada</returns>
        /// <response code="200">Solicitud procesada</response>
        /// <response code="202">Proceso no Completado</response>
        /// <response code="400">Problemas con la solicitud</response>
        /// <response code="401">Falta de permisos</response>
        /// <response code="500">Error Interno</response>

        [HttpPost()]
        public ActionResult<ResponseBase<AplicacionQuery>>  CrearAplicacion(RequestBase<AplicacionCmd> request)
        {
            ResponseBase<AplicacionQuery> response = new ResponseBase<AplicacionQuery>();
            response.code = 500;

            try
            {
                response.data = _aplicacionesService.registrarAplicacion(request.data);


                if (response.data == null)
                {
                    response.code = 202;
                    response.message = "No se ha podido completar el proceso";
                }
                else
                {
                    response.code = 200;
                    response.message = "Aplicación creada";
                }

            }
            catch (Exception e)
            {
                response.message = e.Message;
            }

            response.date = DateTime.Now;

            //Auditoría

            var jrq = JsonConvert.SerializeObject(request);
            var jrp = JsonConvert.SerializeObject(response);
            var host = "";
            try
            {
                var remoteIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                host = Dns.GetHostEntry(remoteIpAddress).HostName;
            }
            catch (Exception e) { }

            try
            {
                this._logServiceCmd.AgregarLog(new LogCmd { Tipo = "Request-Response", Usuario=request.usuario, Request=jrq, Response=jrp, Aplicacion="Autenticación.API", Metodo= MethodInfo.GetCurrentMethod().Name, Entidad=this.ToString(), EsExcepcion=false, Mensaje="", Parametros=""  });

            }
            catch (Exception e) { };


            return StatusCode(response.code, response);
        }




    }
}
