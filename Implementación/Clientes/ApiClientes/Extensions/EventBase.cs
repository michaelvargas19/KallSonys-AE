using Confluent.Kafka;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiClientes.Extensions
{
    public class EventBase<T>
    {
        public string Evento { set; get; }

        public string Topico { set; get; }

        public string Origen { set; get; }

        public string Usuario { set; get; }

        public DateTime Fecha { set; get; }

        public T Data { set; get; }
        public void Producir(EventBase<T> evento, IProducer<Null, string> _producer)
        {
            try
            {
                evento.Fecha = System.DateTime.Now;
                evento.Topico = "T_CLIENTE";
                evento.Origen = "API Clientes";
                evento.Evento = evento.Evento;
                evento.Data = evento.Data;
                var json = JsonConvert.SerializeObject(evento);
                _producer.Produce("TP_Cliente", new Message<Null, string> { Value = json });
            }
            catch(Exception error)
            {

            }
        }
    }
}
