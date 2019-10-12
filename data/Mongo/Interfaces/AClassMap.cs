#nullable enable
using System;
using MongoDB.Bson.Serialization;

namespace Data.Mongo.Interfaces
{
    public abstract class AClassMap<T>: IClassMap<T>
        where T : class
    {
        public abstract BsonClassMap<T> Map { get; }
        public bool IsClassMapRegistered()
        {
            return BsonClassMap.IsClassMapRegistered(typeof(T));
        }
        public bool IsIdGeneratorRegistered(Type t)
        {
            return BsonSerializer .LookupIdGenerator(t)
                != null ? true : false;
        }
        public abstract BsonClassMap<T> Register();
    }
}
