#nullable enable
using MongoDB.Driver;
using Data.Mongo.Interfaces;
using MSHTML;
using Retail;
using System.Collections.Generic;
using System.Linq;
using Retail.Interfaces;
using Retail.Common;
using Com.DOM.Extensions;
using Utility.Extensions;
using Utility.Interfaces;
using System.Globalization;
using Scrape.FF.Helpers;
using Data.Mongo.Extensions;

namespace Scrape.FF.Factories
{
    public class StockItemFactory
        : IMongoFactory
        <IHTMLElement, IItem>
    {
        public event
    IFactoryHelper
    <IHTMLElement, IItem>
        .AfterMany? AfterManyEvent;
        public event
            IFactoryHelper
            <IHTMLElement, IItem>
                .AfterOne? AfterOneEvent;
        public event
            IFactoryHelper
            <IHTMLElement, IItem>
            .BeforeMany? BeforeManyEvent;
        public event
            IFactoryHelper
            <IHTMLElement, IItem>
            .BeforeOne? BeforeOneEvent;

        public StockItemFactory() { }

        public IDictionary
            <IHTMLElement, IItem?>?
        Make(
            IHTMLElement? input)
        {
            BeforeOneEvent
                ?.Invoke(
                    input);
            if (input == null)
                return null;
            (string name
            , string link
            , string upc) =
            ParseNameLinkUPC(
                input);
            IItem? ParseItem()
            {
                if (string.IsNullOrEmpty(
                    upc))
                    return null;
                return
                    new StockItem()
                    {
                        ItemSellingPrices =
                            ParseItemSellingPrices(
                                input),
                        Link =
                            link,
                        Name =
                            name,
                        RetailPackageSize =
                            ParseRetailPackageSize(input),
                        UPC =
                            upc,
                    }.AddImage(
                        ParseHTMLImage(
                            input));
            }
            IItem? item =
                ParseItem();
            Dictionary<IHTMLElement, IItem?>?
                output =
                    input != null
                        ? new Dictionary
                        <IHTMLElement, IItem?>
                        {
                            {input, item.Save<IItem>() }
                        }
                        : null;
            AfterOneEvent
                ?.Invoke((input, item));
            return
                output;
        }
        public IDictionary
            <IHTMLElement, IItem?>?
            Make(
                IEnumerable<IHTMLElement>?
                    input)
        {
            BeforeManyEvent
                ?.Invoke(
                    input);
            IDictionary
                <IHTMLElement
                    , IItem?>? output
                    = input != null
                        ? input.ToList()
                        .Any()
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
        public static IHTMLElement? ParseHeadingElement(
            IHTMLElement? input)
        {
            return
                input?.ClassFamilyFirst(
                    "ProductCard-nameHeading")
                ?? null;
        }
        public static HTMLImage ParseHTMLImage(
            IHTMLElement? input)
        {
            IHTMLElement? img
                = input != null
                    ? input.TryTagFamily(
                        "IMG"
                        , out IEnumerable
                        <IHTMLElement>?
                        tags)
                    ? tags?
                    .First()
                : null
            : null;
            return new HTMLImage()
            {
                Src =
                    ParseHTMLImageSrc(img),
                Alt =
                    ParseHTMLImageAlt(img),
            };
        }
        public static string ParseHTMLImageAlt(
            IHTMLElement? img)
        {
            return
                img != null
               ? img.TryAttributeValue(
                   "alt"
                    , out string?
                    alt)
                    ? alt
                    ?? string.Empty
                : string.Empty
            : string.Empty;
        }
        public static string ParseHTMLImageSrc(
            IHTMLElement? img)
        {
            return
                img != null
                    ? img.TryAttributeValue(
                        "src"
                        , out string?
                        src)
                        ? src
                    ?? string.Empty
                : string.Empty
            : string.Empty;
        }
        public static IItemSellingPrices? ParseItemSellingPrices(
            IHTMLElement? input)
        {
            IHTMLElement? pricing =
               ParsePriceDisplayElement(
                   input);
            return
                ResourceHelper
                .PricesFactory.Make(
                    pricing)
                ?.FirstOrDefault()
                .Value
                ?? null;
        }
        public static string ParseLink(
            IHTMLElement? heading)
        {
            IHTMLElement? anchor =
                heading != null
                ? heading.TryTagFamily(
                    "A"
                    , out IHTMLElement? a)
                    ? a
                : null
            : null;
            return
                anchor != null
                ? anchor
                .TryAttributeValue(
                    "href"
                    , out string? href)
                    ? href ?? string.Empty
                : string.Empty
            : string.Empty;
        }
        public static string ParseName(
            IHTMLElement? heading)
        {
            return
                heading != null
                ? heading.innerText
            : string.Empty;
        }
       public static (string, string, string) ParseNameLinkUPC(
            IHTMLElement? input)
        {
            IHTMLElement? heading =
                ParseHeadingElement(input);
            string link =
                ParseLink(heading);
            return
                (ParseName(heading), link, ParseUPC(link));
        }
        public static IHTMLElement? ParsePriceDisplayElement(
            IHTMLElement? input)
        {
            return
                input?.TagFamilyFirst(
                    "data")
                ?? null;
        }
        public static string ParseRetailPackageSize(
            IHTMLElement? input)
        {
            return
                input != null
                ? input.TryClassFamily(
                    "ProductCard-sizing"
                    , out IHTMLElement? size)
                    ? size != null
                        ? size.innerText
                        .Trim()
                        .ToLower(
                            CultureInfo
                            .CurrentCulture)
                        : string.Empty
                    : string.Empty
                : string.Empty;
        }
        public static string ParseUPC(
            string? link)
        {
            return
                string.IsNullOrEmpty(
                    link)
                    ? string.Empty
                : link.Split('/')?[^1]
            ?? string.Empty;
        }
    }
}
