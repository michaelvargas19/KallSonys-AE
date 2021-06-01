using Catalogos.Infraestructura.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Catalogos.Infraestructura.Specification
{
    public class ProductoFullTextSpecification : BaseSpecification<Producto>
    {

        public ProductoFullTextSpecification(string text) : base(p => p.Nombre.ToUpper().Contains(text.ToUpper()) ||
                                                                      p.Descripcion.ToUpper().Contains(text.ToUpper()) ||
                                                                      p.Marca.ToUpper().Contains(text.ToUpper()) ||
                                                                      p.Sinonimos.Contains(text.ToUpper())
                                                                )
        {
        }

        public ProductoFullTextSpecification(string text, bool estado) : base(p => (
                                                                      p.Nombre.ToUpper().Contains(text.ToUpper()) ||
                                                                      p.Descripcion.ToUpper().Contains(text.ToUpper()) ||
                                                                      p.Marca.ToUpper().Contains(text.ToUpper()) ||
                                                                      p.Sinonimos.Contains(text.ToUpper())  ) 
                                                                      && p.Estado == estado
                                                                )
        {
        }


    }
}
