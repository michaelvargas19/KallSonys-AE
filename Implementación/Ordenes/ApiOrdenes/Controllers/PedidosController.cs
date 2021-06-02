using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ordenes.Dominio.IUnitOfWorks;
using Ordenes.Dominio.Models;

namespace ApiOrdenes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly IUnitOfWork<Pedido> _unitOfWork;
        private readonly IUnitOfWork<Orden> _unitOfWork_Orden;
        private readonly IUnitOfWork<Pedido_Ref_Estados> _unitOfWork_Estado;
        private readonly IUnitOfWork<Orden_Ref_Estados> _unitOfWork_orden;
        private readonly IUnitOfWork<Orden_Item_Ref_Estados> _unitOfWork_estado_item;



        public PedidosController(IUnitOfWork<Orden> unitOfWork_Orden, IUnitOfWork<Pedido> unitOfWork, IUnitOfWork<Pedido_Ref_Estados> unitOfWork_Estado, IUnitOfWork<Orden_Ref_Estados> unitOfWork_orden,
            IUnitOfWork<Orden_Item_Ref_Estados> unitOfWork_estado_item)
        {
            _unitOfWork = unitOfWork;
            _unitOfWork_Orden = unitOfWork_Orden;
            _unitOfWork_Estado = unitOfWork_Estado;
            _unitOfWork_orden = unitOfWork_orden;
            _unitOfWork_estado_item = unitOfWork_estado_item;

        }


        [HttpPost]
        public async Task<Pedido> NuevoPedido(List<ObjEntradaProducto> productos)
        {

            List<Orden> ordenes = new List<Orden>();
            List<string> proveedoresNoRepetidos = new List<string>();
            proveedoresNoRepetidos.Add("");
            foreach (var item in productos)
            {
                Orden objOrden = new Orden();
                Orden_Items objItem = new Orden_Items();
                objOrden.items = new List<Orden_Items>();

                if (!proveedoresNoRepetidos.Contains(item.id_proveedor))
                {

                    proveedoresNoRepetidos.Add(item.id_proveedor);

                    objOrden.id_cliente = item.id_cliente;
                    objOrden.id_proveedor = item.id_proveedor;

                    objItem.id_producto = item.id_producto;
                    objItem.cantidad = item.cantidad;
                    objItem.precio_unitario = item.precio_unitario;

                    objOrden.items.Add(objItem);
                    ordenes.Add(objOrden);

                }
                else
                {
                    objOrden = ordenes.Find(x => x.id_proveedor == item.id_proveedor);
                    objOrden.id_cliente = item.id_cliente;
                    objOrden.id_proveedor = item.id_proveedor;

                    objItem.id_producto = item.id_producto;
                    objItem.cantidad = item.cantidad;
                    objItem.precio_unitario = item.precio_unitario;

                    objOrden.items.Add(objItem);
                }

            }
            List<Orden> respuesta = new List<Orden>();
            //OrdenesController ordenNueva = new OrdenesController(_unitOfWork_Orden, _unitOfWork, _unitOfWork_orden, _unitOfWork_estado_item);
            for (int i = 0; i < ordenes.Count(); i++)
            {
                respuesta.Add(await AgregarOrden(ordenes[i]));
            }


            //Crea Pedido de acuerdo a las ordenes asociadas
           
            Pedido nvoPedido = new Pedido();
            Orden ordenPedido = new Orden();
            nvoPedido.id_orden = new List<string>();

            nvoPedido.fecha_pedido = System.DateTime.Now;


            foreach (var item in respuesta)
            {
                nvoPedido.id_orden.Add(item.Id);
            }
            await this.CrearPedido(nvoPedido);

            return nvoPedido;
        }
        private async Task<Orden> AgregarOrden(Orden objCompra)
        {
            Orden orden = new Orden();
            try
            {
                orden.items = new List<Orden_Items>();


                orden.Id = orden.GetInternalId("ORD");
                orden.fecha_orden = System.DateTime.Now;
                orden.id_cliente = objCompra.id_cliente;
                orden.id_proveedor = objCompra.id_proveedor;
                foreach (var item in objCompra.items)
                {
                    Orden_Items items = new Orden_Items();
                    items.Id = items.GetInternalId("ITEM");
                    items.id_producto = item.id_producto;
                    items.cantidad = item.cantidad;
                    items.precio_unitario = item.precio_unitario;
                    items.estado_item = item.estado_item;
                    orden.items.Add(items);
                }
                //ICollection<Orden> nuevoRegistro = 
                await _unitOfWork_Orden.Service_Commands<Orden>().InsertOneAsync(orden);
            }
            catch (Exception error)
            {
                throw new Exception(error.InnerException + error.Message);
            }
            return orden;
        }

        private async Task<Pedido> CrearPedido(Pedido objPedido)
        {
            try
            {
                objPedido.Id = objPedido.GetInternalId("PED");
                await _unitOfWork.Service_Commands<Pedido>().InsertOneAsync(objPedido);


            }
            catch (Exception error)
            {
                throw new Exception("Error al momento de ingresar el registro: " + error.Message + "," + error.InnerException);

            }
            return objPedido;
        }

        //Cambiar estado pedido
        //[Route("[action]")]
        [HttpPut]
        public ActionResult<Pedido> ActualizarEstadoPedido(objActalizarEstado objEstado)
        {
            Pedido pedido = new Pedido();
            try
            {
                pedido = _unitOfWork.Service_Queries<Pedido>().FindById(objEstado.id);
                if(pedido != null)
                {
                    var estadoExiste = _unitOfWork_Estado.Service_Queries<Pedido_Ref_Estados>().FindOne(x => x.codigo_estado_pedido == objEstado.nuevoEstado);
                    if(estadoExiste != null)
                    {
                        pedido.id_estado_pedido = objEstado.nuevoEstado;
                        pedido.fecha_actualizacion = System.DateTime.Now;
                    }
                    else
                    {
                        return  NotFound("Estado no existe");
                    }
                    
                }
                else
                {
                    return NotFound("Pedido no encontrado");
                } 
            }
            catch (Exception error)
            {
                throw new Exception("Error al momento de ingresar el registro: " + error.Message + "," + error.InnerException);

            }
            return Ok(pedido);
        }

        //Buscar Pedidos
        [HttpGet]
        public IQueryable<Pedido> ObtenerPedidos()
        {
            return _unitOfWork.Service_Queries<Pedido>().AsQueryable();
        }

        
        //Buscar pedido por cliente
        [Route("[action]/{idCliente}")]
        [HttpGet]
        public async Task<ActionResult<List<Pedido>>> ObtenerPedidoPorCliente(string idCliente)
        {
            List<string> ordenes = new List<string>();
            List<Pedido> resultado = new List<Pedido>();
            Pedido pedido = new Pedido();

            var orden = _unitOfWork_Orden.Service_Queries<Orden>().AsQueryable().Where(x => x.id_cliente == idCliente).ToList();
            if (orden != null)
            {
                foreach (var itemOrden in orden)
                {
                    //ordenes.Add(itemOrden.Id);
                    pedido = await _unitOfWork.Service_Queries<Pedido>().FindOneAsync(x => x.id_orden.Contains(itemOrden.Id));
                    if(pedido != null)
                    {
                        if(resultado.Count() != 0)
                        {
                            for (int i = 0; i < resultado.Count(); i++)
                            {
                                if (pedido.Id != resultado[i].Id)
                                {
                                    resultado.Add(pedido);
                                }
                            }
                        }
                        else
                        {
                            resultado.Add(pedido);
                        }
                    }
                    else
                    {
                        return NotFound("Pedido no encontrado");
                    }
                }   
               
            }
            else
            {
                
                return NotFound("Orden no encontrada");
            }
                
            
            return Ok(resultado);
        }

        //Buscar pedido por orden
        [Route("[action]/{id_orden}")]
        [HttpGet]
        public async Task<ActionResult<Pedido>> ObtenerPedidoPorOrden(string id_orden)
        {
            Pedido resultado = new Pedido();
            var orden = await _unitOfWork_Orden.Service_Queries<Orden>().FindByIdAsync(id_orden);
            if(orden != null)
            {
                resultado = await _unitOfWork.Service_Queries<Pedido>().FindOneAsync(x => x.id_orden.Contains(orden.Id));
            }
            else
            {
                return NotFound("Orden no encontrada");
            }


            return Ok(resultado);
        }

       

       
    }
}
