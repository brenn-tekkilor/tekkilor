#nullable enable
using data.mongodb.interfaces;
using System.Collections.Generic;
using System.Threading;
using utility.language;

namespace scraper.frysfood.app
{
    public class MongoCollectionRouter : AMongoCollectionRouter
    {
        private readonly IDictionary<string, string> RouterDictionary;
        private static MongoCollectionRouter? Instance;
        private MongoCollectionRouter()
        {
            RouterDictionary = new Dictionary<string, string>()
            {
                { "CategoryMongoDoc",  "category" },
                { "StockItemMongoDoc", "stockItem" },
                { "ItemSellingPricesMongoDoc", "itemSellingPrices" },
            };

        }
        public static new MongoCollectionRouter GetInstance()
        {
            if (Instance == null)
                Instance = new MongoCollectionRouter();
            return Instance;
        }
        public override string? this[string type]
        {
            get
            {
                if (type != null)
                {
                    string[] parts = type.Split('.');
                    type = parts[^1].ToLower(Thread.CurrentThread.CurrentCulture);
                    foreach (string idx in RouterDictionary.Keys)
                    {
                        if (type == idx.ToLower(Thread.CurrentThread.CurrentCulture))
                            return RouterDictionary[idx];
                    }
                    this[type] = type;
                    return this[type];

                }
                return null;
            }
            set
            { 
                RouterDictionary.Add(
                    type.ToPascelCase(), type.ToCamelCase().Replace(
                        "MongoDoc", null, System.StringComparison.CurrentCultureIgnoreCase));
            }
        }
    }
}
