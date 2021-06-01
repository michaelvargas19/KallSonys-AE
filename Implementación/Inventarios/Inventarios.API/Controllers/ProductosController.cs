using Inventarios.Dominio.IServices.Command;
using Inventarios.Dominio.IServices.Queries;
using Inventarios.Dominio.IUnitOfWorks;
using Inventarios.Dominio.Modelo;
using Inventarios.Dominio.Modelo.Command;
using Inventarios.Dominio.Modelo.Queries;
using Inventarios.Infraestructura.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;

namespace Inventarios.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductosServiceCmd _productosServiceCmd;
        private readonly IProductosServiceQuery _productosServiceQuery;
        private readonly IUnitOfWork<_AuditoriaInventarios> _ufwLog;

        public ProductosController(IProductosServiceCmd productosServiceCmd,
                                   IProductosServiceQuery productosServiceQuery,
                                   IUnitOfWork<_AuditoriaInventarios> ufwLog)
        {
            this._productosServiceCmd = productosServiceCmd;
            this._productosServiceQuery = productosServiceQuery;
            this._ufwLog = ufwLog;
        }


        // GET: Productos/paginacion?skip={#}&take={#}
        /// <summary>Consultar productos paginandos</summary>
        /// <param name="skip">Cantidad de datos a omitir</param>
        /// <param name="take">Cantidad de datos a tomar</param>
        /// <returns>Productos correspondientes</returns>
        /// <response code="200">Solicitud procesada</response>
        /// <response code="202">Solicitud no procesada</response>
        /// <response code="400">Problemas con la solicitud</response>
        /// <response code="401">Falta de permisos</response>
        /// <response code="500">Error Interno</response>


        [HttpGet()]
        public ActionResult<IEnumerable<ProductoQuery>> verPaginacion(int skip, int take)
        {


            ResponseBase<IEnumerable<ProductoQuery>> response = new ResponseBase<IEnumerable<ProductoQuery>>();
            response.code = 500;

            try
            {
                response.data = _productosServiceQuery.verPaginacion(skip, take);
                //response.data = getProductosEjemplo();

                if (response.data == null)
                {
                    response.code = 202;
                    response.message = "No se ha podido completar la operación";
                }
                else
                {
                    response.code = 200;
                    response.message = "Proceso Completado";
                }

            }
            catch (Exception e)
            {
                response.message = e.Message;
            }

            response.date = DateTime.Now;


            return StatusCode(response.code, response);

        }


        // GET: Productos/ranking/catalogo?codigo={''}&skip={#}&take={#}
        /// <summary>Consultar ranking de productos</summary>
        /// <param name="codigo">Código del catálogo</param>
        /// <param name="skip">Cantidad de datos a omitir</param>
        /// <param name="take">Cantidad de datos a tomar</param>
        /// <returns>Productos correspondientes</returns>
        /// <response code="200">Solicitud procesada</response>
        /// <response code="202">Solicitud no procesada</response>
        /// <response code="400">Problemas con la solicitud</response>
        /// <response code="401">Falta de permisos</response>
        /// <response code="500">Error Interno</response>

        [HttpGet("ranking/catalogo")]
        public ActionResult<IEnumerable<ProductoQuery>> verRankingCatalogo(string codigo, int skip, int take)
        {

            ResponseBase<IEnumerable<ProductoQuery>> response = new ResponseBase<IEnumerable<ProductoQuery>>();
            response.code = 500;

            try
            {
                response.data = _productosServiceQuery.verRankingCatalogo(codigo, skip, take);
                //response.data = getProductosEjemplo();

                if (response.data == null)
                {
                    response.code = 202;
                    response.message = "No se ha podido completar la operación";
                }
                else
                {
                    response.code = 200;
                    response.message = "Proceso Completado";
                }

            }
            catch (Exception e)
            {
                response.message = e.Message;
            }

            response.date = DateTime.Now;


            return StatusCode(response.code, response);

        }


        // POST: Productos
        /// <summary>Agregar un producto</summary>
        /// <param name="request">Datos de la solicitud</param>
        /// <returns>Producto Agregado</returns>
        /// <response code="200">Solicitud procesada</response>
        /// <response code="400">Problemas con la solicitud</response>
        /// <response code="401">Falta de permisos</response>
        /// <response code="500">Error Interno</response>

        [HttpPost()]
        public ActionResult<ProductoQuery> AgregarProducto(RequestBase<ProductoCmd> request)
        {
            bool esError = true;
            ResponseBase<ProductoQuery> response = new ResponseBase<ProductoQuery>();
            response.code = 500;

            try
            {
                response.data = _productosServiceCmd.AgregarProducto(request.data);
                //response.data = getCatalogosEjemplo().FirstOrDefault();


                if (response.data == null)
                {
                    response.code = 202;
                    response.message = "No se ha podido completar el proceso";
                }
                else
                {
                    response.code = 200;
                    response.message = "Producto agregado";
                    esError = false;
                }

            }
            catch (Exception e)
            {
                response.message = e.Message;
            }

            response.date = DateTime.Now;

            //Auditoria
            var jrq = JsonConvert.SerializeObject(request);
            var jrp = JsonConvert.SerializeObject(response);
            var host = "";
            try
            {
                var remoteIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                host = Dns.GetHostEntry(remoteIpAddress).HostName;
            }
            catch (Exception e) { }

            _AuditoriaInventarios log = new _AuditoriaInventarios("AgregarProducto", (response.data != null) ? response.data.CodigoCatalogo : "", esError, request.usuario, MethodInfo.GetCurrentMethod().Name, this.ToString(), jrq, jrp, "Host: " + host, "");
            _ufwLog.Repository<_AuditoriaInventarios>().InsertOne(log);
            //


            return StatusCode(response.code, response);
        }



        // UPDATE: Productos

        /// <summary>Actualizar un producto</summary>
        /// <param name="request">Datos de la solicitud</param>
        /// <returns>Producto Actualizadp</returns>
        /// <response code="200">Solicitud procesada</response>
        /// <response code="400">Problemas con la solicitud</response>
        /// <response code="401">Falta de permisos</response>
        /// <response code="500">Error Interno</response>

        [HttpPut()]
        public ActionResult<ProductoQuery> ActualizarProducto(RequestBase<ProductoCmd> request)
        {
            bool esError = true;
            ResponseBase<ProductoQuery> response = new ResponseBase<ProductoQuery>();
            response.code = 500;

            try
            {
                response.data = _productosServiceCmd.ActualizarProducto(request.data);
                //response.data = getCatalogosEjemplo().FirstOrDefault();


                if (response.data == null)
                {
                    response.code = 202;
                    response.message = "No se ha podido completar el proceso";
                }
                else
                {
                    response.code = 200;
                    response.message = "Producto actualizado";
                    esError = false;
                }

            }
            catch (Exception e)
            {
                response.message = e.Message;
            }

            response.date = DateTime.Now;

            //Auditoria
            var jrq = JsonConvert.SerializeObject(request);
            var jrp = JsonConvert.SerializeObject(response);
            var host = "";
            try
            {
                var remoteIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                host = Dns.GetHostEntry(remoteIpAddress).HostName;
            }
            catch (Exception e) { }

            _AuditoriaInventarios log = new _AuditoriaInventarios("AcualizarProducto", (response.data != null) ? response.data.CodigoCatalogo : "", esError, request.usuario, MethodInfo.GetCurrentMethod().Name, this.ToString(), jrq, jrp, "Host: " + host, "");
            _ufwLog.Repository<_AuditoriaInventarios>().InsertOne(log);
            //


            return StatusCode(response.code, response);
        }




    }
}
