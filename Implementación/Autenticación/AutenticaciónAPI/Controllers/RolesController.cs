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
    public class RolesController : ControllerBase
    {
        private readonly INegocioWrapper negocio;
        public RolesController(INegocioWrapper negocioAuth)
        {
            this.negocio = negocioAuth;
        }


        // POST: Roles       
        /// <summary>Consultar Roles por identificación</summary>
        /// <param name="rol">Identificador de aplicación </param>
        /// <returns>Roles de la aplicación</returns>
        /// <response code="200">Solicitud procesada</response>
        /// <response code="400">Problemas con la solicitud</response>
        /// <response code="401">Falta de permisos</response>

        [HttpPost("aplicacion")]
        [ProducesResponseType(typeof(IEnumerable<ResultadoResponse>), 201)]
        public IEnumerable<Rol> VerRoles([FromBody] string idAplicacion)
        {

            IEnumerable<Rol> roles = null;
            Response.StatusCode = 201;

            if (roles == null)
            {
                Response.StatusCode = 202;
            }

            
            return roles;
        }

        // POST: Roles       
        /// <summary>Crear un rol para una aplicación</summary>
        /// <param name="rol">Configuracipon del rol</param>
        /// <returns>Resultado de la operación</returns>
        /// <response code="201">Rol creado</response>
        /// <response code="202">La Aplicación no tiene habilitada la autenticación con JWT</response>
        /// <response code="203">Token JWT inválido</response>
        /// <response code="400">Problemas con la solicitud</response>
        /// <response code="401">Falta de permisos</response>

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<ResultadoResponse>), 201)]
        public ResultadoResponse Post([FromBody] Rol rol)   
        {

            //Rol creado = negocio.role;
            //Response.StatusCode = 201;

            //if (conf == null)
            //{
            //    Response.StatusCode = 202;
            //}

            //return conf;
            return null;
        }


    }
}
