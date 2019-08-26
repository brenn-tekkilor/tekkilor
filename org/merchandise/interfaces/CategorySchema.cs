#nullable enable
using data.mongodb;
using System.Collections.Generic;
using utility.calendar;
using data.mongodb.interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;

namespace org.common.retail.merchandise.interfaces
{
    public class CategoryMap : IInitMongoDriver
    {
        public void Init()
        {
            BsonClassMap.RegisterClassMap<Category>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
                cm.MapCreator(c => new Category(c.Name, c.URL, c.Description
                    , c.Function, c.Level, c.Occasion, c.Restriction));
                cm.MapIdMember(c => c.Id).SetIdGenerator(StringObjectIdGenerator.Instance);
                cm.IdMemberMap.SetSerializer(new StringSerializer(BsonType.ObjectId));
                cm.MapMember(c => c.Children).SetSerializer(
                    new DictionaryInterfaceImplementerSerializer<Dictionary<string, Category>>(
                        DictionaryRepresentation.ArrayOfDocuments));
                cm.MapMember(c => c.URL).SetSerializer(new NullableSerializer());
                cm.MapMember(c => c.Function).SetSerializer(
                    new NullableSerializer<CategoryFunction>(
                    new EnumSerializer<CategoryFunction>(BsonType.String)));
                cm.MapMember(c => c.Level).SetSerializer(
                    new NullableSerializer<CategoryLevel>(
                    new EnumSerializer<CategoryLevel>(BsonType.String)));
                cm.MapMember(c => c.Restriction).SetSerializer(
                    new NullableSerializer<SalesRestrictionType>(
                    new EnumSerializer<SalesRestrictionType>(BsonType.String)));
                cm.MapMember(c => c.Occasion).SetSerializer(
                    new NullableSerializer<Occasions>(
                    new EnumSerializer<Occasions>(BsonType.String)));
            });
        }
    }
}
