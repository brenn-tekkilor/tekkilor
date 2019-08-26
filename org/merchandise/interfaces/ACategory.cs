#nullable enable
using System;
using System.Collections.Generic;
using MongoDb.Bson.Serialization.Attributes;
using data.mongodb.interfaces;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using org.common.retail.merchandise.interfaces;
using utility.calendar;

namespace org.common.retail.merchandise.interfaces
{
    [BsonSerializer(typeof(ImpliedImplementationInterfaceSerializer<ACategory, Category>))]
    public abstract class ACategory : IMongoDoc
    {
        public string ColName { get; set; }
        public IIdGenerator IdGen { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string? URL { get; set; }
        public string? Description { get; set; }
        public CategoryFunction? Function { get; set; }
        public IItemSellingRule? ItemSellingRule { get; set; }
        public CategoryLevel? Level { get; set; }
        public Occasions? Occasion { get; set; }
        public SalesRestrictionType? Restriction { get; set; }
        public Dictionary<string, ACategory> Children { get; set; }
        public ACategory(string name, string? url, string? description
                                                            , CategoryFunction? function
                                                            , CategoryLevel? level, Occasions? occasion
                                                            , SalesRestrictionType? restriction)
        {
            Name = name;
            URL = url;
            Description = description;
            Function = function;
            Level = level;
            Restriction = restriction;
            Children = new Dictionary<string, ACategory>();
        }
        public void AddChild(Category c)
        {
            throw new Exception();
        }
        public IMongoDoc Save()
        {
            throw new Exception();
        }
    }
}