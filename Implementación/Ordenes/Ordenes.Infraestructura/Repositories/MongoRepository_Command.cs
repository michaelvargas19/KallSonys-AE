using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using Ordenes.Dominio.IRepositories;
using Ordenes.Dominio.ISpecification;
using Ordenes.Dominio.Modelo;
using Ordenes.Dominio.Modelo.Settings;
using Ordenes.Infraestructura.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ordenes.Infraestructura.Repositories
{
    public class MongoRepository_Command<T> : IMongoRepository_Command<T> where T : IDocument
    {
        private readonly IMongoCollection<T> _collection;

        public MongoRepository_Command(IMongoDbSettings settings, IConfiguration configuration)
        {
            var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<T>(GetCollectionName(typeof(T)));
        }

        private protected string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }

      
      

        public virtual void InsertOne(T document)
        {
            //document.Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            _collection.InsertOne(document);
        }

        public virtual Task InsertOneAsync(T document)
        {
            //document.Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            return Task.Run(() => _collection.InsertOneAsync(document));
        }

        public void InsertMany(ICollection<T> documents)
        {
            foreach (T document in documents)
            {
                document.Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            }

            _collection.InsertMany(documents);
        }


        public virtual async Task InsertManyAsync(ICollection<T> documents)
        {
            foreach (T document in documents)
            {
                document.Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            }

            await _collection.InsertManyAsync(documents);
        }

        public void ReplaceOne(T document)
        {
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, document.Id);
            _collection.FindOneAndReplace(filter, document);
        }

        public virtual async Task ReplaceOneAsync(T document)
        {
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, document.Id);
            await _collection.FindOneAndReplaceAsync(filter, document);
        }

        public void DeleteOne(Expression<Func<T, bool>> filterExpression)
        {
            _collection.FindOneAndDelete(filterExpression);
        }

        public Task DeleteOneAsync(Expression<Func<T, bool>> filterExpression)
        {
            return Task.Run(() => _collection.FindOneAndDeleteAsync(filterExpression));
        }

        public void DeleteById(string id)
        {
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, id);
            _collection.FindOneAndDelete(filter);
        }

        public Task DeleteByIdAsync(string id)
        {
            return Task.Run(() =>
            {
                var objectId = new ObjectId(id);
                var filter = Builders<T>.Filter.Eq(doc => doc.Id, id);
                _collection.FindOneAndDeleteAsync(filter);
            });
        }

        public void DeleteMany(Expression<Func<T, bool>> filterExpression)
        {
            _collection.DeleteMany(filterExpression);
        }

        public Task DeleteManyAsync(Expression<Func<T, bool>> filterExpression)
        {
            return Task.Run(() => _collection.DeleteManyAsync(filterExpression));
        }


      
    }
}
