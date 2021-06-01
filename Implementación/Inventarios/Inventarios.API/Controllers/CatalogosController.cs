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
using System.Linq;
using System.Net;
using System.Reflection;

namespace Inventarios.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogosController : ControllerBase
    {

        private readonly ICatalogosServiceCmd _catalogosServiceCmd;
        private readonly ICatalogosServiceQuery _catalogosServiceQuery;
        private readonly IUnitOfWork<_AuditoriaInventarios> _ufwLog;

        public CatalogosController(ICatalogosServiceCmd catalogosServiceCmd,
                                   ICatalogosServiceQuery catalogosServiceQuery,
                                   IUnitOfWork<_AuditoriaInventarios> ufwLog)
        {
            this._catalogosServiceCmd = catalogosServiceCmd;
            this._catalogosServiceQuery = catalogosServiceQuery;
            this._ufwLog = ufwLog;
        }


        // GET: Catalogos?skip={#}&take={#}
        /// <summary>Consultar catálogos paginando</summary>
        /// <param name="skip">Cantidad de datos a omitir</param>
        /// <param name="take">Cantidad de datos a tomar</param>
        /// <returns>Catálogos correspondientes</returns>
        /// <response code="200">Solicitud procesada</response>
        /// <response code="202">Solicitud no procesada</response>
        /// <response code="400">Problemas con la solicitud</response>
        /// <response code="401">Falta de permisos</response>
        /// <response code="500">Error Interno</response>


        [HttpGet()]
        public ActionResult<IEnumerable<CatalogoQuery>> verPaginacion(int skip, int take)
        {


            ResponseBase<IEnumerable<CatalogoQuery>> response = new ResponseBase<IEnumerable<CatalogoQuery>>();
            response.code = 500;

            try
            {
                response.data = _catalogosServiceQuery.verPaginacion(skip, take);
                //response.data = getCatalogosEjemplo();
            }
            catch (Exception e)
            {
                response.message = e.Message;
            }

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

            response.date = DateTime.Now;


            return StatusCode(response.code, response);

        }



        // POST: Catalogos
        /// <summary>Crear un catálogo</summary>
        /// <param name="request">Datos de la solicitud</param>
        /// <returns>Catalogo Creado</returns>
        /// <response code="200">Solicitud procesada</response>
        /// <response code="202">Solicitud no procesada</response>
        /// <response code="400">Problemas con la solicitud</response>
        /// <response code="401">Falta de permisos</response>
        /// <response code="500">Error Interno</response>

        [HttpPost()]
        public ActionResult<CatalogoQuery> CrearCatalogo(RequestBase<CatalogoCmd> request)
        {
            bool esError = true;
            ResponseBase<CatalogoQuery> response = new ResponseBase<CatalogoQuery>();
            response.code = 500;

            try
            {
                response.data = _catalogosServiceCmd.AgregarCatalogo(request.data);
                //response.data = getCatalogosEjemplo().FirstOrDefault();


                if (response.data == null)
                {
                    response.code = 202;
                    response.message = "No se ha podido completar el proceso";
                    
                }
                else
                {
                    response.code = 200;
                    response.message = "Catálogo creado";
                    esError = false;
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

            _AuditoriaInventarios log = new _AuditoriaInventarios("CrearCatalogo", (response.data != null) ? response.data.CodigoCatalogo : "", esError, request.usuario, MethodInfo.GetCurrentMethod().Name, this.ToString(), jrq, jrp, "Host: "+host, "");
            _ufwLog.Repository<_AuditoriaInventarios>().InsertOne(log);
            //

            return StatusCode(response.code, response);
        }


        // UPDATE: Catalogos
        /// <summary>Actualizar un catálogo</summary>
        /// <param name="request">Datos de la solicitud</param>
        /// <returns>Catálogo Actualizado</returns>
        /// <response code="200">Solicitud procesada</response>
        /// <response code="202">Solicitud no procesada</response>
        /// <response code="400">Problemas con la solicitud</response>
        /// <response code="401">Falta de permisos</response>
        /// <response code="500">Error Interno</response>

        [HttpPut()]
        public ActionResult<CatalogoQuery> ActualizarCatalogo(RequestBase<CatalogoCmd> request)
        {
            bool esError = true;
            ResponseBase<CatalogoQuery> response = new ResponseBase<CatalogoQuery>();
            response.code = 500;

            try
            {
                response.data = _catalogosServiceCmd.ActualizarCatalogo(request.data);
                //response.data = getCatalogosEjemplo().FirstOrDefault();


                if (response.data == null)
                {
                    response.code = 202;
                    response.message = "No se ha podido completar el proceso";
                }
                else
                {
                    response.code = 200;
                    response.message = "Catálogo actualizado";
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

            _AuditoriaInventarios log = new _AuditoriaInventarios("AcualizarCatalogo", (response.data!=null)?response.data.CodigoCatalogo:"", esError, request.usuario, MethodInfo.GetCurrentMethod().Name, this.ToString(), jrq, jrp, "Host: " + host, "");
            _ufwLog.Repository<_AuditoriaInventarios>().InsertOne(log);
            //


            return StatusCode(response.code, response);
        }



        private IEnumerable<CatalogoQuery> getCatalogosEjemplo()
        {
            List<CatalogoQuery> catalogos = new List<CatalogoQuery>();
            CatalogoQuery c = null;

            // ---Catalogo 1
            c = new CatalogoQuery();
            c.Nombre = "Catalogo de Televisores One";
            c.Descripcion = "Este es un catálodo de televisores. Prueba.";
            c.FechaFin = DateTime.Now.AddYears(2);
            c.Calificacion = 1;
            c.IndExterno = false;
            c.Multimedia = new MultimediaQuery()
            {
                Nombre = "Imagen de ejemplo",
                Descripcion = "Esta es una imagen de pruebas",
                NombreTipo = TIPO_MULTIMEDIA.IMAGEN.ToString(),
                Tipo = TIPO_MULTIMEDIA.IMAGEN,
                url = "https://catalogosvirtualesonline.com/image.axd?picture=%2F2021%2F02%2Falkosto+26+febrero+2021+ofertas+colombia.jpg"
            };

            catalogos.Add(c);



            // ---Catalogo 2
            c = new CatalogoQuery();
            c.Nombre = "Catalogo de Televisores Two";
            c.Descripcion = "Este es un catálodo de televisores. Prueba.";
            c.FechaFin = DateTime.Now.AddYears(2);
            c.Calificacion = 1;
            c.IndExterno = false;
            c.Multimedia = new MultimediaQuery()
            {
                Nombre = "Imagen de ejemplo",
                Descripcion = "Esta es una imagen de pruebas",
                NombreTipo = TIPO_MULTIMEDIA.IMAGEN.ToString(),
                Tipo = TIPO_MULTIMEDIA.IMAGEN,
                url = "https://catalogosvirtualesonline.com/image.axd?picture=%2F2021%2F02%2Falkosto+26+febrero+2021+ofertas+colombia.jpg"
            };

            catalogos.Add(c);



            // ---Catalogo 3
            c = new CatalogoQuery();
            c.Nombre = "Catalogo de Televisores Three Xd";
            c.Descripcion = "Este es un catálodo de televisores. Prueba.";
            c.FechaFin = DateTime.Now.AddYears(2);
            c.Calificacion = 1;
            c.IndExterno = false;
            c.Multimedia = new MultimediaQuery()
            {
                Nombre = "Imagen de ejemplo",
                Descripcion = "Esta es una imagen de pruebas",
                NombreTipo = TIPO_MULTIMEDIA.IMAGEN.ToString(),
                Tipo = TIPO_MULTIMEDIA.IMAGEN,
                url = "https://catalogosvirtualesonline.com/image.axd?picture=catalogo_best_buy_ofertas_televisores_junio_2014_1.jpg"
            };

            catalogos.Add(c);


            return catalogos;
        }



    }
}
