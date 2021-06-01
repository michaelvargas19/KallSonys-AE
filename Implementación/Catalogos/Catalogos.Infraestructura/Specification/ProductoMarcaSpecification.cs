using Catalogos.Infraestructura.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalogos.Infraestructura.Specification
{
    public class ProductoMarcaSpecification : BaseSpecification<Producto>
    {

        public ProductoMarcaSpecification(string marca) : base(p=> p.Marca.ToUpper() == marca.ToUpper() )
        {
        }

        public ProductoMarcaSpecification(string marca, bool estado) : base(p => p.SKU.ToUpper() == marca.ToUpper() &&  p.Estado == estado)
        {
        }

    }
}
