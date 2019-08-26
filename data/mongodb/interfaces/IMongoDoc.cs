#nullable enable
using System;
namespace data.mongodb.interfaces
{
    public interface IMongoDoc : IMongoEntity<string>
    {
        new string? Id { get; set; }
        DateTime Created { get; set; }
        DateTime Modified { get; set; }
    }
}
