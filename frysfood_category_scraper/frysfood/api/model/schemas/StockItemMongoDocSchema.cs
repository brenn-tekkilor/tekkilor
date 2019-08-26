#nullable enable
using data.mongodb.interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;
using retail.inventory.grouping;
using retail.inventory.selling;
using System;
using System.Collections.Generic;
using utility.calendar;

namespace scraper.frysfood.api.model.schemas
{
    public class StockItemMongoDocSchema : IInitMongoDriver
    {
        public StockItemMongoDocSchema()
        {
        }

        public void Init()
        {
            BsonClassMap.RegisterClassMap<StockItemMongoDoc>(cm =>
            {
                cm.SetDiscriminatorIsRequired(true);
                cm.AutoMap();
                cm.MapIdMember(c => c.Id);
                cm.IdMemberMap.SetSerializer(new StringSerializer(BsonType.ObjectId));
                cm.MapMember(c => c.CategoryId).SetSerializer(
                    new StringSerializer(BsonType.ObjectId));
                cm.MapMember(c => c.ItemSellingPricesId).SetSerializer(
                    new StringSerializer(BsonType.ObjectId));
                cm.MapMember(c => c.Created).SetSerializer(new DateTimeSerializer(DateTimeKind.Local));
                cm.MapMember(c => c.Modified).SetSerializer(new DateTimeSerializer(DateTimeKind.Local));

            });
        }
    }
}
