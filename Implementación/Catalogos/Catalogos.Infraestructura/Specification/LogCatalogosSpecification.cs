using Catalogos.Infraestructura.Entities;
using Catalogos.Infraestructura.Specification;

namespace Inventarios.Infraestructura.Specification
{
    public class LogCatalogosSpecification : BaseSpecification<_AuditoriaCatalogos>
    {

        public LogCatalogosSpecification(string referencia, string request) : base(l=> l.Referencia.ToUpper() == referencia.ToUpper() && l.Request.ToUpper() == request.ToUpper() )
        {
        }

    }
}
