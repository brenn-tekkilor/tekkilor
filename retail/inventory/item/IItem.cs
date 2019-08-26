#nullable enable

using System;

namespace retail.inventory.item
{
    public interface IItem
    {
        public string? ItemId { get; set; }
        public Uri? Uri { get; set; }
        public string? CategoryId { get; set; }
        public string? ItemSellingPricesId { get; set; }
        public string? Brand { get; set; }
        public string? Name { get; set; }
        public string? POSName { get; set; }
        public string? Description { get; set; }
        public string? LongDescription { get; set; }
        public string? SubBrand { get; set; }
        public string? Counting { get; set; }
        public bool? IsSoldByWeight { get; set; }
        public string? RetailPackageSize { get; set; }
        public double? UnitPriceFactor { get; set; }
    }
}
