using Ordenes.Dominio.IUnitOfWorks;
using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiOrdenes.Extensions
{
    public class EventKafka : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly ConsumerConfig _consumerConfig;

        protected IServiceProvider _serviceProvider;

        public EventKafka(ILogger<EventKafka> logger,
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


            using (var c = new ConsumerBuilder<Ignore, string>(_consumerConfig).Build())
            {
                c.Subscribe("TP_Venta");

                CancellationTokenSource cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) =>
                {
                    e.Cancel = true; // prevent the process from terminating.
                    cts.Cancel();
                };

                try
                {
                    while (true)
                    {
                        try
                        {
                            var cr = c.Consume(cts.Token);

                            Console.WriteLine($"Consumed message '{cr.Value}' at: '{cr.TopicPartitionOffset}'.");
                        }
                        catch (ConsumeException e)
                        {
                            Console.WriteLine($"Error occurred: {e.Error.Reason}");

                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    // Ensure the consumer leaves the group cleanly and final offsets are committed.
                    c.Close();
                }

            }
        }
    }
}
