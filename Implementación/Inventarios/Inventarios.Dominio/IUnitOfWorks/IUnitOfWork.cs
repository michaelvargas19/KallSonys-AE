using Inventarios.Infraestructura.Entities;
using Inventarios.Infraestructura.Repository;
using System;

namespace Inventarios.Dominio.IUnitOfWorks
{
    public interface IUnitOfWork<T> : IDisposable
    {
        IMongoRepository<T> Repository<T>() where T : IDocument;
        bool Commit();
    }


}
