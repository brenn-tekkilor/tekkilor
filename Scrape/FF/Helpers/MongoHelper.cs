# nullable enable
using Data.Mongo;
using Data.Mongo.Extensions;
using Data.Mongo.Interfaces;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using Retail;
using Retail.Common;
using Retail.Interfaces;
using Retail.Maps;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Utility.Extensions;

namespace Scrape.FF.Helpers
{
    public static class MongoHelper
    {
        public static void InitializeDb()
        {
            System.Environment.SetEnvironmentVariable(
                "MONGO_DB", "kroger_test");
            System.Environment.SetEnvironmentVariable(
                "MONGO_USER", "apptests");
            System.Environment.SetEnvironmentVariable(
                "MONGO_PWD", "d0tn3t");
            System.Environment.SetEnvironmentVariable(
                "MONGO_HOST", "localhost");
            System.Environment.SetEnvironmentVariable(
                "MONGO_PORT", "27017");
            BsonSerializer.RegisterIdGenerator(
                typeof(string)
                , new StringObjectIdGenerator());
            _ = new CategoryMap();
            _ = new ItemSellingPricesMap();
            _ = new HTMLImageMap();
            _ = new StockItemMap();
            _ = new Category();
            _ = new ItemSellingPrices();
            _ = new HTMLImage();
            _ = new StockItem();

        }
}
}
