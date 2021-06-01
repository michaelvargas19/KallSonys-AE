using Inventarios.Infraestructura.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventarios.Infraestructura.Specification
{
    public class ProductoSpecification : BaseSpecification<Producto>
    {
        public ProductoSpecification() : base()
        {
        }

        public ProductoSpecification(bool estado ) : base(c=> c.Estado == estado )
        {
        }

        public ProductoSpecification(string codigoCatalogo) : base(c => c.CodigoCatalogo.ToUpper() == codigoCatalogo.ToUpper())
        {
        }

        public ProductoSpecification(string codigoCatalogo, bool estado) : base(c=> c.CodigoCatalogo.ToUpper() == codigoCatalogo.ToUpper() && c.Estado == estado && c.EnAlmacen == true)
        {
        }
    }
}
