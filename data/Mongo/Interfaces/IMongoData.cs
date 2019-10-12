#nullable enable
using MongoDB.Driver;
namespace Data.Mongo.Interfaces
{
    public interface IMongoData<TDoc>
        where TDoc : class, IMongoDoc<TDoc>
    {
        IMongoDatabase Db { get; }
        IMongoCollection<TDoc> Collection { get; }
    }
}
