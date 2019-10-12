using Data.Mongo.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using Retail.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Retail.Maps
{
    public class ItemSellingPricesMap : AClassMap<ItemSellingPrices>
    {
        private static BsonClassMap<ItemSellingPrices> _map;
        public override BsonClassMap<ItemSellingPrices> Map
        { 
            get => _map;
        }
        public ItemSellingPricesMap()
        {
            if (!(IsIdGeneratorRegistered(typeof(string))))
                BsonSerializer.RegisterIdGenerator(typeof(string)
                    , new StringObjectIdGenerator());
            if (IsClassMapRegistered())
                _map = (BsonClassMap<ItemSellingPrices>)
                    BsonClassMap.LookupClassMap(typeof(ItemSellingPrices));
            else
                _map = Register();
        }
        public override BsonClassMap<ItemSellingPrices> Register()
        {
            return BsonClassMap
                .RegisterClassMap<ItemSellingPrices>(
            cm =>
            {
                // init
                cm.SetIsRootClass(true);
                cm.SetDiscriminatorIsRequired(true);
                cm.AddKnownType(typeof(ItemSellingPrices));
                cm.AutoMap();
                // Created
                cm.UnmapMember(c => c.Created);
                // CurrentPriceEffectiveDate
                cm.UnmapMember(c => c.CurrentPriceEffectiveDate);
                // CurrentPriceExpirationDate
                cm.UnmapMember(c => c.CurrentPriceExpirationDate);
                // CurrentPriceType
                cm.UnmapMember(c => c.CurrentPriceType);
                // CurrentQuantityPricingPrice
                cm.UnmapMember(c => c.CurrentQuantityPricingPrice);
                // CurrentSavings
                cm.UnmapMember(c => c.CurrentSavings);
                // CurrentUnitPrice
                cm.UnmapMember(c => c.CurrentUnitPrice);
                // HasQuantityPricing
                cm.UnmapMember(c => c.HasQuantityPricing);
                // Id
                cm.UnmapMember(c => c.Id);
                // IsActive
                cm.UnmapMember(c => c.IsActive);
                // IsPricePromo
                cm.UnmapMember(c => c.IsPricePromo);
                // IsTaxIncluded
                cm.UnmapMember(c => c.IsTaxIncluded);
                // Modified
                cm.UnmapMember(c => c.Modified);
                // PermanentPriceEffectiveDate
                cm.UnmapMember(c => c.PermanentPriceEffectiveDate);
                // PermanentPriceType
                cm.UnmapMember(c => c.PermanentPriceType);
                // PermanentQuantityPricingPrice
                cm.UnmapMember(c => c.PermanentQuantityPricingPrice);
                // PermanentSavings
                cm.UnmapMember(c => c.PermanentSavings);
                // PermanentUnitPrice
                cm.UnmapMember(c => c.PermanentUnitPrice);
                // QuantityPricingQuantity
                cm.UnmapMember(c => c.QuantityPricingQuantity);
                // Svc
                cm.UnmapMember(c => c.Svc);
                // TotalSavings
                cm.UnmapMember(c => c.TotalSavings);
                // _created
                cm.MapMember(c => c._created).SetSerializer(
                    new DateTimeSerializer(DateTimeKind.Utc));
                // _currentPriceEffectiveDate
                cm.MapMember(c => c._currentPriceEffectiveDate)
                    .SetSerializer(new DateTimeSerializer(
                        DateTimeKind.Utc));
                // _currentPriceExpirationDate
                cm.MapMember(c => c._currentPriceExpirationDate)
                    .SetSerializer(new DateTimeSerializer(
                        DateTimeKind.Utc));
                // _currentPriceType
                cm.MapMember(c => c._currentPriceType)
                    .SetSerializer(new EnumSerializer<PriceType>(
                        BsonType.String));
                cm.MapMember(c => c._currentPriceType)
                    .SetDefaultValue(PriceType.RegularListPrice);
                cm.MapMember(c => c._currentPriceType)
                    .SetIgnoreIfDefault(true);
                // _currentQuantityPricingPrice
                cm.MapMember(c => c._currentQuantityPricingPrice)
                    .SetDefaultValue(0);
                cm.MapMember(c => c._currentQuantityPricingPrice)
                    .SetIgnoreIfDefault(true);
                // _currentSavings
                cm.MapMember(c => c._currentSavings)
                    .SetDefaultValue((double)0);
                cm.MapMember(c => c._currentSavings)
                    .SetIgnoreIfDefault(true);
                // _currentUnitPrice
                cm.MapMember(c => c._currentUnitPrice)
                    .SetDefaultValue(0);
                cm.MapMember(c => c._currentUnitPrice)
                    .SetIgnoreIfDefault(true);
                // _hash
                cm.MapMember(c => c._hash)
                    .SetDefaultValue(0);
                // _id
                cm.MapIdMember(c => c._id);
                cm.IdMemberMap.SetSerializer(
                    new StringSerializer(BsonType.ObjectId));
                // _isActive
                cm.MapMember(c => c.IsActive).SetDefaultValue(true);
                // IsTaxIncluded
                cm.MapMember(c => c._isTaxIncluded)
                    .SetDefaultValue(false);
                cm.MapMember(c => c._isTaxIncluded)
                    .SetIgnoreIfDefault(true);
                // _modified
                cm.MapMember(c => c._modified).SetSerializer(
                    new DateTimeSerializer(DateTimeKind.Utc));
                // _permanentPriceEffectiveDate
                cm.MapMember(c => c._permanentPriceEffectiveDate)
                    .SetSerializer(new DateTimeSerializer(
                        DateTimeKind.Utc));
                // _permanentPriceType
                cm.MapMember(c => c._permanentPriceType)
                    .SetSerializer(new EnumSerializer<PriceType>(
                        BsonType.String));
                cm.MapMember(c => c._permanentPriceType)
                    .SetDefaultValue(PriceType.RegularListPrice);
                cm.MapMember(c => c._permanentPriceType)
                    .SetIgnoreIfDefault(true);
                // _permanentQuantityPricingPrice
                cm.MapMember(c => c._permanentQuantityPricingPrice)
                    .SetDefaultValue(0);
                cm.MapMember(c => c._permanentQuantityPricingPrice)
                    .SetIgnoreIfDefault(true);
                // _permanentSavings
                cm.MapMember(c => c._permanentSavings)
                    .SetDefaultValue(0);
                cm.MapMember(c => c._permanentSavings)
                    .SetIgnoreIfDefault(true);
                // _permanentUnitPrice
                cm.MapMember(c => c._permanentUnitPrice)
                    .SetDefaultValue(0);
                cm.MapMember(c => c._permanentUnitPrice)
                    .SetIgnoreIfDefault(true);
                // _quantityPricingQuantity
                cm.MapMember(c => c._quantityPricingQuantity)
                    .SetDefaultValue(0);
                cm.MapMember(c => c._quantityPricingQuantity)
                    .SetIgnoreIfDefault(true);
            });
        }
    }
}
