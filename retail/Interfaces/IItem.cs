#nullable enable
using Data.Mongo.Interfaces;
using System.Collections.Generic;

namespace Retail.Interfaces
{
    public interface IItem : IMongoDoc<IItem>
    {
        string Brand { get; set; }
        ICategory? Category { get; set; }
        string Counting { get; set; }
        string Description { get; set; }
        bool HasElectronicCoupon { get; set; }
        IEnumerable<IHTMLImage>? Images { get; }
        bool IsQuantityEntryRequired { get; set; }
        bool IsWeightEntryRequired { get; set; }
        IItemSellingPrices? ItemSellingPrices { get; set; }
        string Link { get; set; }
        string LongDescription { get; set; }
        string Name { get; set; }
        string POSName { get; set; }
        string RetailPackageSize { get; set; }
        string SubBrand { get; set; }
        string UnitPriceFactor { get; set; }
        string UPC { get; set; }
        IItem AddImage(IHTMLImage image);
    }
}
