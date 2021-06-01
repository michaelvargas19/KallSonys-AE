using Catalogos.Infraestructura.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalogos.Infraestructura.Specification
{
    public class ProductoSKUSpecification : BaseSpecification<Producto>
    {

        public ProductoSKUSpecification(string sku) : base(p=> p.SKU.ToUpper() == sku.ToUpper() )
        {
        }

        public ProductoSKUSpecification(string sku, bool estado) : base(p => p.SKU.ToUpper() == sku.ToUpper() &&  p.Estado == estado)
        {
        }

    }
}
