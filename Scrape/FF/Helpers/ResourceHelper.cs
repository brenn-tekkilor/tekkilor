#nullable enable
using Scrape.FF.Factories;
using System;
using System.Resources;


namespace Scrape.FF.Helpers

{
    public class ResourceHelper
    {
        private const string _resourceFile =
            @".\Properties\Resources.resources";
        private static readonly
           CategoryFactory _catFactory =
            new CategoryFactory();
        private static readonly
            ItemSellingPricesFactory
            _pricesFactory =
            new ItemSellingPricesFactory();
        private static readonly
            StockItemFactory _stockFactory =
            new StockItemFactory();
        public ResourceHelper() { }
        public static ItemSellingPricesFactory
            PricesFactory => _pricesFactory;
        public static StockItemFactory
            StockFactory => _stockFactory;
        public static string GetValue(string? key)
        {
            using ResourceSet resources =
                new ResourceSet(
                    _resourceFile);
            return
            !string.IsNullOrEmpty(
                key)
                ? resources.GetString(
                    key)
                ?? string.Empty
            : string.Empty;
        }
        public static string GetValue(Type? t)
        {
            using ResourceSet resources
                = new ResourceSet(
                    _resourceFile);
            return
            t != null
                ? !string.IsNullOrEmpty(
                    t.Name)
                    ? resources.GetString(
                        t.Name)
                    ?? string.Empty
                : string.Empty
            : string.Empty;
        }
    }
}
