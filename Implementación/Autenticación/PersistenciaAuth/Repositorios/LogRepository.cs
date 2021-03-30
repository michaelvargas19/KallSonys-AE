using DominioAuth.Modelo;
using PersistenciaAuth.Interfaces;

namespace PersistenciaAuth.Repositorios
{
    public class LogRepository: RepositoryBase<_LogAutenticacionAPI>, ILogRepository
    {
        public LogRepository(ContextoAuthDB ContextDB)
            : base(ContextDB)
    {
    }
}
}
