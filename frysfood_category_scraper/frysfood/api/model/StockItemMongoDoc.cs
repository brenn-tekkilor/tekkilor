#nullable enable
using data.mongodb;
using data.mongodb.interfaces;
using retail.inventory.item;
using System;

namespace scraper.frysfood.api.model
{
    public class StockItemMongoDoc : IMongoDoc, IItem
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? POSName { get; set; }
        public string? ItemSellingPricesId { get; set; }
        public string? ItemId { get; set; }
        public Uri? Uri { get; set; }
        public string? CategoryId { get; set; }
        public string? Brand { get; set; }
        public string? Description { get; set; }
        public string? LongDescription { get; set; }
        public string? SubBrand { get; set; }
        public string? Counting { get; set; }
        public bool? IsSoldByWeight { get; set; }
        public string? RetailPackageSize { get; set; }
        public double? UnitPriceFactor { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public StockItemMongoDoc()
        {
            Created = DateTime.UtcNow;
            Modified = DateTime.UtcNow;
        }
        public StockItemMongoDoc SetCategoryId(string? v)
        {
            CategoryId = v ?? throw new ArgumentNullException(nameof(v));
            return this;
        }
    }
}
