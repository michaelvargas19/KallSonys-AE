using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalogos.Dominio.Modelo
{

    public abstract class Document : IDocument
    {
        public string Id { get; set; }

        public DateTime FechaCreacion => DateTime.Now;

        public ObjectId GetInternalId(string id)
        {
            ObjectId internalId;
            if (!ObjectId.TryParse(id, out internalId))
                internalId = ObjectId.Empty;

            return internalId;
        }
    }
}
