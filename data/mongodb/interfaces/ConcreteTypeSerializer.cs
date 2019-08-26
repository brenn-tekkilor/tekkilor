#nullable enable
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;

namespace data.mongodb.interfaces
{
    public class ConcreteTypeSerializer<TInterface, TImplementation> : IBsonSerializer<TImplementation> where TImplementation: class, TInterface
    {
        private readonly Lazy<IBsonSerializer> LazyImplementationSerializer;
        public Type ValueType => typeof(TImplementation);
        public ConcreteTypeSerializer()
        {
            var serializer = BsonSerializer.LookupSerializer(typeof(TImplementation));

            LazyImplementationSerializer = new Lazy<IBsonSerializer>(() => serializer);

        }
        public TImplementation Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            if (context != null)
            {
                if (context.Reader.GetCurrentBsonType() == BsonType.Null)
                {
                    throw new NullReferenceException();
                }
                else
                    return (TImplementation)(TInterface)
                        LazyImplementationSerializer.Value.Deserialize(context);
            }
            else
            {
                throw new ArgumentNullException(nameof(context));
            }

        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TImplementation value)
        {
            if (value == null)
            {
                if (context != null)
                    context.Writer.WriteNull();
                else
                    throw new ArgumentNullException(nameof(context));
            }
            else
            {
                var actualType = value.GetType();
                if (actualType == typeof(TImplementation))
                {
                    LazyImplementationSerializer.Value.Serialize(context, args, (TImplementation)value);
                }
                else
                {
                    var serializer = BsonSerializer.LookupSerializer(actualType);
                    serializer.Serialize(context, args, value);
                }
            }
            }
        object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
           return (object)this.Deserialize(context, args);
        }
        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            this.Serialize(context, args, value);
        }
    }
}