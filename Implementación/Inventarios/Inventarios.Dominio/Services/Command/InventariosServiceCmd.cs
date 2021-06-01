﻿using Newtonsoft.Json;
using Inventarios.Dominio.IServices.Command;
using Inventarios.Dominio.IServices.Queries;
using Inventarios.Dominio.IUnitOfWorks;
using Inventarios.Dominio.Modelo;
using Inventarios.Dominio.Modelo.Command;
using Inventarios.Dominio.Modelo.Queries;
using Inventarios.Dominio.Util;
using Inventarios.Infraestructura.Entities;
using Inventarios.Infraestructura.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace Inventarios.Dominio.Services.Command
{
    public class InventariosServiceCmd : IInventariosServiceCmd
    {
        private readonly IUnitOfWork<Producto> _ufwProductos;
        private readonly IUnitOfWork<_AuditoriaInventarios> _ufwLog;
        private readonly IInventariosServiceQuery _inventariosServiceQ;
        private readonly IProducer<Null, string> _producer;
        private readonly IUtils _utils;

        public InventariosServiceCmd(IInventariosServiceQuery inventariosServiceQ,
                                     IUnitOfWork<Producto> ufwProductos,
                                     IUnitOfWork<_AuditoriaInventarios> ufwLog,
                                     ProducerConfig producerConfig,
                                     IUtils utils)
        {
            this._inventariosServiceQ = inventariosServiceQ;
            this._ufwProductos = ufwProductos;
            this._producer = new ProducerBuilder<Null, string>(producerConfig).Build();
            this._ufwLog = ufwLog;
            this._utils = utils;
        }

        public void ProcesarVenta(EventBase<List<VentaCmd>> eventoVenta)
        {
            try
            {
                string[] skus = eventoVenta.Data.ConvertAll<string>(u => u.SKU).ToArray();

                IEnumerable<ExistenciaProducto> existencias = this._inventariosServiceQ.ConsultarDisponibilidad(skus);
                Dictionary<VentaCmd, Producto> productos = new Dictionary<VentaCmd, Producto>();

                if (existencias.Any(e => (!e.Disponible)))
                {
                    throw new Exception("Hay productos no disponibles");
                }


                foreach (VentaCmd v in eventoVenta.Data)
                {
                    ExistenciaProducto e = existencias.Where(e => e.sku == v.SKU).FirstOrDefault();

                    if ((e != null))
                    {
                        if ((e.Disponible) && (e.CantidadDisponibles >= v.Unidades))
                        {
                            Producto p = this._ufwProductos.Repository<Producto>().Find(new ProductoSKUSpecification(e.sku)).FirstOrDefault();
                            p.NivelInventario = p.NivelInventario - v.Unidades;
                            p.EnAlmacen = (p.NivelInventario > 0) ? true : false;
                            productos.Add(v, p);
                        }
                        else
                        {
                            throw new Exception("No hay unidades disponibles del producto SKU: " + v.SKU);
                        }

                    }
                    else
                    {
                        throw new Exception("Error en la consulta de disponibilidad");
                    }
                }



                foreach (KeyValuePair<VentaCmd, Producto> pair in productos)
                {
                    //Seguimiento
                    Producto p = this.HacerSeguimiento(pair.Value);


                    //Auditoría
                    var jrq = JsonConvert.SerializeObject(eventoVenta);
                    var jrp = JsonConvert.SerializeObject(p);
                    _AuditoriaInventarios log = new _AuditoriaInventarios("Venta", pair.Key.Referencia, false, eventoVenta.Usuario, MethodInfo.GetCurrentMethod().Name, this.ToString(), jrq, jrp, "", "Referencia: " + pair.Key.Referencia + "   SKU: " + pair.Key.SKU + "   Unidades: " + pair.Key.Unidades + "");
                    
                    
                    //Persistencia
                    _ufwProductos.Repository<Producto>().ReplaceOne(p);
                    _ufwLog.Repository<_AuditoriaInventarios>().InsertOne(log);
                    

                }

                

            }
            catch (Exception e)
            {
                throw e;
            }


        }



        private Producto HacerSeguimiento(Producto producto)
        {
            try
            {

                switch (producto.TipoSeguimiento)
                {
                    case TIPO_SEGUIMIENTO.NINGUNO:

                        break;

                    case TIPO_SEGUIMIENTO.PRODUCTO:
                        if (producto.NivelInventario <= 0)
                        {
                            this.NotificarCambioEstadoProducto(producto);
                            //Notificar a Proveedor
                        }
                        break;

                    case TIPO_SEGUIMIENTO.VARIABLE:

                        if (producto.NivelInventario <= producto.NivelAdvertencia)
                        {
                            producto.TipoSeguimiento = TIPO_SEGUIMIENTO.PRODUCTO;

                            if (producto.NivelInventario <= 0)
                            {
                                this.NotificarCambioEstadoProducto(producto);
                            }
                            else
                            {
                                //Notificar a Proveedor
                            }
                        }
                        break;

                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return producto;

        }

        private void NotificarCambioEstadoProducto(Producto p)
        {
            try
            {
                EventBase<EstadoProductoCmd> evento = new EventBase<EstadoProductoCmd>();
                evento.Evento = "EstadoActualizado";
                evento.Fecha = DateTime.Now;
                evento.Origen = "api-inventarios";
                evento.Topico = "TP_Inventario";
                evento.Data = new EstadoProductoCmd()
                {
                    SKU = p.SKU,
                    NivelInventario = p.NivelInventario,
                    Estado = p.Estado,
                    EnAlmacen = p.EnAlmacen
                };

                var json = JsonConvert.SerializeObject(evento);

                this._producer.Produce("TP_Inventario", new Message<Null, string> { Value = json });

            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
