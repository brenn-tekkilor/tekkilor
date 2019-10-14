#nullable enable
using Data.Mongo.Interfaces;
using System.Collections.Generic;
namespace Retail.Interfaces
{
    public interface ICategory : IMongoDoc<ICategory>
    {
        string CategoryFunction { get; set; }
        string CategoryLevel { get; set; }
        IEnumerable<ICategory>? Children { get; }
        string Description { get; set; }
        bool HasElectronicCoupon { get; set; }
        bool IsQuantityEntryRequired { get; set; }
        bool IsWeightEntryRequired { get; set; }
        string Link { get; set; }
        string Name { get; set; }
        ICategory? Parent { get; set; }
        string Season { get; set; }
        string SellingRestriction { get; set; }
        string TaxonomyId { get; set; }
        ICategory? AddChild(ICategory child);
        string ParseTaxonomyId(string link);

    }
}
