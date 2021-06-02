
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ordenes.Infraestructura.Repositories;
using Ordenes.Dominio.IUnitOfWorks;
using Ordenes.Dominio.IRepositories;
using Ordenes.Dominio.IServices;
using Ordenes.Infraestructura.Services;
using Ordenes.Dominio.Modelo;

namespace Ordenes.Infraestructura.UnitOfWork
{
    public class UnitOfWorkI<T> : IUnitOfWork<T> where T : IDocument
    {
        
        private readonly IMongoContext _context;
        private readonly IService_Command<T> _repo_command;
        private readonly IService_Query<T> _repo_query;


        public UnitOfWorkI(IMongoContext context,
                        IService_Command<T> repository_command,
                        IService_Query<T> repository_query)
        {
            _context = context;
            _repo_command = repository_command;
            _repo_query = repository_query;
          
        }
     
        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        //public IGenericRepository<T> Repository<T>() where T : class
        //{
        //    return (GenericRepository<T>)_repository;
        //}



        //public IService_Command<T> Service_Commands<T>() where T : class
        //{
        //    return (Service_Command<T>)_repo_command;
        //}

        //public IService_Query<T> Service_Queries<T>() where T : class
        //{
        //    return (Service_Query<T>)_repo_query;
        //}

        //public IRepository_Command<T> Service_Commands<T>() where T : class
        //{
        //    return (Repository_Command<T>)_repo_command;
        //}

        //public IRepository_Query<T> Service_Queries<T>() where T : class
        //{
        //    return (Repository_Query<T>)_repo_query;
        //}

        public IService_Command<T> Service_Commands<T>() where T : IDocument
        {
            return (Service_Command<T>)_repo_command;
        }

        public IService_Query<T> Service_Queries<T>() where T : IDocument
        {
            return (Service_Query<T>)_repo_query;
        }
    }
}
