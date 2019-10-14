using Retail;
using Retail.Interfaces;
using Scrape.FF.Helpers;
using System;
using Data.Mongo.Extensions;
using Data.Mongo;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using Retail.Maps;
using Retail.Common;

namespace Scrape
{
    internal class Program
    {
        private static CategoryHelper _categories =
                    new CategoryHelper();
        private static void Main()
        {
            {
                InitializeData();
                _ = new ResourceHelper();
                _ = BrowserHelper.Instance();
                
                
            }
            static void Run()
            {
                DisplayUI();
                string userSelection = Console.ReadLine();
                switch (userSelection)
                {
                    case "A":
                    case "a":
                        _ = BrowserHelper.Reset();
                        _categories.Autorun(null);
                        Run();
                        break;
                    case "D":
                    case "d":
                        new MongoService<ICategory>().DropCollection();
                        new MongoService<IItemSellingPrices>().DropCollection();
                        new MongoService<IItem>().DropCollection();
                        Run();
                        break;
                    case "Q":
                    case "q":
                        BrowserHelper.Close();
                        Run();
                        break;
                    case "R":
                    case "r":
                        _categories.ResumeLastCategory();
                        Run();
                        break;
                    case "S":
                    case "s":
                        BrowserHelper.Visible = true;
                        Run();
                        break;
                    default:
                        Run();
                        break;
                }
            }
            static void InitializeData()
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
            static void DisplayUI()
            {
                Console.WriteLine("Welcome to Tekkilor's Scraper!");
                Console.WriteLine("Please make a selection.");
                Console.WriteLine("(A)utoRun from start.");
                Console.WriteLine("(D)rop data.");
                Console.WriteLine("Show (I)tems.");
                Console.WriteLine("Show (C)ategories.");
                Console.WriteLine("(Q)uit");
                Console.WriteLine("(R)esume");
                Console.WriteLine("(S)how IE Browser");
            }
        }
    }
    }
}