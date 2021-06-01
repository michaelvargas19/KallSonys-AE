using Inventarios.Infraestructura.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inventarios.Infraestructura.Specification
{
    public class ProductoSKUSpecification : BaseSpecification<Producto>
    {

        public ProductoSKUSpecification(string sku) : base(p=> p.SKU.ToUpper() == sku.ToUpper() )
        {
        }

        public ProductoSKUSpecification(string [] sku) : base(p => sku.Contains(p.SKU) && p.Estado == true)
        {
        }

        public ProductoSKUSpecification(string sku, bool estado) : base(p => p.SKU.ToUpper() == sku.ToUpper() &&  p.Estado == estado)
        {
        }

    }
}
