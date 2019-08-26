using data.mongodb.interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using scraper.frysfood.api.model;
using System;

namespace scraper.frysfood.api.model.schemas
{
    public class IMongoDocSchema : IInitMongoDriver
    {
        public IMongoDocSchema()
        {
        }

        public void Init()
        {
            BsonSerializer.RegisterIdGenerator(typeof(IMongoDoc), StringObjectIdGenerator.Instance);
            BsonSerializer.RegisterSerializer(new DiscriminatedInterfaceSerializer<IMongoDoc>());
        }
    }
}
