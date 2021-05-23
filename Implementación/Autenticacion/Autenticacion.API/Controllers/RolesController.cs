using Autenticacion.Dominio.IServices.Queries;
using Autenticacion.Dominio.Modelo;
using Autenticacion.Dominio.Modelo.Command;
using Autenticacion.Dominio.Modelo.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autenticacion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesServiceQuery _rolesService;

        public RolesController(IRolesServiceQuery rolesService)
        {
            this._rolesService = rolesService;

        }

        // GET: Roles/aplicacion/{idAplicacion}
        /// <summary>Consultar Roles por aplicación</summary>
        /// <param name="idAplicacion">Identificador de aplicación</param>
        /// <returns>Roles de la aplicación</returns>
        /// <response code="200">Solicitud procesada</response>
        /// <response code="400">Problemas con la solicitud</response>
        /// <response code="401">Falta de permisos</response>
        /// <response code="500">Error Interno</response>

        [HttpGet("aplicacion/{idAplicacion}")]
        public ActionResult<ResponseBase<IEnumerable<RolQuery>>>  VerRoles(string idAplicacion)
        {
            ResponseBase<IEnumerable<RolQuery>> response = new ResponseBase<IEnumerable<RolQuery>>();
            response.code = 500;

            try 
            { 
                response.data = _rolesService.rolesPorAplicacion(idAplicacion);
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



        // GET: Roles/usuario/{idAplicacion}
        /// <summary>Consultar Roles de un Usuario</summary>
        /// <param name="usuario">Nombre de ususario</param>
         /// <param name="idAplicacion">Aplicación</param>
        /// <returns>Roles del usuario</returns>
        /// <response code="200">Solicitud procesada</response>
        /// <response code="400">Problemas con la solicitud</response>
        /// <response code="401">Falta de permisos</response>
        /// <response code="500">Error Interno</response>

        [HttpGet("usuario")]
        public ActionResult<ResponseBase<UsuarioQuery>> VerRolesPorUsuario(string usuario, string idAplicacion)
        {
            ResponseBase<UsuarioQuery> response = new ResponseBase<UsuarioQuery>();
            response.code = 500;

            try
            {
                if(idAplicacion == null)
                {
                    response.data = _rolesService.verRolesPorUsuario(usuario);
                }
                else
                {
                    response.data = _rolesService.verRolesPorUsuario_Aplicacion(usuario, idAplicacion); 
                }

                
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

        //// POST: Roles       
        ///// <summary>Crear un rol para una aplicación</summary>
        ///// <param name="rol">Configuracipon del rol</param>
        ///// <returns>Resultado de la operación</returns>
        ///// <response code="201">Rol creado</response>
        ///// <response code="202">La Aplicación no tiene habilitada la autenticación con JWT</response>
        ///// <response code="203">Token JWT inválido</response>
        ///// <response code="400">Problemas con la solicitud</response>
        ///// <response code="401">Falta de permisos</response>

        //[HttpPost]
        //[ProducesResponseType(typeof(IEnumerable<ResultadoResponse>), 201)]
        //public ResultadoResponse Post([FromBody] Rol rol)
        //{

        //    //Rol creado = negocio.role;
        //    //Response.StatusCode = 201;

        //    //if (conf == null)
        //    //{
        //    //    Response.StatusCode = 202;
        //    //}

        //    //return conf;
        //    return null;
        //}


    }
}
