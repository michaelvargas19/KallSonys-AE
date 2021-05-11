using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalogos.Dominio.Modelo
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
