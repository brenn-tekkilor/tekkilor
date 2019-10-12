#nullable enable
using Data.Mongo.Interfaces;
namespace Retail.Interfaces
{
    public interface IItemSellingPrices : IMongoDoc<IItemSellingPrices>
    {
        string PermanentPriceEffectiveDate { get; }
        string PermanentUnitPrice { get; set; }
        string PermanentQuantityPricingPrice { get; set; }
        string PermanentSavings { get; set; }
        string PermanentPriceType { get; }
        string CurrentPriceEffectiveDate { get; }
        string CurrentPriceExpirationDate { get; set; }
        string CurrentUnitPrice { get; set; }
        string CurrentQuantityPricingPrice { get; set; }
        string CurrentSavings { get; set; }
        string CurrentPriceType { get; set; }
        string QuantityPricingQuantity { get; set; }
        string TotalSavings { get; }
        bool IsTaxIncluded { get; set; }
        bool HasQuantityPricing { get; }
        bool IsPricePromo { get; }
    }
}