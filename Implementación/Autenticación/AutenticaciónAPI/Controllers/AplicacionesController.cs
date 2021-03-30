using DominioAuth.Modelo;
using DominioAuth.Response;
using DominioAuth.Tokens;
using Microsoft.AspNetCore.Mvc;
using NegocioAuth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;

namespace AutenticaciónAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AplicacionesController : ControllerBase
    {
        private readonly INegocioWrapper negocio;
        public AplicacionesController(INegocioWrapper negocioAuth)
        {
            this.negocio = negocioAuth;
        }


        // GET: Aplicaciones       
        /// <summary>Obtener la configuración para validar Tokens JWT de una Aplicación</summary>
        /// <param name="tokenR">Token JWT válido para la Aplicación especificada</param>
        /// <remarks>
        /// `Descripción:`  
        /// ```
        ///     Servicio disponible para consultar la información de configuración JWT de una aplicación específica.
        ///     La consulta consiste en los siguentes pasos:
        ///         * Validar que el token JWT esté vigente
        ///         * Consula la configuración de la aplicación
        ///         * Retorna la configuración para validar tokens JWT:
        ///             - Algoritmo
        ///             - Issuer
        ///             - Audience
        ///             - ClockSkew
        ///             - Roles
        ///  </remarks>
        /// <returns>Configuración de la aplicación</returns>
        /// <response code="201">Aplicación disponible</response>
        /// <response code="202">La Aplicación no tiene habilitada la autenticación con JWT</response>
        /// <response code="203">Token JWT inválido</response>
        /// <response code="400">Problemas con la solicitud</response>
        /// <response code="401">Falta de permisos</response>

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Configuracion>), 201)]
        public Configuracion Post([FromBody] TokenJWT tokenR)
        {
            if ((tokenR.IdAplicacion.Length == 0) || (tokenR.Token.Length == 0))
            {
                Response.StatusCode = 400;
                return null;
            }

            TokenJWT token = new TokenJWT();
            token.IdAplicacion = tokenR.IdAplicacion;
            token.Token = tokenR.Token;

            if (!negocio.Sesiones.validarTokenJWT(token).TokenValido)
            {
                Response.StatusCode = 203;
                return null;
            }

            Configuracion conf = negocio.Aplicaciones.verConfiguracion(token.IdAplicacion);
            Response.StatusCode = 201;

            if (conf == null)
            {
                Response.StatusCode = 202;
            }

            return conf;
        }



        // POST: Aplicaciones       
        /// <summary>Agregar Aplicación</summary>
        
        /// <response code="201">Aplicación Creada</response>
        /// <response code="202">Han habido problemas en la creación</response>
        /// <response code="400">Problemas con la solicitud</response>
        /// <response code="401">Falta de permisos</response>

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<ResultadoResponse>), 201)]
        public ResultadoResponse Crear([FromBody] Aplicacion aplicacion)
        {

            ResultadoResponse response = new ResultadoResponse();
            response.Proceso = "CREAR_APLICACION";
            string mensaje = "";
            string parametros = "";
            bool error = false;

            try
            {
                
                Aplicacion app = negocio.Aplicaciones.Crear(aplicacion);
                Response.StatusCode = 201;
                response.Mensaje = "Aplicación creada";

                if (app == null)
                {
                    Response.StatusCode = 202;
                    response.Mensaje = "Han habido problemas en la creación";
                }

            }
            catch (Exception e)
            {
                mensaje = e.Message;
                parametros = e.StackTrace;
                error = true;
            }

            //Auditoría
            var jrq = JsonConvert.SerializeObject(aplicacion);
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
                negocio.Log.Crear(new _LogAutenticacionAPI("Error", aplicacion.Usuario, "Autenticación API", MethodInfo.GetCurrentMethod().Name, this.ToString(), jrq, jrp, error, mensaje, parametros));
            }
            catch (Exception e) { };



            return response;

        }

        // POST: Llave
        /// <summary>Enviar llave secreta al email del contacto</summary>
        /// <param name="tokenR">Token JWT válido para la Aplicación especificada</param>
        /// <remarks>
        /// `Descripción:`  
        /// ```
        ///     Servicio habilitado para solicitaar el envío de los datos de configuración por medio de un correo electrónico.
        ///     La consulta consiste en los siguentes pasos:
        ///         * Validar que el token JWT esté vigente
        ///         * Consula la configuración de la aplicación
        ///         * Genera un archivo .txt contenido con la llave privada de JWT
        ///         * Envía la información de configuración al correo electronico del responsable:
        ///             - Algoritmo
        ///             - Issuer
        ///             - Audience
        ///             - ClockSkew
        ///             - Roles
        ///             - Llave privada
        ///  </remarks>
        /// <returns>Resultado del proceso</returns>
        /// <response code="200">Proceso completado</response>
        /// <response code="202">La Aplicación no tiene habilitada la autenticación con JWT</response>
        /// <response code="203">Token JWT inválido</response>
        /// <response code="400">Problemas con la solicitud</response>
        /// <response code="401">Falta de permisos</response>
        //[HttpPost]
        //[Route("llave")]
        //public ResponseCorreo Llave([FromBody] TokenJWT tokenR)
        //{


        //    if ((tokenR.Abreviacion.Length == 0) || (tokenR.Token.Length == 0))
        //    {
        //        Response.StatusCode = 400;
        //        return null;
        //    }

        //    TokenJWT token = new TokenJWT();
        //    token.Abreviacion = tokenR.Abreviacion;
        //    token.Token = tokenR.Token;

        //    if (!repositoryLogin.validarTokenJWT(token))
        //    {
        //        Response.StatusCode = 203;
        //        return null;
        //    }

        //    ResponseCorreo resultado = servicioCorreo.enviarLlaveSecreta(token.Abreviacion);

        //    if (resultado == null)
        //    {
        //        Response.StatusCode = 202;
        //    }

        //    return resultado;
        //}


        //[HttpPost]
        //[Route("llave")]
        //public ResponseCorreo Llave([FromBody] TokenJWT tokenR)
        //{


        //    if ((tokenR.Abreviacion.Length == 0) || (tokenR.Token.Length == 0))
        //    {
        //        Response.StatusCode = 400;
        //        return null;
        //    }

        //    TokenJWT token = new TokenJWT();
        //    token.Abreviacion = tokenR.Abreviacion;
        //    token.Token = tokenR.Token;

        //    if (!repositoryLogin.validarTokenJWT(token))
        //    {
        //        Response.StatusCode = 203;
        //        return null;
        //    }

        //    ResponseCorreo resultado = servicioCorreo.enviarLlaveSecreta(token.Abreviacion);

        //    if (resultado == null)
        //    {
        //        Response.StatusCode = 202;
        //    }

        //    return resultado;
        //}


    }
}
