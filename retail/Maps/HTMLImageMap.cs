using Data.Mongo.Interfaces;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using Retail.Common;

namespace Retail.Maps
{
    public class HTMLImageMap : AClassMap<HTMLImage>
    {
        private static BsonClassMap<HTMLImage> _map;
        public override BsonClassMap<HTMLImage> Map
        {
            get => _map;
        }
        public HTMLImageMap()
        {
            if (!(IsIdGeneratorRegistered(typeof(string))))
                BsonSerializer.RegisterIdGenerator(typeof(string)
                    , new StringObjectIdGenerator());
            if (IsClassMapRegistered())
                _map = (BsonClassMap<HTMLImage>)
                    BsonClassMap.LookupClassMap(typeof(HTMLImage));
            else
                _map = Register();
        }
        public override BsonClassMap<HTMLImage> Register()
        {
            return BsonClassMap.RegisterClassMap<HTMLImage>(
                cm =>
             {
                 // init
                 cm.SetIsRootClass(true);
                 cm.SetDiscriminatorIsRequired(true);
                 cm.AddKnownType(typeof(HTMLImage));
                 cm.AutoMap();
                // Alt
                 cm.UnmapMember(c => c.Alt);
                 // Src
                 cm.UnmapMember(c => c.Src);
                 // _alt
                 cm.MapMember(c => c._alt).SetDefaultValue(
                     string.Empty);
                 cm.MapMember(c => c._alt).SetIgnoreIfDefault(true);
                 // _src
                 cm.MapMember(c => c._src).SetSerializer(
                     new UriSerializer());
                 cm.MapMember(c => c._src).SetIgnoreIfNull(true);
             });
        }
    }
}
