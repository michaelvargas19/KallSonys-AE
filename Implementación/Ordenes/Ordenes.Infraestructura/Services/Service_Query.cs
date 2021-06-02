using Ordenes.Dominio.IRepositories;
using Ordenes.Dominio.IServices;
using Ordenes.Dominio.ISpecification;

using Ordenes.Dominio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ordenes.Infraestructura.Services
{
    public class Service_Query<T> : IService_Query<T> where T : IDocument
    {
        private IMongoRepository_Query<T> _repositoryQuery;

        public Service_Query(IMongoRepository_Query<T> genericRepositorio)
        {
            _repositoryQuery = genericRepositorio;
        }

        public IQueryable<T> AsQueryable()
        {
            return _repositoryQuery.AsQueryable();
        }

        public bool Contains(ISpecification<T> specification = null)
        {
            throw new NotImplementedException();
        }

        public int Count(ISpecification<T> specification = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> FilterBy(Expression<Func<T, bool>> filterExpression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TProjected> FilterBy<TProjected>(Expression<Func<T, bool>> filterExpression, Expression<Func<T, TProjected>> projectionExpression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Find(ISpecification<T> specification = null)
        {
           return _repositoryQuery.Find(specification);
        }

        public T FindById(string id)
        {
            return _repositoryQuery.FindById(id);
        }

        public Task<T> FindByIdAsync(string id)
        {
            return _repositoryQuery.FindByIdAsync(id);
        }

        public T FindOne(Expression<Func<T, bool>> filterExpression)
        {
            return _repositoryQuery.FindOne(filterExpression);
        }

        public Task<T> FindOneAsync(Expression<Func<T, bool>> filterExpression)
        {
            return _repositoryQuery.FindOneAsync(filterExpression);
        }

      

      
    }
}
