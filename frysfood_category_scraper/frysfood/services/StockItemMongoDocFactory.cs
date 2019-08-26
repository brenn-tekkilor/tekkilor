#nullable enable
using data.mongodb.interfaces;
using scraper.frysfood.api.model;
using utility.language;
using MSHTML;
using SHDocVw;
using retail.inventory.item;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Text.RegularExpressions;
using data.mongodb;
using System.Threading;
using System.Resources;
using System.Collections;
using System.Linq;
using data.mongodb.extensions;

namespace scraper.frysfood.services
{
    public class StockItemMongoDocFactory : AMongoDocFactory
    {
        private const string resourceFile = @".\Properties\Resources.resources";

        private static StockItemMongoDocFactory? Instance;
        public static StockItemMongoDocFactory GetInstance()
        {
            if (Instance == null)
                Instance = new StockItemMongoDocFactory();
            return Instance;
        }
        private StockItemMongoDocFactory() { }
        public override IMongoDoc GetDoc(IHTMLDOMNode? n)
        {
            if (n != null)
            {
                using System.Resources.ResourceSet resources
                    = new ResourceSet(resourceFile);
                StockItemMongoDoc d = new StockItemMongoDoc();
                string? u = resources.GetString("_uri_home");
                IHTMLElement6 e = (IHTMLElement6)n;
                IHTMLElement6? h = GetElementByClass(
                    e, "ProductCard-nameHeading");
                IHTMLElement6? a = h != null ? GetElementByClass(
                    h, "ProductCard-name") : null;
                if (a != null)
                {
                    string? l = (string?)GetAttributeValue(a, "href");
                    string? url = u != null ? l != null ? u + l : u : l ?? null;
                    d.Uri = Uri.TryCreate(url, UriKind.Absolute, out Uri? result) ? result : null;
                    string[]? parts = l?.Split('/');
                    d.ItemId = parts?[^1];
                    IMongoDoc? doc = d.ItemId != null ? Db.GetDocByUniqueField(
                        d, "ItemId") : null;
                    d = doc != null ? (StockItemMongoDoc)doc : d;
                    d.ItemSellingPricesId = ItemSellingPricesMongoDocFactory
                        .GetInstance().GetDoc(d, n).Save().Id;
                    if (doc == null)
                    {
                        Match m;
                        d.Name = GetInnerText(a);
                        List<Regex> r = new List<Regex>()
                        { 
                            new Regex(@"^(.*?)[™©®].*?$"),
                            new Regex(@"^[K|k]roger\s.*?$"),
                        };
                        foreach (Regex i in r)
                        {
                            m = i.Match(d.Name);
                            if (m.Success)
                            {
                                d.Brand = m.Groups[1].Value.Trim();
                                break;
                            }
                        }
                        d.Brand ??= d.Name?.Split(' ')[0];
                        d.RetailPackageSize = GetInnerText(
                            GetElementByClass(e, "ProductCard-sizing"))?.Trim();
                        if (new Regex(@"^.*?\d{1,3}\.\d{2}\s?/\s?lb.*?$")
                            .Match(d.RetailPackageSize).Success
                            || new Regex(@"^1?\s?lb$")
                            .Match(d.RetailPackageSize).Success)
                        {
                            d.IsSoldByWeight = true;
                            d.UnitPriceFactor = 1.00;
                            d.Counting = "lb";
                        }
                        else
                        {
                            d.IsSoldByWeight = false;
                            m = new Regex(@"^.*?((\s?[\.\d\s\-\$x/]*)+?).*?$")
                                .Match(d.RetailPackageSize);
                            d.UnitPriceFactor = m.Success ? double.Parse(
                                m.Groups[1].Value.Trim()
                                , Thread.CurrentThread.CurrentCulture.NumberFormat)
                                : 1.00;
                            m = new Regex(
                                @"^.*?(?:(?:\s?[\.\d\s\-\$x/]*)*?)((\s?[A-Za-z\-]*)+?).*?$")
                                .Match(d.RetailPackageSize);
                            d.Counting = m.Success ? m.Groups[1].Value.Trim()
                            : "units";
                        }
                        return d;
                    }
                }
            }
                throw new ArgumentNullException(nameof(n));
        }

    }
}
