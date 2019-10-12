using Data.Mongo.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;
using Retail.Common;
using Retail.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Retail.Maps
{
    public class CategoryMap : AClassMap<Category>
    {
        private static BsonClassMap<Category> _map;
        public override BsonClassMap<Category> Map
        {
            get => _map;
        }
        public CategoryMap()
        {
            if (!(IsIdGeneratorRegistered(typeof(string))))
                BsonSerializer.RegisterIdGenerator(typeof(string)
                    , new StringObjectIdGenerator());
            if (IsClassMapRegistered())
                _map = (BsonClassMap<Category>)
                    BsonClassMap.LookupClassMap(typeof(Category));
            else
                _map = Register();

        }
        public override BsonClassMap<Category> Register()
        {
            return BsonClassMap.RegisterClassMap<Category>(cm =>
             {
                 // init
                 cm.SetIsRootClass(true);
                 cm.SetDiscriminatorIsRequired(true);
                 cm.AddKnownType(typeof(Category));
                 cm.AutoMap();
                 // CategoryFunction
                 cm.UnmapMember(c => c.CategoryFunction);
                 // CategoryLevel
                 cm.UnmapMember(c => c.CategoryLevel);
                 // Children
                 cm.UnmapMember(c => c.Children);
                 // Created
                 cm.UnmapMember(c => c.Created);
                 // Description
                 cm.UnmapMember(c => c.Description);
                 // IsActive
                 cm.UnmapMember(c => c.IsActive);
                 // Id
                 cm.UnmapMember(c => c.Id);
                 // Link
                 cm.UnmapMember(c => c.Link);
                 // Modified
                 cm.UnmapMember(c => c.Modified);
                 // HasElectronicCoupon
                 cm.UnmapMember(c => c.HasElectronicCoupon);
                 // IsQuantityEntryRequired
                 cm.UnmapMember(c => c.IsQuantityEntryRequired);
                 // IsWeightEntryRequired
                 cm.UnmapMember(c => c.IsWeightEntryRequired);
                 // Name
                 cm.UnmapMember(c => c.Name);
                 // Parent
                 cm.UnmapMember(c => c.Parent);
                 // Season
                 cm.UnmapMember(c => c.Season);
                 // SellingRestriction
                 cm.UnmapMember(c => c.SellingRestriction);
                 // Svc
                 cm.UnmapMember(c => c.Svc);
                 // TaxonomyId
                 cm.UnmapMember(c => c.TaxonomyId);
                 // _categoryFunction
                 cm.MapMember(c => c._categoryFunction)
                 .SetSerializer(new EnumSerializer
                     <CategoryFunction>(
                         BsonType.String));
                 cm.MapMember(c => c._categoryFunction)
                 .SetDefaultValue(
                     CategoryFunction.Operating);
                 cm.MapMember(c => c._categoryFunction)
                     .SetIgnoreIfDefault(true);
                 // _categoryLevel
                 cm.MapMember(c => c._categoryLevel)
                 .SetSerializer(new EnumSerializer
                         <CategoryLevel>(BsonType.String));
                 cm.MapMember(c => c._categoryLevel)
                     .SetDefaultValue(CategoryLevel.Category);
                 cm.MapMember(c => c._categoryLevel)
                     .SetIgnoreIfDefault(true);
                 // _children
                 cm.MapMember(c => c._children).SetSerializer(
                 new DictionaryInterfaceImplementerSerializer
                 <Dictionary<string, ICategory>>(
                     DictionaryRepresentation
                     .ArrayOfDocuments));
                 cm.MapMember(c => c._children)
                     .SetShouldSerializeMethod(
                         cat => ((Category)cat)._children != null
                             ? ((Category)cat)._children
                             .Any()
                                 ? true : false : false);
                 cm.MapMember(c => c._children).SetIgnoreIfNull(true);
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
                 // _isActive
                 cm.MapMember(c => c._isActive)
                     .SetDefaultValue(true);
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
                 // _name
                 cm.MapMember(c => c._name)
                     .SetDefaultValue(
                    string.Empty);
                 cm.MapMember(c => c._name)
                     .SetIgnoreIfDefault(true);
                 // _parent
                 cm.MapMember(c => c._parent)
                    .SetDefaultValue(string.Empty);
                 // _modified
                 cm.MapMember(c => c._modified)
                    .SetSerializer(new DateTimeSerializer(
                    DateTimeKind.Utc));
                 // _season
                 cm.MapMember(c => c._season).SetSerializer(
                    new EnumSerializer<Season>(
                        BsonType.String));
                 cm.MapMember(c => c._season)
                     .SetDefaultValue(Season.All);
                 cm.MapMember(c => c._season)
                     .SetIgnoreIfDefault(true);
                 // _sellingRestriction
                 cm.MapMember(c => c._sellingRestriction)
                     .SetSerializer(new EnumSerializer
                         <SellingRestriction>(BsonType.String));
                 cm.MapMember(c => c._sellingRestriction)
                     .SetDefaultValue(SellingRestriction.None);
                 cm.MapMember(c => c._sellingRestriction)
                     .SetIgnoreIfDefault(true);
                 // _taxonomyId
                 cm.MapMember(c => c._taxonomyId)
                 .SetDefaultValue(string.Empty);
                 cm.MapMember(c => c._uri).SetSerializer(
                     new UriSerializer());
             });
        }
    }
}
