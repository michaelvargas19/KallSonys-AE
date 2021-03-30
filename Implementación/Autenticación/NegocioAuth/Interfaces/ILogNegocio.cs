using DominioAuth.Modelo;

namespace NegocioAuth.Interfaces
{
    public interface ILogNegocio 
    {
        _LogAutenticacionAPI Crear(_LogAutenticacionAPI entity);
        _LogAutenticacionAPI Borrar(_LogAutenticacionAPI entity);
    }
}
