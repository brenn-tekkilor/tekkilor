#nullable enable
using MSHTML;
using Retail.Common;
using Utility.Extensions;
using Retail.Interfaces;
using Retail;
using Com.DOM.Extensions;
using System.Collections.Generic;
using Data.Mongo.Interfaces;
using System.Linq;
using Utility.Interfaces;
using System;
using Data.Mongo.Extensions;

namespace Scrape.FF.Factories
{
    public class ItemSellingPricesFactory
        : IMongoFactory
        <IHTMLElement, IItemSellingPrices>
    {
        private const string _p1 =
@"^\s*\$?(?<cp>\d+\.?\d{2}?)\s*$";
        private const string _p2 =
@"^\s*(?<tp>\w+)\:?\s+\$?(?<cp>\d+\.?\d{2}?)\s+discounted\s+from\s+\$?(?<pp>\d+\.?\d{2}?)\s*$";
        private const string _p3 =
@"^((?<tp>\w+)(?:\s+Price\:\s+\$?)(?<cp>\d+\.\d{2}))((((?:\s+Buy\s+)(?<qq>\d{1,})(?:\s+For\s+\$?\s*)(?<qp>\d{1,}\.?\d?\d?)(?:\s+))|(?:\s+\$?\d+\.?\d{2}?\s+))(?:Regular\s+Price\:?\s+)(?<pp>\d{1,}\.?\d{2}?))?$";

        private static readonly string[] _patterns = new string[3] {
            _p3
            , _p2
            , _p1
        };
        private static readonly string[] _keys = new string[5]
        {
            "tp"
            , "cp"
            , "pp"
            , "qq"
            , "qp"
        };
        
        public event
        IFactoryHelper
        <IHTMLElement, IItemSellingPrices>
        .AfterMany? AfterManyEvent;
        public event
            IFactoryHelper
            <IHTMLElement, IItemSellingPrices>
                .AfterOne? AfterOneEvent;
        public event
            IFactoryHelper
            <IHTMLElement, IItemSellingPrices>
            .BeforeMany? BeforeManyEvent;
        public event
            IFactoryHelper
            <IHTMLElement, IItemSellingPrices>
            .BeforeOne? BeforeOneEvent;


        public ItemSellingPricesFactory() { }

        public IDictionary
            <IHTMLElement, IItemSellingPrices?>?
        Make(
            IHTMLElement?
                input)
        {
            BeforeOneEvent
                ?.Invoke(
                    input);

            string? label =
                input
                ?.AttributeValue(
                    "aria-label")
                ?? null;
            IDictionary<string, string>? pricing =
                ParseLabel(label);
            IItemSellingPrices? prices =
                pricing?.Any() ?? false
                    ? new ItemSellingPrices()
                    {
                        CurrentUnitPrice =
                            pricing?.ContainsKey(
                                "cp")
                            ?? false
                                ? pricing?["cp"]
                                .FormatDouble()
                                ?? string.Empty
                            : string.Empty
                        , CurrentPriceType =
                            pricing?.ContainsKey(
                                "pt")
                            ?? false
                                ? pricing?["pt"]
                                switch
                                {
                                    "Regular" => PriceType
                                           .RegularListPrice
                                           .ToString(),
                                    "Sale" =>
                                        PriceType
                                        .PromotionalSaleDiscount
                                        .ToString(),
                                    _ =>
                                        PriceType
                                        .RegularListPrice
                                        .ToString()
                                } ?? string.Empty
                            : string.Empty
                        , QuantityPricingQuantity =
                             pricing?.ContainsKey(
                                "qq")
                            ?? false
                                ? pricing?["qq"]
                                ?? string.Empty
                            : string.Empty
                        , CurrentQuantityPricingPrice =
                             pricing?.ContainsKey(
                                "qp")
                            ?? false
                                ? pricing?["qp"].FormatDouble()
                                ?? string.Empty
                            : string.Empty
                        , PermanentUnitPrice =
                             pricing?.ContainsKey(
                                "pp")
                            ?? false
                                ? pricing?["pp"]
                                .FormatDouble()
                                ?? string.Empty
                            : pricing?.ContainsKey(
                                "cp")
                            ?? false
                                ? pricing?["cp"]
                                .FormatDouble()
                                ?? string.Empty
                            : string.Empty
                    }.Save<IItemSellingPrices>()
                : null;
            AfterOneEvent
                ?.Invoke((input, prices));
            return
                input != null
                    ? new Dictionary
                    <IHTMLElement
                    , IItemSellingPrices?>()
                {
                    { input, prices }
                }
                    : null;
        }
        public IDictionary
            <IHTMLElement, IItemSellingPrices?>?
        Make(
            IEnumerable<IHTMLElement>?
                input)
        {
            BeforeManyEvent
                ?.Invoke(input);
            IDictionary
                <IHTMLElement, IItemSellingPrices?>?
                output
                    = input != null
                        ? input.ToList().Any()
                            ? input.ToList()
                            .ToDictionary(
                                i => i
                                , i => Make(i)?[i])
                : null
            : null;
            AfterManyEvent
                ?.Invoke(
                    output);
                return output;
        }
        private static IDictionary<string, string>?  ParseLabel(
            string? label)
        {
            foreach(string pattern in _patterns)
            {
                if (label?.IsMatch(pattern)
                    ?? false)
                {
                    return
                        label?.GetMatchValues(
                            pattern
                            , _keys)
                        ?? null;
                }
            }
            return null;
        }
    }
}

