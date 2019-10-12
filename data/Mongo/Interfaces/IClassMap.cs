using MongoDB.Bson.Serialization;
using System;

namespace Data.Mongo.Interfaces
{
    public interface IClassMap<T>
        where T: class
    {
        BsonClassMap<T> Register();
    }
}
