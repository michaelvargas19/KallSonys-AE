using Catalogos.Infraestructura.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalogos.Infraestructura.Specification
{
    public class ProductoSpecification : BaseSpecification<Producto>
    {
        public ProductoSpecification() : base()
        {
        }

        public ProductoSpecification(bool estado ) : base(c=> c.Estado == estado )
        {
        }
        public ProductoSpecification(int skip, int take) : base(c => c.Estado == true && c.EnAlmacen == true)
        {
            ApplyPaging(skip, take);
        }

        public ProductoSpecification(string codigoCatalogo) : base(c => c.CodigoCatalogo.ToUpper() == codigoCatalogo.ToUpper())
        {
        }

        public ProductoSpecification(string codigoCatalogo, bool estado) : base(c=> c.CodigoCatalogo.ToUpper() == codigoCatalogo.ToUpper() && c.Estado == estado && c.EnAlmacen == true)
        {
        }
    }
}
