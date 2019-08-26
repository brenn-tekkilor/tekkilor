#nullable enable
using System;
using data.mongodb.interfaces;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;

namespace data.mongodb.extensions
{
    public static class MongoDocExtensions
    {
        public static AMongoCollectionRouter? ColRouter { get; set; }
        public static string GetColName(this IMongoDoc d)
        {
            if (d != null && ColRouter != null)
                return ColRouter[d.GetType().ToString()];
            throw new MemberAccessException();
        }
        public static IMongoCollection<IMongoDoc> GetCol(this IMongoDoc d)
        {
            return Db.GetCollection(d);
        }
        public static IMongoDoc GenerateId(this IMongoDoc d)
        {
            if (d != null)
            {
                if (d.Id == null)
                    return Db.Save(d);
                else
                    return d;
            }
            throw new ArgumentNullException(nameof(d));
        }
        public static IMongoDoc Save(this IMongoDoc d)
        {
            if (d != null)
                return Db.Save(d);
            throw new ArgumentNullException(nameof(d));
        }
    }
}