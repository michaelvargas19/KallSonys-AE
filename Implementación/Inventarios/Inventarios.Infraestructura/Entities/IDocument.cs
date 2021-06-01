using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventarios.Infraestructura.Entities
{
    public interface IDocument
    {
        //[BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        //[BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        //[BsonRepresentation(BsonType.ObjectId)]
        string Id { get; set; }

        DateTime FechaCreacion { get; }

        ObjectId GetInternalId(string id);

    }
}
