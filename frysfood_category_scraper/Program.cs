#nullable enable
using System;
using System.Threading;
using System.Collections.Generic;
using scraper.frysfood.api.model;
using scraper.frysfood.app;
using data.mongodb;
using data.mongodb.interfaces;
using data.mongodb.extensions;
using DotNetEnv;
namespace scraper.frysfood
{
    class Program
    {
        static void Main()
        {
                WebScraper scraper = new WebScraper();
                Console.WriteLine("Welcome to Tekkilor's Frys Scraper!");
                Console.WriteLine("Please make a selection.");
                Console.WriteLine("(S)tart a new scraping session.");
                Console.WriteLine("(R)esume the last scraping session.");
                Console.WriteLine("(T)est scraping session.");
                string userSelection = Console.ReadLine();
                switch (userSelection)
                {
                    case "S":
                    case "s":
                        scraper.StartNewScrape();
                        break;
                    case "R":
                    case "r":
                        scraper.ResumeScrape();
                        break;
                    case "T":
                    case "t":
                        WebScraper.TestScrape();
                        break;
                    default:
                        Main();
                        break;
            }
        }
    }
}
