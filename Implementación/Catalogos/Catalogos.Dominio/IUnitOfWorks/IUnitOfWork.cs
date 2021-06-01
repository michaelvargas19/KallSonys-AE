using Catalogos.Infraestructura.Entities;
using Catalogos.Infraestructura.Repository;
using System;

namespace Catalogos.Dominio.IUnitOfWorks
{
    public interface IUnitOfWork<T>: IDisposable 
    {
        IMongoRepository<T> Repository<T>() where T : IDocument;
        bool Commit();
    }


}
