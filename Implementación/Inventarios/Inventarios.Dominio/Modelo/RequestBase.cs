using System;
using System.Collections.Generic;
using System.Text;

namespace Inventarios.Dominio.Modelo
{
    public class RequestBase<T>
    {
        public string usuario { set; get; }
        public T data { set; get; }

    }
}
