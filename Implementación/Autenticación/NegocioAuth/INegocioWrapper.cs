
using NegocioAuth.Interfaces;

namespace NegocioAuth
{
    public interface INegocioWrapper
    {
        ILogNegocio Log { get; }
        ISesionesNegocio Sesiones { get; }
        IAplicacionesNegocio Aplicaciones { get; }
    }
}
