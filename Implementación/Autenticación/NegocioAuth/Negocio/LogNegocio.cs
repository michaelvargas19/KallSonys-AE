using DominioAuth.Modelo;
using NegocioAuth.Interfaces;
using PersistenciaAuth;

namespace NegocioAuth.Negocio
{
    public class LogNegocio : ILogNegocio
    {
        private readonly IRepositoryWrapper repository;
        public LogNegocio(IRepositoryWrapper repositoryWrapper)
        {
            this.repository = repositoryWrapper;
        }
        
        public _LogAutenticacionAPI Crear(_LogAutenticacionAPI entity)
        {
            return repository.Log.Crear(entity);
        }

        public _LogAutenticacionAPI Borrar(_LogAutenticacionAPI entity)
        {
            return repository.Log.Borrar(entity);
        }

    }
}
