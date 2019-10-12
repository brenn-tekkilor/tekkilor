#nullable enable
using Utility.Interfaces;
namespace Data.Mongo.Interfaces
{
    public interface IMongoFactory
        <TIn, TOut>
            : IFactoryHelper
                <TIn, TOut>
    where TIn: class
    where TOut: class
        , IMongoDoc<TOut>
    {
    }
}
