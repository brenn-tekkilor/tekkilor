#nullable enable
using System;

namespace retail.inventory.selling
{
    public class SellingRule : ISellingRule
    {
        public SellingStatus? SellingStatus { get; set; }
        public DateTime? SellingStatusEffectiveDate { get; set; }
        public bool? CouponRestricted { get; set; }
        public bool? ElectronicCoupon { get; set; }
        public bool? WeightEntryRequired { get; set; }
        public bool? AllowFoodStamps { get; set; }
        public bool? ProhibitRepeatKey { get; set; }
        public QuantityKeyAction? QuantityKeyAction { get; set; }
        public bool? ProhibitReturn { get; set; }
        public bool? AllowWIC { get; set; }
        public bool? AllowGiveaway { get; set; }
        public bool? AllowRaincheck { get; set; }
        public SellingRule()
        { }
    }
}
