using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordenes.Dominio.Modelo
{

    public abstract class Document : IDocument
    {
        //[BsonRepresentation(BsonType.ObjectId)]
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }

        public DateTime FechaCreacion => DateTime.Now;

        public string GetInternalId(string id)
        {
            string internalId = id + MongoDB.Bson.ObjectId.GenerateNewId(DateTime.Now).ToString();

            return internalId;
        }
        
    }
}
