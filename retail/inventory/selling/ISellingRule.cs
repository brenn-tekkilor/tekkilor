using System;

namespace retail.inventory.selling
{
    public interface ISellingRule
    {
        bool? AllowFoodStamps { get; set; }
        bool? AllowGiveaway { get; set; }
        bool? AllowRaincheck { get; set; }
        bool? AllowWIC { get; set; }
        bool? CouponRestricted { get; set; }
        bool? ElectronicCoupon { get; set; }
        bool? ProhibitRepeatKey { get; set; }
        bool? ProhibitReturn { get; set; }
        QuantityKeyAction? QuantityKeyAction { get; set; }
        SellingStatus? SellingStatus { get; set; }
        DateTime? SellingStatusEffectiveDate { get; set; }
        bool? WeightEntryRequired { get; set; }
    }
}