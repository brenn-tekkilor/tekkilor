#nullable enable
using System;

namespace retail.inventory.selling
{
    public class ItemSellingPrices : IItemSellingPrices
    {
        public double? PermanentUnitPrice { get; set; }
        public DateTime? PermanentPriceEffectiveDate { get; set; }
        public double? CurrentUnitPrice { get; set; }
        public PriceType? CurrentPriceType { get; set; }
        public DateTime? CurrentPriceEffectiveDate { get; set; }
        public DateTime? CurrentPriceExpirationDate { get; set; }
        public double? CurrentSavings { get; set; }
        public bool? QuantityPricingFlag { get; set; }
        public double? QuantityPricingQuantity { get; set; }
        public double? CurrentQuantityPricingPrice { get; set; }
        public double? PermanentQuantityPricingPrice { get; set; }
        public double? CurrentReturnPrice { get; set; }
        public bool? IncludesTax { get; set; }
        public ItemSellingPrices()
        { }
    }
}
