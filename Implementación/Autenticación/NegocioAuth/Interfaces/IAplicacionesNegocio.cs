using DominioAuth.Modelo;

namespace NegocioAuth.Interfaces
{
    public interface IAplicacionesNegocio : INegocioBase<Aplicacion>
    {
        Configuracion verConfiguracion(string IdAplicacion);
    }
}
