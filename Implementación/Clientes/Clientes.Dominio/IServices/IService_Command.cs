using Clientes.Dominio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Clientes.Dominio.IServices
{
    public interface IService_Command<T> where T : IDocument
    {
       // void InsertOne(T document);

        Task InsertOneAsync(T document);
        //void InsertMany(ICollection<T> documents);

        Task InsertManyAsync(ICollection<T> documents);

        //void ReplaceOne(T document);

        Task ReplaceOneAsync(T document);

        //void DeleteOne(Expression<Func<T, bool>> filterExpression);

        Task DeleteOneAsync(Expression<Func<T, bool>> filterExpression);

        //void DeleteById(string id);

        Task DeleteByIdAsync(string id);

        //void DeleteMany(Expression<Func<T, bool>> filterExpression);

        Task DeleteManyAsync(Expression<Func<T, bool>> filterExpression);

    }
}
