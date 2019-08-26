using System;
namespace data.mongodb.interfaces
{
    public abstract class AMongoCollectionRouter
    {
        public abstract string this[string type] { get; set; }
    }
}