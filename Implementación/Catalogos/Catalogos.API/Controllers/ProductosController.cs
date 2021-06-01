using Catalogos.Dominio.IServices.Command;
using Catalogos.Dominio.IServices.Queries;
using Catalogos.Dominio.Modelo;
using Catalogos.Dominio.Modelo.Command;
using Catalogos.Dominio.Modelo.Queries;
using Catalogos.Infraestructura.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalogos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductosServiceCmd _productosServiceCmd;
        private readonly IProductosServiceQuery _productosServiceQuery;

        public ProductosController(IProductosServiceCmd productosServiceCmd,
                                   IProductosServiceQuery productosServiceQuery)
        {
            this._productosServiceCmd = productosServiceCmd;
            this._productosServiceQuery = productosServiceQuery;
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


        // GET: Productos/ranking/fulltext?texto={''}&skip={#}&take={#}
        /// <summary>Consultar productos por texto</summary>
        /// <param name="texto">Texto de búsqueda</param>
        /// <param name="skip">Cantidad de datos a omitir</param>
        /// <param name="take">Cantidad de datos a tomar</param>
        /// <returns>Productos encontrados</returns>
        /// <response code="200">Solicitud procesada</response>
        /// <response code="202">Solicitud no procesada</response>
        /// <response code="400">Problemas con la solicitud</response>
        /// <response code="401">Falta de permisos</response>
        /// <response code="500">Error Interno</response>

        [HttpGet("ranking/fulltext")]
        public ActionResult<IEnumerable<ProductoQuery>> verRankingFullText(string texto, int skip, int take)
        {

            ResponseBase<IEnumerable<ProductoQuery>> response = new ResponseBase<IEnumerable<ProductoQuery>>();
            response.code = 500;

            try
            {
                response.data = _productosServiceQuery.verRankingFullText(texto, skip, take);
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



        // GET: Productos/ranking/marca?marca={''}&skip={#}&take={#}
        /// <summary>Consultar productos por marca</summary>
        /// <param name="marca">Marca de búsqueda</param>
        /// <param name="skip">Cantidad de datos a omitir</param>
        /// <param name="take">Cantidad de datos a tomar</param>
        /// <returns>Productos encontrados</returns>
        /// <response code="200">Solicitud procesada</response>
        /// <response code="202">Solicitud no procesada</response>
        /// <response code="400">Problemas con la solicitud</response>
        /// <response code="401">Falta de permisos</response>
        /// <response code="500">Error Interno</response>

        [HttpGet("ranking/marca")]
        public ActionResult<IEnumerable<ProductoQuery>> verRankingMarca(string marca, int skip, int take)
        {

            ResponseBase<IEnumerable<ProductoQuery>> response = new ResponseBase<IEnumerable<ProductoQuery>>();
            response.code = 500;

            try
            {
                response.data = _productosServiceQuery.verRankingMarca(marca, skip, take);
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



        // GET: Productos/{id}
        /// <summary>Consultar producto por Id</summary>
        /// <param name="codigo">Código del producto</param>
        /// <returns>Detalles del Producto</returns>
        /// <response code="200">Solicitud procesada</response>
        /// <response code="202">Solicitud no procesada</response>
        /// <response code="400">Problemas con la solicitud</response>
        /// <response code="401">Falta de permisos</response>
        /// <response code="500">Error Interno</response>

        [HttpGet("codigo/{codigo}")]
        public ActionResult<ProductoQuery> VerProductoPorId(string codigo)
        {
            ResponseBase<ProductoQuery> response = new ResponseBase<ProductoQuery>();
            response.code = 500;

            try
            {
                response.data = _productosServiceQuery.verProductoPorCodigo(codigo);
                //response.data = getProductosEjemplo().FirstOrDefault();

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



        // GET: Productos/sku/{sku}
        /// <summary>Consultar producto por Id</summary>
        /// <param name="sku">Código SKU del producto</param>
        /// <returns>Detalles del Producto</returns>
        /// <response code="200">Solicitud procesada</response>
        /// <response code="400">Problemas con la solicitud</response>
        /// <response code="401">Falta de permisos</response>
        /// <response code="500">Error Interno</response>

        [HttpGet("sku/{sku}")]
        public ActionResult<ProductoQuery> VerProductoPorSKU(string sku)
        {
            ResponseBase<ProductoQuery> response = new ResponseBase<ProductoQuery>();
            response.code = 500;

            try
            {
                response.data = _productosServiceQuery.verProductoPorSKU(sku);
                //response.data = getProductosEjemplo().FirstOrDefault();

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



        //// POST: Productos
        ///// <summary>Agregar un producto</summary>
        ///// <param name="request">Datos de la solicitud</param>
        ///// <returns>Producto Agregado</returns>
        ///// <response code="200">Solicitud procesada</response>
        ///// <response code="400">Problemas con la solicitud</response>
        ///// <response code="401">Falta de permisos</response>
        ///// <response code="500">Error Interno</response>

        //[HttpPost()]
        //public ActionResult<ProductoQuery> AgregarProducto(RequestBase<ProductoCmd> request)
        //{
        //    ResponseBase<ProductoQuery> response = new ResponseBase<ProductoQuery>();
        //    response.code = 500;

        //    try
        //    {
        //        response.data = _productosServiceCmd.AgregarProducto(request.data);
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





        private IEnumerable<ProductoQuery> getProductosEjemplo()
        {
            List<ProductoQuery> productos = new List<ProductoQuery>();
            ProductoQuery p = null;

            // ---Producto 1
            p = new ProductoQuery();
            p.Nombre = "Televisor QLED - Q80R";
            p.Descripcion = "TV con resolución 8K lanzado inicialmente en 75”, ahora también disponible 65 y 55 pulgadas tiene una resolución cuatro veces mayor al UHD 4K. Además cuenta con inteligencia artificial aplicada a imagen, sonido y modo ambiente";
            p.TipoProducto = "Televisor";
            p.sku = "P001";
            p.CodigoProducto = "001";
            p.iva = 0;
            p.PesoKg = 1;
            p.ValorUnitario = 5000;
            p.EnAlmacen = true;
            p.IndExterno = false;
            p.Descuentos = new DescuentoQuery();
            p.Descuentos.Nombre = "";
            p.Descuentos.Descripcion = "";
            p.Descuentos.Porcentaje = 0;
            p.Descuentos.MediosDePago = new List<MEDIO_PAGO>() { MEDIO_PAGO.CONTRA_ENTREGA, MEDIO_PAGO.PSE };
            p.Multimedia = new MultimediaQuery() 
                    { 
                        Nombre="Imagen de ejemplo",
                        Descripcion="Esta es una imagen de pruebas",
                        NombreTipo= TIPO_MULTIMEDIA.IMAGEN.ToString(),
                        Tipo=TIPO_MULTIMEDIA.IMAGEN,
                        url= "https://cr00.epimg.net/radio/imagenes/2019/07/20/tecnologia/1563574963_042001_1563575140_noticia_normal.jpg"
                    };
            p.Marca = "Samsung";
            p.Calificacion = 1;

            productos.Add(p);

            // ---Producto 2
            p = new ProductoQuery();
            p.Nombre = "Televisor QLED - Q80R";
            p.Descripcion = "Resolución UHD 4K con sistema HDR de 1500 nits para potenciar el brillo y Direct Full Array detallado para escenas oscuras. Tiene inteligencia artificial en imagen, audio y modo ambiente. Cuenta además con pantalla antirreflejo que reduce luz exterior hasta en un 40%. Disponible en 75 pulgadas";
            p.TipoProducto = "Televisor";
            p.sku = "P002";
            p.CodigoProducto = "002";
            p.iva = 0;
            p.PesoKg = 1;
            p.ValorUnitario = 5000;
            p.EnAlmacen = true;
            p.IndExterno = false;
            p.Descuentos = new DescuentoQuery();
            p.Descuentos.Nombre = "";
            p.Descuentos.Descripcion = "";
            p.Descuentos.Porcentaje = 0;
            p.Descuentos.MediosDePago = new List<MEDIO_PAGO>() { MEDIO_PAGO.CONTRA_ENTREGA, MEDIO_PAGO.PSE };
            p.Multimedia = new MultimediaQuery()
            {
                Nombre = "Imagen de ejemplo",
                Descripcion = "Esta es una imagen de pruebas",
                NombreTipo = TIPO_MULTIMEDIA.IMAGEN.ToString(),
                Tipo = TIPO_MULTIMEDIA.IMAGEN,
                url = "https://olimpica.vtexassets.com/arquivos/ids/184924/Televisor-FHD-OLIMPO-100Cm-40----L40D2200S-Smartv.jpg?v=636864666579170000"
            };
            p.Marca = "Samsung";
            p.Calificacion = 1;

            productos.Add(p);

            // ---Producto 3
            p = new ProductoQuery();
            p.Nombre = "Televisor QLED Q70R";
            p.Descripcion = "Televisor 4K, disponible en 55 y 65 pulgadas con HDR de 1000 nits y Full Array, Procesador Quantum e inteligencia artificial para imagen, audio y modo ambiente.";
            p.TipoProducto = "Televisor";
            p.sku = "P003";
            p.CodigoProducto = "003";
            p.iva = 0;
            p.PesoKg = 1;
            p.ValorUnitario = 5000;
            p.EnAlmacen = true;
            p.IndExterno = false;
            p.Descuentos = new DescuentoQuery();
            p.Descuentos.Nombre = "";
            p.Descuentos.Descripcion = "";
            p.Descuentos.Porcentaje = 0;
            p.Descuentos.MediosDePago = new List<MEDIO_PAGO>() { MEDIO_PAGO.CONTRA_ENTREGA, MEDIO_PAGO.PSE };
            p.Multimedia = new MultimediaQuery()
            {
                Nombre = "Imagen de ejemplo",
                Descripcion = "Esta es una imagen de pruebas",
                NombreTipo = TIPO_MULTIMEDIA.IMAGEN.ToString(),
                Tipo = TIPO_MULTIMEDIA.IMAGEN,
                url = "https://i2.wp.com/todocali.com/wp-content/uploads/2020/08/tu8000-7_1.jpg?fit=700%2C700&ssl=1"
            };
            p.Marca = "Samsung";
            p.Calificacion = 1;

            productos.Add(p);

            return productos;
        }

    }
}
