#nullable enable
using retail.inventory.selling;
using System;
using System.Collections.Generic;
using utility.calendar;

namespace retail.inventory.grouping
{
    public class Category : ICategory
    {
        public string? Name { get; set; }
        public Uri? Uri { get; set; }
        public string? Description { get; set; }
        public CategoryFunction? CategoryFunction { get; set; }
        public CategoryLevel? Level { get; set; }
        public SellingRule? SellingRule { get; set; }
        public Seasonal? Season { get; set; }
        public SellingRestriction? Restriction { get; set; }
        public ICategory? Parent { get; set; }
        public List<ICategory>? Children { get; set; }
        public Category()
        {
        }
        public ICategory SetChildren(List<ICategory> children)
        {
            Children = children;
            return this;
        }
        public ICategory AddChild(dynamic child)
        {
            if (Children == null)
                Children = new List<ICategory>();
            Children.Add(child);
            return this;
        }
    }
}
