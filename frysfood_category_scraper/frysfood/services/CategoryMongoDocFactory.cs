using data.mongodb.interfaces;
using retail.inventory.grouping;
using scraper.frysfood.api.model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using MSHTML;
using data.mongodb.extensions;

namespace scraper.frysfood.services
{
    class CategoryMongoDocFactory : AMongoDocFactory
    {
        private static CategoryMongoDocFactory Instance;
        private static string ParseName(string v)
        {
            Match m = new Regex(@"(?:\s*?)((\s?([A-za-z&\-\,\.\'\(\)`\!\?\*\%\#\@\:\;\/_\+\=])*)*)(?:\s\(\d{1,5}\))").Match(v);
            if (m.Success)
                return m.Groups[1].Value.Trim();
            else
                return m.Value;
        }
        public static CategoryMongoDocFactory GetInstance()
        {
            if (Instance == null)
                Instance = new CategoryMongoDocFactory();
            return Instance;
        }
        private CategoryMongoDocFactory() { }
        public override IMongoDoc GetDoc(IHTMLDOMNode n)
        {
            CategoryMongoDoc d = new CategoryMongoDoc()
            {
                CategoryFunction = CategoryFunction.Marketing,
                Level = CategoryLevel.Category,
            };
            d.Name = ParseName(GetInnerText(n));
            d.Uri = GetUri(n);
            return ((IMongoDoc)d).Save();
        }
    }
}
