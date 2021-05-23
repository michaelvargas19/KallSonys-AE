using System;

namespace Autenticacion.Dominio.Modelo
{
    public class ResponseBase<T>
    {
        public DateTime date { set; get; }
        public int code { set; get; }
        public string message { set; get; }
        public T data { set; get; }

    }
}
