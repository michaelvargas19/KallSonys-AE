using Catalogos.Dominio.IServices.Command;
using Catalogos.Dominio.IServices.Queries;
using Catalogos.Dominio.IUnitOfWorks;
using Catalogos.Dominio.Modelo;
using Catalogos.Dominio.Modelo.Command;
using Catalogos.Dominio.Modelo.Queries;
using Catalogos.Infraestructura.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Catalogos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogosController : ControllerBase
    {
        private readonly ICatalogosServiceCmd _catalogosServiceCmd;
        private readonly ICatalogosServiceQuery _catalogosServiceQuery;

        public CatalogosController(ICatalogosServiceCmd catalogosServiceCmd,
                                   ICatalogosServiceQuery catalogosServiceQuery)
        {
            this._catalogosServiceCmd = catalogosServiceCmd;
            this._catalogosServiceQuery = catalogosServiceQuery;
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



        // GET: Catalogos/{#}
        /// <summary>Consultar catálogo con el código</summary>
        /// <param name="codigo">Código del cátalogo</param>
        /// <returns>Catálogo correspondiente</returns>
        /// <response code="200">Solicitud procesada</response>
        /// <response code="202">Solicitud no procesada</response>
        /// <response code="400">Problemas con la solicitud</response>
        /// <response code="401">Falta de permisos</response>
        /// <response code="500">Error Interno</response>


        [HttpGet("codigo/{codigo}")]
        public ActionResult<CatalogoQuery> verCatalogo(string codigo)
        {


            ResponseBase<CatalogoQuery> response = new ResponseBase<CatalogoQuery>();
            response.code = 500;

            try
            {
                response.data = _catalogosServiceQuery.verCatalogoPorCodigo(codigo);
                //response.data = getCatalogosEjemplo().FirstOrDefault();

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



        

        //// POST: Catalogos
        ///// <summary>Agregar un catálogo</summary>
        ///// <param name="request">Datos de la solicitud</param>
        ///// <returns>Catalogo Creado</returns>
        ///// <response code="200">Solicitud procesada</response>
        ///// <response code="400">Problemas con la solicitud</response>
        ///// <response code="401">Falta de permisos</response>
        ///// <response code="500">Error Interno</response>

        //[HttpPost()]
        //public ActionResult<CatalogoQuery> AgregarCatalogo(RequestBase<CatalogoCmd> request)
        //{
        //    ResponseBase<CatalogoQuery> response = new ResponseBase<CatalogoQuery>();
        //    response.code = 500;

        //    try
        //    {
        //        response.data = _catalogosServiceCmd.AgregarCatalogo(request.data);
        //        //response.data = getCatalogosEjemplo().FirstOrDefault();


        //        if (response.data == null)
        //        {
        //            response.code = 202;
        //            response.message = "No se ha podido completar el proceso";
        //        }
        //        else
        //        {
        //            response.code = 200;
        //            response.message = "Catálogo creado";
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        response.message = e.Message;
        //    }

        //    response.date = DateTime.Now;

        //    ////Auditoría

        //    //var jrq = JsonConvert.SerializeObject(request);
        //    //var jrp = JsonConvert.SerializeObject(response);
        //    //var host = "";
        //    //try
        //    //{
        //    //    var remoteIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
        //    //    host = Dns.GetHostEntry(remoteIpAddress).HostName;
        //    //}
        //    //catch (Exception e) { }

        //    //try
        //    //{
        //    //    this._logServiceCmd.AgregarLog(new LogCmd { Tipo = "Request-Response", Usuario = request.usuario, Request = jrq, Response = jrp, Aplicacion = "Autenticación.API", Metodo = MethodInfo.GetCurrentMethod().Name, Entidad = this.ToString(), EsExcepcion = false, Mensaje = "", Parametros = "" });

        //    //}
        //    //catch (Exception e) { };


        //    return StatusCode(response.code, response);
        //}



        private IEnumerable<CatalogoQuery> getCatalogosEjemplo()
        {
            List<CatalogoQuery> catalogos = new List<CatalogoQuery>();
            CatalogoQuery c = null;

            // ---Catalogo 1
            c = new CatalogoQuery();
            c.Nombre = "Catalogo de Televisores One";
            c.CodigoCatalogo = "CAT001";
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
            c.CodigoCatalogo = "CAT002";
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
            c.CodigoCatalogo = "CAT003";
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
