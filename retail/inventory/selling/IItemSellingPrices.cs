#nullable enable

namespace retail.inventory.selling
{
    public interface IItemSellingPrices
    {
        PriceType? CurrentPriceType { get; set; }
        double? CurrentQuantityPricingPrice { get; set; }
        double? CurrentSavings { get; set; }
        double? CurrentUnitPrice { get; set; }
        bool? IncludesTax { get; set; }
        double? PermanentQuantityPricingPrice { get; set; }
        double? PermanentUnitPrice { get; set; }
        bool? QuantityPricingFlag { get; set; }
        double? QuantityPricingQuantity { get; set; }
    }
}