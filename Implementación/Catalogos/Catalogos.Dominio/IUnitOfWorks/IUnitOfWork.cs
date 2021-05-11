using Catalogos.Dominio.ISpecification;
using Catalogos.Dominio.Modelo;
using Catalogos.Dominio.Repository;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogos.Dominio.IUnitOfWorks
{
    public interface IUnitOfWork<T>: IDisposable 
    {
        IMongoRepository<T> Repository<T>() where T : IDocument;
        Task<bool> Commit();
    }


}
