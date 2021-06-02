using Inventarios.Infraestructura.Entities;

namespace Inventarios.Infraestructura.Specification
{
    public class LogInventarioSpecification : BaseSpecification<_AuditoriaInventarios>
    {

        public LogInventarioSpecification(string referencia, string request) : base(l=> l.Referencia.ToUpper() == referencia.ToUpper() && l.Request.ToUpper() == request.ToUpper() )
        {
        }

    }
}
