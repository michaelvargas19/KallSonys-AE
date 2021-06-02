using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiClientes.Extensions;
using Clientes.Dominio.IUnitOfWorks;
using Clientes.Dominio.Models;
using Confluent.Kafka;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiClientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        
        private readonly IUnitOfWork<Cliente> _unitOfWork_cliente; 
       private readonly IUnitOfWork<Ref_Metodos_Pago> _unitOfWork_metodoPago;

        private readonly IProducer<Null, string> _producer;
        EventBase<Cliente> _evento = new EventBase<Cliente>();



        public ClienteController(IUnitOfWork<Cliente> unitOfWork, IUnitOfWork<Ref_Metodos_Pago> unitOfWork_metodoPago,
            ProducerConfig producerConfig)
        {
            _unitOfWork_cliente = unitOfWork;
            _unitOfWork_metodoPago = unitOfWork_metodoPago;
            _producer = new ProducerBuilder<Null, string>(producerConfig).Build();
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> AgregarCliente(Cliente cliente)
        {


            try
            {
                if (ModelState.IsValid)
                {
                    var existeCliente = _unitOfWork_cliente.Service_Queries<Cliente>().FindOne(x => x.correo_cliente == cliente.correo_cliente);
                    if (existeCliente == null)
                    {

                        await _unitOfWork_cliente.Service_Commands<Cliente>().InsertOneAsync(cliente);

                        _evento.Data = cliente;
                        _evento.Evento = "ClienteAgregado";
                        _evento.Producir(_evento,_producer);
                       
                    }
                    else
                    {
                        return NotFound("Este correo ya esta registrado ");
                    }
                }
            }
            catch (Exception error) {
                throw new Exception(error.InnerException + error.Message);
            }

            return cliente;
        }


        [HttpGet]
        public IQueryable<Cliente> ObtenerClientes()
        {
            return _unitOfWork_cliente.Service_Queries<Cliente>().AsQueryable();
        }


        //Buscar pago por pedido
        [Route("[action]/{correo}")]
        [HttpGet]
        public Cliente ObtenerClientePorCorreo(string correo)
        {
            var cliente = _unitOfWork_cliente.Service_Queries<Cliente>().FindOne(x => x.correo_cliente == correo);
           
            return cliente;
        }

        //Buscar pago por pedido
        [Route("[action]/{nombreUsuario}")]
        [HttpGet]
        public Cliente ObtenerClientePorNombreUsuario(string nombreUsuario)
        {
            return _unitOfWork_cliente.Service_Queries<Cliente>().FindOne(x => x.nombre_usuario == nombreUsuario);
        }

        [HttpPut]
        public async Task<ActionResult<Cliente>> ActualizarDatosCliente(Cliente cliente)
        {


            if (ModelState.IsValid)
            {
                var existeCliente = _unitOfWork_cliente.Service_Queries<Cliente>().FindOne(x => x.correo_cliente == cliente.correo_cliente);
                if (existeCliente != null)
                {
                    cliente.Id = existeCliente.Id;

                    cliente.correo_cliente = string.IsNullOrEmpty(cliente.correo_cliente) ? existeCliente.correo_cliente : cliente.correo_cliente;
                    cliente.nombres_cliente = string.IsNullOrEmpty(cliente.nombres_cliente) ? existeCliente.nombres_cliente : cliente.nombres_cliente;
                    cliente.nombre_usuario = string.IsNullOrEmpty(cliente.nombre_usuario) ? existeCliente.nombre_usuario : cliente.nombre_usuario;
                    cliente.password_cliente = string.IsNullOrEmpty(cliente.password_cliente) ? existeCliente.password_cliente : cliente.password_cliente;
                    cliente.direccion_cliente = string.IsNullOrEmpty(cliente.direccion_cliente) ? existeCliente.direccion_cliente : cliente.direccion_cliente;
                    cliente.telefono_cliente = string.IsNullOrEmpty(cliente.telefono_cliente) ? existeCliente.telefono_cliente : cliente.telefono_cliente;
                    cliente.ciudad_cliente = string.IsNullOrEmpty(cliente.ciudad_cliente) ? existeCliente.ciudad_cliente : cliente.ciudad_cliente;
                    cliente.pais_cliente = string.IsNullOrEmpty(cliente.pais_cliente) ? existeCliente.pais_cliente : cliente.pais_cliente;
                    if (cliente.metodo_pago == null)
                    {
                        cliente.metodo_pago = existeCliente.metodo_pago;
                    }
                    
                    

                    await _unitOfWork_cliente.Service_Commands<Cliente>().ReplaceOneAsync(cliente);

                    _evento.Data = cliente;
                    _evento.Evento = "DatosActualizadosCliente";
                    _evento.Producir(_evento, _producer);
                }
                else
                {
                    return NotFound("Este correo no esta registrado ");
                }
            }

            return cliente;
        }
        [Route("[action]")]
        [HttpPut]
        public async Task<ActionResult<Cliente>> ActualizarMetodoPagoCliente(Cliente cliente_metodo)
        {
            Cliente existeCliente = new Cliente();

            if (ModelState.IsValid)
            {
                existeCliente = _unitOfWork_cliente.Service_Queries<Cliente>().FindOne(x => x.correo_cliente == cliente_metodo.correo_cliente);
                var metodoPagoValido = _unitOfWork_metodoPago.Service_Queries<Ref_Metodos_Pago>().FindOne(x => x.codigo_metodo_pago == cliente_metodo.metodo_pago.codigo_metodo_pago);
                if (existeCliente != null && metodoPagoValido != null)
                {
                    
                    if (cliente_metodo.metodo_pago == null)
                    {
                        cliente_metodo.metodo_pago = existeCliente.metodo_pago;
                    }
                    else
                    {
                        existeCliente.metodo_pago = cliente_metodo.metodo_pago;
                        cliente_metodo = existeCliente;
                    }
                   
                    await _unitOfWork_cliente.Service_Commands<Cliente>().ReplaceOneAsync(cliente_metodo);

                    _evento.Data = cliente_metodo;
                    _evento.Evento = "MetodoPagoActualizadoCliente";
                    _evento.Producir(_evento, _producer);
                }
                else
                {
                    return NotFound("Datos incorrectos");
                }
            }

            return cliente_metodo;
        }
    }
}
