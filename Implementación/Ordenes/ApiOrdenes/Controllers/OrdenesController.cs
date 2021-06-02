using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ordenes.Infraestructura.Repositories;
using Ordenes.Infraestructura.UnitOfWork;
using Ordenes.Dominio.Models;
using Ordenes.Infraestructura.Specification;
using Ordenes.Dominio.IServices;
using Ordenes.Infraestructura.Services;
using Ordenes.Dominio.IRepositories;
using Ordenes.Dominio.IUnitOfWorks;
using Confluent.Kafka;
using ApiOrdenes.Extensions;

namespace ApiOrdenes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdenesController : ControllerBase
    {

        private readonly IUnitOfWork<Orden> _unitOfWork;
        private readonly IUnitOfWork<Pedido> _unitOfWork_pedido;
        private readonly IUnitOfWork<Orden_Ref_Estados> _unitOfWork_estado;
        private readonly IUnitOfWork<Orden_Item_Ref_Estados> _unitOfWork_estado_item;

        private readonly IProducer<Null, string> _producer;
        EventBase<Orden> _evento = new EventBase<Orden>();




        public OrdenesController(IUnitOfWork<Orden> unitOfWork, IUnitOfWork<Pedido> unitOfWork_pedido, IUnitOfWork<Orden_Ref_Estados> unitOfWork_estado,
            IUnitOfWork<Orden_Item_Ref_Estados> unitOfWork_estado_item
             , ProducerConfig producerConfig)
        {
            _unitOfWork = unitOfWork;
            _unitOfWork_pedido = unitOfWork_pedido;
            _unitOfWork_estado = unitOfWork_estado;
            _unitOfWork_estado_item = unitOfWork_estado_item;
            _producer = new ProducerBuilder<Null, string>(producerConfig).Build();
        }

       

        //[HttpPost("")]
        

        //Cambiar estado producto por orden
        [Route("[action]")]
        [HttpPut]
        public async Task<ActionResult<Orden>> ActualizarEstadoProductoPorOrden(objActalizarEstado objEstado)
        {
            Orden orden = new Orden();
            bool actualiza = false;
            try
            {
                orden = _unitOfWork.Service_Queries<Orden>().FindById(objEstado.id);
               
                var existeEstado = _unitOfWork_estado_item.Service_Queries<Orden_Item_Ref_Estados>().FindOne(x => x.descripcion_estado_item == objEstado.nuevoEstado);
                if (existeEstado != null)
                {
                    if (orden != null)
                    {
                        foreach (var item in orden.items)
                        {
                            if(item.id_producto == objEstado.idProducto)
                            {
                                actualiza = true;
                                item.estado_item = objEstado.nuevoEstado;
                                orden.fecha_actualizacion = System.DateTime.Now;
                                await _unitOfWork.Service_Commands<Orden>().ReplaceOneAsync(orden);

                                
                                return orden;
                            }
                          
                        }
                        if (!actualiza)
                        {
                            return NotFound("Producto no encontrado");
                        }

                    }
                    else
                    {
                        return NotFound("Orden no encontrada");
                    }
                }
                else
                {
                    return NotFound("Estado no existe");
                }

            }
            catch (Exception error)
            {
                throw new Exception("Error al momento de ingresar el registro: " + error.Message + "," + error.InnerException);

            }
            return Ok(orden);
        }

        //Cambiar estado orden
        [Route("[action]")]
        [HttpPut]
        public async Task<ActionResult<Orden>> ActualizarEstadoOrden(objActalizarEstado objEstado)
        {
            Orden orden = new Orden();
            try
            {
                orden = _unitOfWork.Service_Queries<Orden>().FindById(objEstado.id);
                if(orden == null)
                {
                    return NotFound("Orden no existe");
                }
                var existeEstado = _unitOfWork_estado.Service_Queries<Orden_Ref_Estados>().FindOne(x => x.descripcion_estado_orden == objEstado.nuevoEstado);
                if (existeEstado != null)
                {
                    if (orden != null)
                    {
                        orden.estado_orden = objEstado.nuevoEstado;
                        orden.fecha_actualizacion = System.DateTime.Now;
                        await _unitOfWork.Service_Commands<Orden>().ReplaceOneAsync(orden);
                      
                    }
                }
                else
                {
                    return NotFound("Estado no existe");
                }
                
            }
            catch (Exception error)
            {
                throw new Exception("Error al momento de ingresar el registro: " + error.Message + "," + error.InnerException);

            }
            return Ok(orden);
        }

        //Buscar Orden
        [HttpGet]
        public IQueryable<Orden> ObtenerOrdenes()
        {
            return _unitOfWork.Service_Queries<Orden>().AsQueryable();
        }


        //Buscar orden por pedido
        [Route("[action]/{numPedido}")]
        [HttpGet]
        public  ActionResult<List<Orden>> ObtenerOrdenPorPedido(string numPedido)
        {
            Orden orden = new Orden();
            List<Orden> resultado = new List<Orden>();

            var pedido =  _unitOfWork_pedido.Service_Queries<Pedido>().FindById(numPedido);
            if (pedido != null)
            {
                foreach(var itemOrden in pedido.id_orden)
                {
                    //
                    orden =  _unitOfWork.Service_Queries<Orden>().FindById(itemOrden);
                    if (orden != null)
                    {
                        if (resultado.Count() != 0)
                        {
                            for (int i = 0; i < resultado.Count(); i++)
                            {
                                if (orden.Id != resultado[i].Id)
                                {
                                    resultado.Add(orden);
                                }
                            }
                        }
                        else
                        {
                            resultado.Add(orden);
                        }
                    }
                    else
                    {
                        return NotFound("Orden no encontrada");
                    }
                    //
                }
            }
            else
            {
                return NotFound("Pedido no encontrado");
            }
            return resultado;
        }

        //Buscar Orden por proveedor
        [Route("[action]/{idProveedor}")]
        [HttpGet]
        public ActionResult<List<Orden>> ObtenerOrdenPorProveedor(string idProveedor)
        {
            var orden = _unitOfWork.Service_Queries<Orden>().AsQueryable().Where(x => x.id_proveedor == idProveedor).ToList();
            if(orden != null)
            {
                return Ok(orden);
            }
            else
            {
                return NotFound("Orden no encontrada");
            }
            
        }

        //Buscar orden por cliente
        [Route("[action]/{porCliente}")]
        [HttpGet]
        public ActionResult<List<Orden>> ObtenerOrdenPorCliente(string idCliente)
        {
            var orden = _unitOfWork.Service_Queries<Orden>().AsQueryable().Where(x => x.id_cliente == idCliente).ToList();
            if (orden != null)
            {
                return Ok(orden);
            }
            else
            {
                return NotFound("Orden no encontrada");
            }
        }
    }
}
