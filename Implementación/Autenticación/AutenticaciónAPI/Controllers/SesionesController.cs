using DominioAuth.Modelo;
using DominioAuth.Request;
using DominioAuth.Response;
using DominioAuth.Tokens;
using Microsoft.AspNetCore.Mvc;
using NegocioAuth;
using System;
using System.Reflection;

namespace AutenticaciónAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SesionesController : ControllerBase
    {
        private readonly INegocioWrapper negocio;


        public SesionesController(INegocioWrapper negocioWrapper)
        {
            this.negocio = negocioWrapper;
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
            /// <response code="201">Usuario autenticado</response>
            /// <response code="202">Inicio de sesión inválido</response>
            /// <response code="203">Cuenta LDAP bloqueada</response>
            /// <response code="400">Problemas con la solicitud</response>
            /// <response code="401">Falta de permisos</response>
            [HttpPost]
            [ProducesResponseType(typeof(LoginResponse), 201)]
            [ProducesResponseType(typeof(LoginResponse), 202)]
            [ProducesResponseType(typeof(LoginResponse), 203)]
            public LoginResponse Post([FromBody] LoginRequest request)
            {
                LoginResponse response = null;
                string mensaje = "";
                string parametros = "";

                try
                {

                    if (!request.datosParaLogin())
                    {
                        Response.StatusCode = 400;
                        response.Mensaje = "Solicitud Inválida";
                        return response;
                    }

                    try
                    {
                        LoginResponse usuario = negocio.Sesiones.IniciarSesion(request);
                        Response.StatusCode = 201;

                    }
                    catch (Exception e)
                    {
                        Response.StatusCode = 202;
                        response.Mensaje = e.Message;
                        return response;
                    }

                    if (response == null)
                    {
                        response = new LoginResponse();
                        response.Autenticacion = false;
                        response.Bloqueado = false;
                        Response.StatusCode = 202;
                        response.Mensaje = "Inicio de Sesión Inválido";
                        return response;
                    }

                }
                catch (Exception e)
                {
                    mensaje = e.Message;   
                    parametros = e.StackTrace;
                    negocio.Log.Crear(new _LogAutenticacionAPI("Error", request.Usuario ,"Autenticación API", MethodInfo.GetCurrentMethod().Name, this.ToString(), true, mensaje, parametros));
                }
                           

            return response;

            }


    }
}
