#nullable enable
namespace data.mongodb.interfaces
{
    public interface IMongoEntity<TId> where TId: class
    {
        TId? Id { get; set; }
    }
}
