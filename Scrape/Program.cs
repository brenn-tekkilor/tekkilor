using Retail.Interfaces;
using Scrape.FF.Helpers;
using System;

namespace Scrape
{
    internal class Program
    {
        private static void Main()
        {
            {
                MongoHelper.InitializeDb();
                _ = new ResourceHelper();
                _ = BrowserHelper.Instance();
                CategoryHelper categories =
                    new CategoryHelper();

                Console.WriteLine("Welcome to Tekkilor's Scraper!");
                Console.WriteLine("Please make a selection.");
                Console.WriteLine("(A)utoRun from start.");
                Console.WriteLine("(D)rop All Data.");
                Console.WriteLine("Show All (I)tems.");
                Console.WriteLine("Show All (C)ategories.");
                Console.WriteLine("(Q)uit");
                Console.WriteLine("(R)esume");
                Console.WriteLine("(S)how IE Browser");
                // Console.WriteLine("(R)esume the last scraping session.");
                string userSelection = Console.ReadLine();
                switch (userSelection)
                {
                    case "A":
                    case "a":
                        _ = BrowserHelper.Reset();
                        categories.Autorun(null);
                        Main();
                        break;
                    case "D":
                    case "d":
                        MongoHelper.DropCollections();
                        Main();
                        break;
                    case "I":
                    case "i":
                        MongoHelper.WriteAll<IItem>();
                        Main();
                        break;
                    case "C":
                    case "c":
                        MongoHelper.WriteAll<ICategory>();
                        Main();
                        break;
                    case "Q":
                    case "q":
                        BrowserHelper.Close();
                        Main();
                        break;
                    case "R":
                    case "r":
                        categories.ResumeLastCategory();
                        Main();
                        break;
                    case "S":
                    case "s":
                        BrowserHelper.Visible = true;
                        Main();
                        break;
                    default:
                        Main();
                        break;
                }
            }
        }
    }
}