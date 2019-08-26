#nullable enable
using retail.inventory.selling;
using System;
using utility.calendar;

namespace retail.inventory.grouping
{
    public interface ICategory
    {
        string? Name { get; set; }
        Uri? Uri { get; set; }
        string? Description { get; set; }
        CategoryFunction? CategoryFunction { get; set; }
        CategoryLevel? Level { get; set; }
        SellingRule? SellingRule { get; set; }
        Seasonal? Season { get; set; }
        SellingRestriction? Restriction { get; set; }
    }
}
