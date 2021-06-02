using Confluent.Kafka;
using Inventarios.Dominio.IServices.Command;
using Inventarios.Dominio.IUnitOfWorks;
using Inventarios.Dominio.Modelo;
using Inventarios.Dominio.Modelo.Command;
using Inventarios.Infraestructura.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Inventarios.API.Events
{
    public class EventsKafka : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly ConsumerConfig _consumerConfig;
        
        protected IServiceProvider _serviceProvider;
        protected IInventariosServiceCmd _inventariosServiceCmd;
        protected IUnitOfWork<_AuditoriaInventarios> _log;

        public EventsKafka(ILogger<EventsKafka> logger,
                           ConsumerConfig consumerConfig,
                           IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
            this._consumerConfig = consumerConfig;
            this._logger = logger;
            
        }


        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
            try
            {
                Task.Run(() => Start(stoppingToken));
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                _logger.LogError($"MENSAJE:  '{e.Message}'  STACKTRACE:  '{e.StackTrace}' \n\n\n");
                throw e;
            }
        }

        private void Start(CancellationToken stoppingToken)
        {
            try
            {
                CancellationTokenSource cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) => 
                        { e.Cancel = true; 
                          cts.Cancel(); 
                        };

                using ( var c = new ConsumerBuilder<string, string>(this._consumerConfig).Build() )
                {
                    c.Subscribe(new List<string>() { "TP_Venta", "TP_Inventario" });

                    while (!stoppingToken.IsCancellationRequested)
                    {

                        _AuditoriaInventarios log = null;

                        using (var scope = this._serviceProvider.CreateScope())
                        {
                            _inventariosServiceCmd = (IInventariosServiceCmd)scope.ServiceProvider.GetRequiredService(typeof(IInventariosServiceCmd));
                            _log = (IUnitOfWork<_AuditoriaInventarios>)scope.ServiceProvider.GetRequiredService(typeof(IUnitOfWork<_AuditoriaInventarios>));
                            
                            try
                            {
                                var cr = c.Consume(cts.Token);

                                _logger.LogInformation($"\n\n");
                                _logger.LogInformation($"--------------------------------------------------{DateTime.Now}\n");
                                _logger.LogInformation($"TOPIC:  '{cr.Topic}'  EVENTO: '{cr.Key}'   MENSAJE CONSUMIDO:  '{cr.Value}'  \n");

                                //log = new _AuditoriaInventarios("TOPIC", "", false, "", "sad", this.ToString(), $"TOPIC:  '{cr.Topic}'  EVENTO: '{cr.Key}'   MENSAJE CONSUMIDO:  '{cr.Value}' ", "", "", "");
                                //_log.Repository<_AuditoriaInventarios>().InsertOne(log);


                                switch (cr.Topic)
                                {
                                    case "TP_Venta":
                                        
                                        
                                        switch (cr.Key)
                                        {
                                            case "er la integración con":

                                                EventBase<List<VentaCmd>> eventoVenta = JsonConvert.DeserializeObject<EventBase<List<VentaCmd>>>(cr.Value);
                                                this._inventariosServiceCmd.ProcesarVenta(eventoVenta);

                                                break;

                                            default:
                                                throw new Exception("Evento no identificado");
                                        }

                                        break;

                                    case "TP_Inventario":
                                        
                                        
                                        switch (cr.Key)
                                        {
                                            case "EstadoActualizado":
                                                
                                                EventBase<EstadoProductoCmd> eventoEstado = JsonConvert.DeserializeObject<EventBase<EstadoProductoCmd>>(cr.Value);
                                                this._inventariosServiceCmd.ProcesarEstadoProducto(eventoEstado);
                                                
                                                break;

                                            default:
                                                throw new Exception("Evento no identificado");
                                        }

                                        break;

                                }
                                                                
                            }

                            catch (Exception e)
                            {
                                log = new _AuditoriaInventarios("TOPIC PROCESO EXCEPTION", "", true, "", "", this.ToString(), e.Message, e.StackTrace, "", "");
                                _log.Repository<_AuditoriaInventarios>().InsertOne(log);
                            }




                        }
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"MENSAJE:  '{e.Message}'  STACKTRACE:  '{e.StackTrace}' \n\n\n");
            }
        }
    }
}
