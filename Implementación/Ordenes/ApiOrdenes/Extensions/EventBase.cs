using Confluent.Kafka;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiOrdenes.Extensions
{
    public class EventBase<T>
    {
        public string Evento { set; get; }

        public string Topico { set; get; }

        public string Origen { set; get; }

        public string Usuario { set; get; }

        public DateTime Fecha { set; get; }

        public List<T> Data { set; get; }

        public void Producir(EventBase<T> evento, IProducer<string, string> _producer)
        {
            try
            {
                evento.Fecha = System.DateTime.Now;
                evento.Topico = "T_VENTAS";
                evento.Origen = "API Ventas";
                evento.Evento = evento.Evento;
                evento.Data = evento.Data;
                var json = JsonConvert.SerializeObject(evento);
                _producer.Produce("TP_Venta", new Message<string, string> {Key=evento.Evento, Value = json });
            }
            catch (Exception error)
            {

            }
        }
    }
}
