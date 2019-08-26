#nullable enable
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using data.mongodb.extensions;
using data.mongodb.interfaces;
using retail.inventory.grouping;
using retail.inventory.selling;
using System.Collections.Generic;
using utility.calendar;
using System;

namespace scraper.frysfood.api.model
{
    public class CategoryMongoDoc : IMongoDoc, ICategory
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public Uri? Uri { get; set; }
        public string? Description { get; set; }
        public CategoryFunction? CategoryFunction { get; set; }
        public CategoryLevel? Level { get; set; }
        public SellingRule? SellingRule { get; set; }
        public Seasonal? Season { get; set; }
        public SellingRestriction? Restriction { get; set; }
        public string? ParentId { get; set; }
        public Dictionary<string, IMongoDoc> Children { get; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public CategoryMongoDoc()
        {
            Created = DateTime.UtcNow;
            Modified = DateTime.UtcNow;
            Children = new Dictionary<string, IMongoDoc>();
        }
        public IMongoDoc AddChild(IMongoDoc? c)
        {
            if (c != null)
            {
                if (c.Id != null)
                    Children.Add(c.Id, c);
                else
                    AddChild(c.Save());
            }
            else
                throw new ArgumentNullException(nameof(c));
            return this.Save();
        }
        public IMongoDoc SetParentId(IMongoDoc? p)
        {
            if (p != null)
                ParentId = p.Id;
            else
                throw new ArgumentNullException(nameof(p));
            return this.Save();
        }
        public IMongoDoc SetParentId(string v)
        {
            ParentId = v;
            return this.Save();
        }
    }
}
