#nullable enable
namespace Data.Mongo.Interfaces
{
    public interface IMongoEntity<TId>
    {
        TId Id { get; set; }
    }
}
