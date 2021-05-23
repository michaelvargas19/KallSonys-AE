using System;

namespace Autenticacion.Dominio.Modelo
{
    public class RequestBase<T>
    {
        public string usuario { set; get; }
        public T data { set; get; }

    }
}
