using System;
using System.Collections.Generic;
using System.Text;

namespace Inventarios.Dominio.Modelo
{
    public class EventBase<T>
    {
        public string Evento { set; get; }
        
        public string Topico { set; get; }

        public string Origen { set; get; }

        public string Usuario { set; get; }

        public DateTime Fecha { set; get; }

        public T Data { set; get; }

    }
}
