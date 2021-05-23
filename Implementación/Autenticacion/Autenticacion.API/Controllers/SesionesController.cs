using Autenticacion.Dominio.IServices.Command;
using Autenticacion.Dominio.IUnitOfWorks;
using Autenticacion.Dominio.Modelo;
using Autenticacion.Dominio.Modelo.Command;
using Autenticacion.Infraestructura.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Autenticacion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SesionesController : ControllerBase
    {
        private readonly ISesionesServiceCmd _sesionesService;
        

        public SesionesController(ISesionesServiceCmd sesionesService)
        {
            this._sesionesService = sesionesService;
            
        }

        // POST: Login
        /// <summary>Iniciar sesión con usuario y contraseña</summary>
        /// <remarks>
        /// `Descripción:`
        /// 
        ///     Este es el servicio habilitado para el inicio de sesión, apatir de un usuario y contraseña.
        ///     Los pasos que se ejecutan son:
        ///         * Recibir la solicitud de inicio de seción
        ///         * Validar existencia de la aplicación y Usuario
        ///         * Validar permiso del usuario en la aplicación
        ///         * Identificar la información del usuario
        ///         * Generar token JWT de sesión
        ///         * Retornar el token JWT de la sesión
        ///  
        /// `JSON de respuesta:`
        /// ```
        /// {
        ///      autenticacion":    Indica el resulado del inicio de sesión
        ///      tokenJWT":         Token JWT generado
        ///      mensaje":          Descripción del resultado
        ///      bloqueada":        Indica si la cuenta LDAP está bloqueada
        ///      urLdesbloqueo":    URL para desbloquear la cuenta LDAP
        ///  }
        ///  ```
        ///  </remarks>
        /// <param name="request">Datos para el inicio de sesión</param>
        /// <returns>Token JWT con datos de la sesión</returns>
        /// <response code="200">Usuario autenticado</response>
        /// <response code="202">Inicio de sesión inválido</response>
        /// <response code="203">Cuenta LDAP bloqueada</response>
        /// <response code="400">Problemas con la solicitud</response>
        /// <response code="401">Falta de permisos</response>
        [HttpPost]
        [ProducesResponseType(typeof(LoginResponse), 200)]
        [ProducesResponseType(typeof(LoginResponse), 202)]
        [ProducesResponseType(typeof(LoginResponse), 203)]
        public ActionResult<ResponseBase<LoginResponse>>  Post([FromBody] CredencialesLogin request)
        {
            ResponseBase<LoginResponse> response = new ResponseBase<LoginResponse>();


            //request.IdAplicacion = "Manager";
            //request.Usuario = "Admin";
            //request.Contrasena = "Scare2021";

            try
            {

                if (!request.datosParaLogin())
                {
                    response.code = 400;
                    response.message = "Solicitud Inválida";
                    return response;
                }

                try
                {
                    response.data = this._sesionesService.IniciarSesion(request);
                    
                    if (response.data == null)
                    {
                        response.data = new LoginResponse();
                        response.data.Autenticacion = false;
                        response.data.Bloqueado = false;
                        response.code = 202;
                        response.message = "Inicio de Sesión Inválido";
                        return response;
                    }

                    response.code = 200;

                }
                catch (Exception e)
                {
                    response.code = 202;
                    response.message = e.Message;
                    return response;
                }

                

            }
            catch (Exception e)
            {
                //_ufw.InsertarLog(new _LogAutenticacionAPI("Error", request.Usuario, "Autenticación API", MethodInfo.GetCurrentMethod().Name, this.ToString(), true, e.Message, e.TargetSite.ToString()));
            }


            return response;

        }


    }

}
