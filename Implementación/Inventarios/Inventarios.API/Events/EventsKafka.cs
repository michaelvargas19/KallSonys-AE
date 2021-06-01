using Confluent.Kafka;
using Inventarios.Dominio.IServices.Command;
using Inventarios.Dominio.Modelo;
using Inventarios.Dominio.Modelo.Command;
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
                throw e;
            }
        }

        private void Start(CancellationToken stoppingToken)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) => 
                    { e.Cancel = true; 
                      cts.Cancel(); 
                    };

            using ( var c = new ConsumerBuilder<Ignore, string>(this._consumerConfig).Build() )
            {
                c.Subscribe("TP_Venta");

                while (!stoppingToken.IsCancellationRequested)
                {
                    
                    try
                    {
                        using (var scope = this._serviceProvider.CreateScope())
                        {
                            _inventariosServiceCmd = (IInventariosServiceCmd)scope.ServiceProvider.GetRequiredService(typeof(IInventariosServiceCmd));

                            var cr = c.Consume(cts.Token);
                            _logger.LogInformation($"\n\n--------------------------------------------------{DateTime.Now}\n");
                            _logger.LogInformation($"MENSAJE CONSUMIDO:  '{cr.Value}'  \nTOPIC:  '{cr.TopicPartitionOffset}' \n");

                            EventBase<List<VentaCmd>> evento = JsonConvert.DeserializeObject<EventBase<List<VentaCmd>>>(cr.Value);

                            switch (evento.Evento)
                            {
                                case "VentaProductos":

                                    this._inventariosServiceCmd.ProcesarVenta(evento);

                                    break;

                                default:
                                    throw new Exception("Evento no identificado");
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
    }
}
