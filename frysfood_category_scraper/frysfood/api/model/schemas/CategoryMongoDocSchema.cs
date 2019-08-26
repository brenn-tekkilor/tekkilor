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
    public class CategoryMongoDocSchema : IInitMongoDriver
    {
        public CategoryMongoDocSchema()
        {
        }
        public void Init()
        {
            BsonClassMap.RegisterClassMap<CategoryMongoDoc>(cm =>
           {
               cm.SetDiscriminatorIsRequired(true);
               cm.AutoMap();
               cm.MapIdMember(c => c.Id);
                cm.IdMemberMap.SetSerializer(new StringSerializer(BsonType.ObjectId));
                cm.MapMember(c => c.CategoryFunction).SetSerializer(
                    new NullableSerializer<CategoryFunction>(
                    new EnumSerializer<CategoryFunction>(BsonType.String)));
                cm.MapMember(c => c.Level).SetSerializer(
                    new NullableSerializer<CategoryLevel>(
                    new EnumSerializer<CategoryLevel>(BsonType.String)));
                cm.MapMember(c => c.Restriction).SetSerializer(
                    new NullableSerializer<SellingRestriction>(
                    new EnumSerializer<SellingRestriction>(BsonType.String)));
                cm.MapMember(c => c.Season).SetSerializer(
                    new NullableSerializer<Seasonal>(
                    new EnumSerializer<Seasonal>(BsonType.String)));
                cm.MapMember(c => c.Children).SetSerializer(
                    new DictionaryInterfaceImplementerSerializer<Dictionary<string, IMongoDoc>>(
                    DictionaryRepresentation.ArrayOfDocuments));
                cm.MapMember(c => c.ParentId).SetSerializer(
                    new StringSerializer(BsonType.ObjectId));
               cm.MapMember(c => c.Created).SetSerializer(new DateTimeSerializer(DateTimeKind.Local));
               cm.MapMember(c => c.Modified).SetSerializer(new DateTimeSerializer(DateTimeKind.Local));
           });
        }
    }
}
