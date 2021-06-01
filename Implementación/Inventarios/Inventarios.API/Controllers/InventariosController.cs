using Inventarios.Dominio.IServices.Command;
using Inventarios.Dominio.IServices.Queries;
using Inventarios.Dominio.Modelo;
using Inventarios.Dominio.Modelo.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Inventarios.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventariosController : ControllerBase
    {
        private readonly IInventariosServiceCmd _inventariosServiceCmd;
        private readonly IInventariosServiceQuery _inventariosServiceQuery;

        public InventariosController(IInventariosServiceCmd inventariosServiceCmd,
                                     IInventariosServiceQuery inventariosServiceQuery)
        {
            this._inventariosServiceCmd = inventariosServiceCmd;
            this._inventariosServiceQuery = inventariosServiceQuery;
        }

        // GET: api/Inventarios/existencias
        /// <summary>Consultar disponibilidad en almacen de un producto</summary>
        /// <param name="sku">Códigos SKU de los productos</param>
        /// <response code="200">Resultado de disponibilidad en almacen</response>
        /// <response code="202">Han habido problemas en la creación</response>
        /// <response code="400">Problemas con la solicitud</response>
        /// <response code="401">Falta de permisos</response>

        [HttpGet("existencias/sku")]
        public ActionResult<IEnumerable<ExistenciaProducto>> ConsultarDisponibilidadSKUs([FromQuery] string[] sku)
        {
            ResponseBase<IEnumerable<ExistenciaProducto>> response = new ResponseBase<IEnumerable<ExistenciaProducto>>();
            response.code = 500;

            try
            {
                response.data = this._inventariosServiceQuery.ConsultarDisponibilidad(sku);
                
                if (response.data == null)
                {
                    response.code = 202;
                    response.message = "No se ha podido completar el proceso";
                }
                else
                {
                    response.code = 200;
                    response.message = "Proceso completado";
                }

            }
            catch (Exception e)
            {
                response.message = e.Message;
            }

            response.date = DateTime.Now;


            return StatusCode(response.code, response);
        }



        // GET: api/Inventarios/existencias
        /// <summary>Consultar disponibilidad en almacen de un producto</summary>
        /// <param name="sku">Código SKU del producto</param>
        /// <response code="200">Resultado de disponibilidad en almacen</response>
        /// <response code="202">Han habido problemas en la creación</response>
        /// <response code="400">Problemas con la solicitud</response>
        /// <response code="401">Falta de permisos</response>
        
        [HttpGet("existencias/sku/{sku}")]
        public ActionResult<ExistenciaProducto> ConsultarDisponibilidadSKU(string sku)
        {
            ResponseBase<ExistenciaProducto> response = new ResponseBase<ExistenciaProducto>();
            response.code = 500;

            try
            {
                
                response.data = this._inventariosServiceQuery.ConsultarDisponibilidadSKU(sku);
                                                
                if (response.data == null)
                {
                    response.code = 202;
                    response.message = "No se ha podido completar el proceso";
                }
                else
                {
                    response.code = 200;
                    response.message = "Catálogo creado";
                }

            }
            catch (Exception e)
            {
                response.message = e.Message;
            }

            response.date = DateTime.Now;


            return StatusCode(response.code, response);
        
        }


        
    }
}
