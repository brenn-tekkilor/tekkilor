#nullable enable
using data.mongodb.interfaces;
using scraper.frysfood.api.model;
using retail.inventory.selling;
using MSHTML;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using data.mongodb;
using System.Threading;

namespace scraper.frysfood.services
{
    public class ItemSellingPricesMongoDocFactory : AMongoDocFactory
    {
        private static ItemSellingPricesMongoDocFactory? Instance;
        public static ItemSellingPricesMongoDocFactory GetInstance()
        {
            if (Instance == null)
                Instance = new ItemSellingPricesMongoDocFactory();
            return Instance;
        }
        public static double ParsePrice(string? v)
        {
            return v != null ? double.Parse(v.Remove(v.IndexOf('$', StringComparison.CurrentCultureIgnoreCase), 1)
                , Thread.CurrentThread.CurrentCulture.NumberFormat) : throw new ArgumentNullException(nameof(v));
        }
        public static IHTMLElement6? GetPricingBlock(IHTMLElement6 e, string? v)
        {
            IHTMLElement6? b = GetElementByClass(e, "PriceDisplay");
            if (b != null)
            {
                if (v == null)
                    return b;
                return GetElementByClass(b, v);
            }
            return null;
        }
        private ItemSellingPricesMongoDocFactory() { }
        private bool PriceIsPromo(IHTMLElement6 e)
        {
            return GetElementByClass(e, "kds-Price-promotional") == null ?
                    false : true;
        }
        private double? ParsePrice(IHTMLElement6? e)
        {
            return e != null ? ParsePrice(GetInnerText(e)) : throw new ArgumentNullException(nameof(e));
        }
        private IHTMLElement6? GetCurrentPromotionalPriceBlock(IHTMLElement6 e)
        {
            return GetPricingBlock(e, "kds-Price-promotional");
        }
        private IHTMLElement6? GetCurrentPromotionalPriceOriginalPriceDisplayBlock(IHTMLElement6 e)
        {
            return GetPricingBlock(e, "kds-Price-original");
        }
        private IHTMLElement6? GetRegularPriceDisplayBlock(IHTMLElement6 e)
        {
            return GetPricingBlock(e, "kds-Price-singular");
        }
        private IMongoDoc SetQuantityPricing(ItemSellingPricesMongoDoc d, dynamic e)
        {
            Match m = new Regex(@"^.*?(\d)\s[F|f]or\s\$(\d\.?\d?\d?).*?$").Match(
                (string?)GetAttributeValue(GetPricingBlock(e, null), "aria-label"));
            if (m.Success)
            {
                d.QuantityPricingFlag = true;
                d.CurrentQuantityPricingPrice = double.Parse(m.Groups[2].Value
                    , Thread.CurrentThread.CurrentCulture.NumberFormat);
                d.QuantityPricingQuantity = double.Parse(m.Groups[1].Value
                    , Thread.CurrentThread.CurrentCulture.NumberFormat);
            }
            else
                d.QuantityPricingFlag = false;
            return d;
        }
        public static ItemSellingPricesMongoDoc? GetDoc (string? v)
        {
            return v != null ? (ItemSellingPricesMongoDoc?)Db.FindById(
                    Db.GetCollection(new ItemSellingPricesMongoDoc())
                    , v) : throw new ArgumentNullException(nameof(v));
        }
        public static ItemSellingPricesMongoDoc? GetDoc(StockItemMongoDoc? i)
        {
            return i != null ? i.ItemSellingPricesId != null ? GetDoc(i.ItemSellingPricesId)
                : null : throw new ArgumentNullException(nameof(i));
        }
        public IMongoDoc GetDoc(StockItemMongoDoc i, IHTMLDOMNode n)
        {
            ItemSellingPricesMongoDoc? o = (ItemSellingPricesMongoDoc?)GetDoc(i);
            ItemSellingPricesMongoDoc c = (ItemSellingPricesMongoDoc)GetDoc(n);
            return o != null ? GetDoc(new ItemSellingPricesMongoDoc()
            {
                IncludesTax = o.IncludesTax,
                PermanentQuantityPricingPrice = o.PermanentQuantityPricingPrice,
                QuantityPricingFlag = o.QuantityPricingFlag == true
                    ? true : c.QuantityPricingFlag == true ? true : false,
                QuantityPricingQuantity = c.QuantityPricingQuantity != null
                    ? c.QuantityPricingQuantity : o.QuantityPricingQuantity != null
                        ? o.QuantityPricingQuantity : null,
                CurrentQuantityPricingPrice = c.CurrentQuantityPricingPrice != null
                    ? c.CurrentQuantityPricingPrice : o.CurrentQuantityPricingPrice != null
                        ? o.CurrentQuantityPricingPrice : null,
                CurrentPriceType = c.CurrentPriceType,
                CurrentSavings = c.CurrentSavings,
                CurrentUnitPrice = c.CurrentUnitPrice,
            }) : GetDoc(c);
        }
        public static IMongoDoc GetDoc(ItemSellingPricesMongoDoc d)
        {
            IMongoDoc? i = Db.FindDocEqualTo(d);
            return i ?? d;
        }
        public override IMongoDoc GetDoc(IHTMLDOMNode n)
        {
            IHTMLElement6 e = (IHTMLElement6)n;
            ItemSellingPricesMongoDoc d = new ItemSellingPricesMongoDoc() 
            {
                IncludesTax = false
            };
            if (PriceIsPromo(e))
            {
                d.CurrentPriceType = PriceType.PromotionalSaleDiscount;
                d.CurrentUnitPrice = ParsePrice(
                    GetCurrentPromotionalPriceBlock(e));
                d.PermanentUnitPrice = ParsePrice(
                    GetCurrentPromotionalPriceOriginalPriceDisplayBlock(e));
                d = (ItemSellingPricesMongoDoc)SetQuantityPricing(d, e);
            }
            else
            {
                d.CurrentPriceType = PriceType.RegularListPrice;
                d.CurrentUnitPrice = d.PermanentUnitPrice = ParsePrice(
                    GetRegularPriceDisplayBlock(e));
                d.QuantityPricingFlag = false;
            }
            d.CurrentSavings = d.PermanentUnitPrice != null
                ? d.CurrentUnitPrice != null
                    ? d.PermanentUnitPrice - d.CurrentUnitPrice
                    : 0
                : 0;
            return d;
        }
    }
}
