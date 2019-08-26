#nullable enable
using retail.inventory.selling;
using data.mongodb.interfaces;
using System;

namespace scraper.frysfood.api.model
{
    public class ItemSellingPricesMongoDoc : IMongoDoc, IItemSellingPrices
    {
        public string? Id { get; set; }
        public PriceType? CurrentPriceType { get; set; }
        public double? CurrentQuantityPricingPrice { get; set; }
        public double? CurrentSavings { get; set; }
        public double? CurrentUnitPrice { get; set; }
        public bool? IncludesTax { get; set; }
        public double? PermanentQuantityPricingPrice { get; set; }
        public double? PermanentUnitPrice { get; set; }
        public bool? QuantityPricingFlag { get; set; }
        public double? QuantityPricingQuantity { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public ItemSellingPricesMongoDoc()
        {
            Created = DateTime.UtcNow;
            Modified = DateTime.UtcNow;
        }
        public bool IsPricePromo()
        {
            return CurrentPriceType == PriceType.RegularListPrice ? false : true;
        }
    }
}
