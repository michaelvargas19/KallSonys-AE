using Inventarios.Infraestructura.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventarios.Infraestructura.Specification
{
    public class CatalogoSpecification : BaseSpecification<Catalogo>
    {
        public CatalogoSpecification() : base()
        {
        }

        public CatalogoSpecification(bool estado ) : base(c=> c.Estado == estado )
        {
        }

        public CatalogoSpecification(string codigo) : base(c => c.CodigoCatalogo.ToUpper() == codigo.ToUpper())
        {
        }

        public CatalogoSpecification(string codigo, bool estado) : base(c=> c.CodigoCatalogo.ToUpper() == codigo.ToUpper() && c.Estado == estado)
        {
        }
    }
}
