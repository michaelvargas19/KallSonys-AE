using Inventarios.Infraestructura.Entities;

namespace Inventarios.Infraestructura.Specification
{
    public class ProductoCodigoSpecification : BaseSpecification<Producto>
    {

        public ProductoCodigoSpecification(string codigo) : base(p=> p.CodigoProducto.ToUpper() == codigo.ToUpper() )
        {
        }

        public ProductoCodigoSpecification(string codigo, bool estado) : base(p => p.CodigoProducto.ToUpper() == codigo.ToUpper() &&  p.Estado == estado)
        {
        }

    }
}
