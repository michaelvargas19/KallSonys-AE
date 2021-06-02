using Ordenes.Dominio.ISpecification;
using Ordenes.Dominio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ordenes.Dominio.IRepositories
{
    public interface IMongoRepository_Query<T> where T : IDocument
    {
        IQueryable<T> AsQueryable();

        IEnumerable<T> FilterBy(
            Expression<Func<T, bool>> filterExpression);

        IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<T, bool>> filterExpression,
            Expression<Func<T, TProjected>> projectionExpression);

        T FindOne(Expression<Func<T, bool>> filterExpression);

        Task<T> FindOneAsync(Expression<Func<T, bool>> filterExpression);

        T FindById(string id);

        Task<T> FindByIdAsync(string id);

        //void InsertOne(T document);

        //Task InsertOneAsync(T document);



        IEnumerable<T> Find(ISpecification<T> specification = null);

        bool Contains(ISpecification<T> specification = null);

        int Count(ISpecification<T> specification = null);
    }
}
