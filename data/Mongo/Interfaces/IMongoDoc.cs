#nullable enable
namespace Data.Mongo.Interfaces
{
    public interface IMongoDoc<TDoc> : IMongoEntity<string>
        where TDoc: class, IMongoDoc<TDoc>
    {
        IMongoService<TDoc> Svc { get; }
        bool IsActive { get; set; }
        string Created { get; }
        string Modified { get; set; }
        object Clone();
        bool TryValidateDoc(out dynamic? result);
    }
}
