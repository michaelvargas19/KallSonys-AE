using Ordenes.Dominio.IRepositories;
using Ordenes.Dominio.IServices;
using Ordenes.Dominio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ordenes.Infraestructura.Services
{
    public class Service_Command<T> : IService_Command<T> where T : IDocument
    {
        //private IRepository_Command<T> __repositoryCommand;

        private readonly IMongoContext _context;
        private readonly IMongoRepository_Command<T> _repository;
        public Service_Command(IMongoContext context,
                          IMongoRepository_Command<T> repository)
        {
            _context = context;
            _repository = repository;
        }
        public IMongoRepository_Command<T> Repository<T>() where T : IDocument
        {
            return (IMongoRepository_Command<T>)_repository;
        }
        public async Task DeleteByIdAsync(string id)
        {
            await _repository.DeleteByIdAsync(id);
        }

        public async Task DeleteManyAsync(Expression<Func<T, bool>> filterExpression)
        {
            await _repository.DeleteManyAsync(filterExpression);
        }

        public async Task DeleteOneAsync(Expression<Func<T, bool>> filterExpression)
        {
            await _repository.DeleteOneAsync(filterExpression);
        }

        public async Task InsertManyAsync(ICollection<T> documents)
        {
            await _repository.InsertManyAsync(documents);
        }

        public async Task InsertOneAsync(T document)
        {

            await _repository.InsertOneAsync(document);
        }

        public async Task ReplaceOneAsync(T document)
        {
            await _repository.ReplaceOneAsync(document);
        }

        




        //public async Task Delete(int id)
        //{
        //    await _repository.DeleteOne(id);
        //}

        //public async Task<T> Insert(T item)
        //{
        //    return await __repositoryCommand.Insert(item);
        //}

        //public async Task<T> Update(T item)
        //{
        //    return await __repositoryCommand.Update(item);
        //}
    }
}
