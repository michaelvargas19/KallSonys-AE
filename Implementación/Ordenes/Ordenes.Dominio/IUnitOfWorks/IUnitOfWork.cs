
using Ordenes.Dominio.IRepositories;
using Ordenes.Dominio.IServices;
using Ordenes.Dominio.Modelo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ordenes.Dominio.IUnitOfWorks
{
    public interface IUnitOfWork<T> :IDisposable
    {
        //IGenericRepository<T> Repository<T>() where T : class;
        //IGenericService<T> Service<T>() where T : class;
        //IMongoRepository<T> Repository<T>() where T : IDocument;
        //Task<bool> Commit();
        IService_Command<T> Service_Commands<T>() where T : IDocument;
        IService_Query<T> Service_Queries<T>() where T : IDocument;
    }
}
