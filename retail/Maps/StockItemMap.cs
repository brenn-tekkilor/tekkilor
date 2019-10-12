using Data.Mongo.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Retail.Maps
{
    public class StockItemMap : AClassMap<StockItem>
    {
        private static BsonClassMap<StockItem> _map;
        public override BsonClassMap<StockItem> Map
        {
            get => _map;
        }
        public StockItemMap()
        {
            if (!(IsIdGeneratorRegistered(typeof(string))))
                BsonSerializer.RegisterIdGenerator(typeof(string)
                    , new StringObjectIdGenerator());
            if (IsClassMapRegistered())
                _map = (BsonClassMap<StockItem>)
                    BsonClassMap.LookupClassMap(typeof(StockItem));
            else
                _map = Register();

        }
        public override BsonClassMap<StockItem> Register()
        {
            return BsonClassMap.RegisterClassMap<StockItem>(cm =>
             {
                 // init
                 cm.SetIsRootClass(true);
                 cm.SetDiscriminatorIsRequired(true);
                 cm.AddKnownType(typeof(StockItem));
                 cm.AutoMap();
                 // Brand
                 cm.UnmapMember(c => c.Brand);
                 // Category
                 cm.UnmapMember(c => c.Category);
                 // Counting
                 cm.UnmapMember(c => c.Counting);
                 // Created
                 cm.UnmapMember(c => c.Created);
                 // Description
                 cm.UnmapMember(c => c.Description);
                 // HasElectronicCoupon
                 cm.UnmapMember(c => c.HasElectronicCoupon);
                 // Id
                 cm.UnmapMember(c => c.Id);
                 // Images
                 cm.UnmapMember(c => c.Images);
                 // IsActive
                 cm.UnmapMember(c => c.IsActive);
                 // IsQuantityEntryRequired
                 cm.UnmapMember(c => c.IsQuantityEntryRequired);
                 // IsWeightEntryRequired
                 cm.UnmapMember(c => c.IsWeightEntryRequired);
                 // ItemSellingPrices
                 cm.UnmapMember(c => c.ItemSellingPrices);
                 // Link
                 cm.UnmapMember(c => c.Link);
                 // LongDescription
                 cm.UnmapMember(c => c.LongDescription);
                 // Modified
                 cm.UnmapMember(c => c.Modified);
                 // Name
                 cm.UnmapMember(c => c.Name);
                 // POSName
                 cm.UnmapMember(c => c.POSName);
                 // RetailPackageSize
                 cm.UnmapMember(c => c.RetailPackageSize);
                 // SubBrand
                 cm.UnmapMember(c => c.SubBrand);
                 // Svc
                 cm.UnmapMember(c => c.Svc);
                 // UnitPriceFactor
                 cm.UnmapMember(c => c.UnitPriceFactor);
                 // UPC
                 cm.UnmapMember(c => c.UPC);
                 // _brand
                 cm.MapMember(c => c._brand)
                    .SetDefaultValue(string.Empty);
                 cm.MapMember(c => c._brand)
                    .SetIgnoreIfDefault(true);
                 // _category
                 cm.MapMember(c => c._category)
                    .SetDefaultValue(string.Empty);
                 // _counting
                 cm.MapMember(c => c._counting)
                    .SetDefaultValue(string.Empty);
                 cm.MapMember(c => c._counting)
                    .SetIgnoreIfDefault(true);
                 // _created
                 cm.MapMember(c => c._created)
                    .SetSerializer(new DateTimeSerializer(
                    DateTimeKind.Utc));
                 // _description
                 cm.MapMember(c => c._description)
                    .SetDefaultValue(string.Empty);
                 cm.MapMember(c => c._description)
                    .SetIgnoreIfDefault(true);
                 // _hasElectronicCoupon
                 cm.MapMember(c => c._hasElectronicCoupon)
                    .SetDefaultValue(false);
                 cm.MapMember(c => c._hasElectronicCoupon)
                    .SetIgnoreIfDefault(true);
                 // _id
                 cm.MapIdMember(c => c._id);
                 cm.IdMemberMap.SetSerializer(
                     new StringSerializer(BsonType.ObjectId));
                 // _images
                 cm.MapMember(c => c._images).SetShouldSerializeMethod(
                    item => ((StockItem)item)._images != null
                        ? ((StockItem)item)._images.Any()
                            ? true : false : false);
                 cm.MapMember(c => c._images).SetIgnoreIfNull(true);
                 // _isQuantityEntryRequired
                 cm.MapMember(c => c._isQuantityEntryRequired)
                    .SetDefaultValue(false);
                 cm.MapMember(c => c._isQuantityEntryRequired)
                    .SetIgnoreIfDefault(true);
                 // _isWeightEntryRequired
                 cm.MapMember(c => c._isWeightEntryRequired)
                    .SetDefaultValue(false);
                 cm.MapMember(c => c._isWeightEntryRequired)
                    .SetIgnoreIfDefault(true);
                 // _itemSellingPrices
                 cm.MapMember(c => c._itemSellingPrices)
                    .SetDefaultValue(string.Empty);
                 cm.MapMember(c => c._itemSellingPrices)
                    .SetIgnoreIfDefault(true);
                 // _longDescription
                 cm.MapMember(c => c._longDescription)
                    .SetDefaultValue(string.Empty);
                 cm.MapMember(c => c._longDescription)
                    .SetIgnoreIfDefault(true);
                 // _modified
                 cm.MapMember(c => c._modified)
                    .SetSerializer(new DateTimeSerializer(
                    DateTimeKind.Utc));
                 // _name
                 cm.MapMember(c => c._name)
                    .SetDefaultValue(string.Empty);
                 cm.MapMember(c => c._name)
                    .SetIgnoreIfDefault(true);
                 // _posName
                 cm.MapMember(c => c._posName)
                    .SetDefaultValue(string.Empty);
                 cm.MapMember(c => c._posName)
                    .SetIgnoreIfDefault(true);
                 // _retailPackageSize
                 cm.MapMember(c => c._retailPackageSize)
                    .SetDefaultValue(string.Empty);
                 cm.MapMember(c => c._retailPackageSize)
                    .SetIgnoreIfDefault(true);
                 // _subBrand
                 cm.MapMember(c => c._subBrand)
                    .SetDefaultValue(string.Empty);
                 cm.MapMember(c => c._subBrand)
                    .SetIgnoreIfDefault(true);
                 // _unitPriceFactor
                 cm.MapMember(c => c._unitPriceFactor)
                    .SetDefaultValue(0);
                 cm.MapMember(c => c._unitPriceFactor)
                    .SetIgnoreIfDefault(true);
                 // _upc
                 cm.MapMember(c => c._upc)
                   .SetDefaultValue(string.Empty);
                 cm.MapMember(c => c._upc)
                    .SetIgnoreIfDefault(true);
                 // _uri
                 cm.MapMember(c => c._uri)
                    .SetSerializer(new UriSerializer());
                 cm.MapMember(c => c._uri)
                    .SetIgnoreIfNull(true);
             });
        }
    }
}