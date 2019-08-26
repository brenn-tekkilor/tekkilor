#nullable enable
using data.mongodb.interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using retail.inventory.selling;
using System;
using System.Collections.Generic;
using System.Text;
using scraper.frysfood.api.model;

namespace scraper.frysfood.api.model.schemas
{
    public class ItemSellingPricesMongoDocSchema : IInitMongoDriver
    {
        public ItemSellingPricesMongoDocSchema()
        {
        }
        public void Init()
        {
            BsonClassMap.RegisterClassMap<ItemSellingPricesMongoDoc>(cm =>
            {
                cm.SetDiscriminatorIsRequired(true);
                cm.AutoMap();
                cm.MapIdMember(c => c.Id);
                cm.IdMemberMap.SetSerializer(new StringSerializer(BsonType.ObjectId));
                cm.MapMember(c => c.CurrentPriceType).SetSerializer(
                    new NullableSerializer<PriceType>(
                    new EnumSerializer<PriceType>(BsonType.String)));
                cm.MapMember(c => c.Created).SetSerializer(new DateTimeSerializer(DateTimeKind.Local));
                cm.MapMember(c => c.Modified).SetSerializer(new DateTimeSerializer(DateTimeKind.Local));

            });
        }
    }
}

