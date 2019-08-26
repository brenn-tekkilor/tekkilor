#nullable enable
using com.client.browser.iexplorer;
using com.client.dom;
using com.client.dom.interfaces;
using data.mongodb;
using data.mongodb.extensions;
using data.mongodb.interfaces;
using scraper.frysfood.api.model;
using scraper.frysfood.services;
using MSHTML;
using retail.inventory.grouping;
using System;
using System.Resources;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using utility.language;
using System.Threading;
using DotNetEnv;
using scraper.frysfood.app;
using System.Resources;
using System.Linq;
using SHDocVw;
using scraper.frysfood.api.model.schemas;

namespace scraper.frysfood
{
    public class WebScraper
    {
        private const string resourceFile = @".\Properties\Resources.resources";
        public Db Dbase { get; private set; }
        public NodeFilter? TreeWalkerFilterNode { get; set; }
        public NodeFilter? NodeIteratorFilterNode { get; set; }
        public WebScraper()
        {
            Env.Load();
            new CategoryMongoDocSchema().Init();
            new StockItemMongoDocSchema().Init();
            new ItemSellingPricesMongoDocSchema().Init();
            MongoDocExtensions.ColRouter = MongoCollectionRouter.GetInstance();
            Dbase = Db.GetInstance();
        }
        private static bool ScrapeItems(CategoryMongoDoc m)
        {
            using ResourceSet resources = new ResourceSet(resourceFile);
            string? qpc = resources.GetString(
                "_query_product_card_node");
            List<dynamic> r = qpc != null
                ? IE.GetInstance().QuerySelectorAll(qpc).Cast<dynamic>().ToList()
                    : throw new NullReferenceException();
            foreach (var d in from dynamic? i in r
                              let d = (StockItemMongoDoc)StockItemMongoDocFactory
                         .GetInstance().GetDoc((IHTMLDOMNode)i)
                              select d)
            {
                d.CategoryId = m.Id;
                
                Console.WriteLine("scraped item: "
                    + ((StockItemMongoDoc)d.SetCategoryId(m.Id).Save()).Name);
                }
            string? qpn = resources.GetString("_query_pagination_next_link");
            return qpn != null
                    ? IE.GetInstance().ClickSelector(qpn)
                    : throw new NullReferenceException();
            return false;
        }
        private void Scrape(CategoryMongoDoc? m, IHTMLDOMNode? n, com.client.dom.TreeWalker? t)
        {
            using System.Resources.ResourceSet resources = new ResourceSet(resourceFile);
            if (t == null)
            {
                string? qcr = resources.GetString(
                    "_query_categories_root_node");
                if (qcr != null)
                {
                    IHTMLElement? e = IE.GetInstance().QuerySelector(qcr);
                    if (e != null)
                    {
                        IHTMLDOMNode? r = IE.ToNode(e);
                        if (r != null)
                        {
                            t = new com.client.dom.TreeWalker(r, WhatToShow.Elements, TreeWalkerFilterNode = n =>
                            {
                                IHTMLElement? e = (IHTMLElement?)(IHTMLDOMNode?)n;
                                if (e != null)
                                {
                                    if (e.tagName == "LI")
                                    {
                                        if (e.className == "ProductsNavList-item is-top"
                                            || e.className == "ProductsNavList-item")
                                        {
                                            IE.GetInstance().Click(e);
                                            return NodeFilterResult.Accept;
                                        }
                                    }
                                }
                                return NodeFilterResult.Skip;
                            });
                        }
                    }
                }
                if (m != null && n == null)
                {
                    string? qsc = resources.GetString(
                        "_query_selected_category");
                    if (qsc != null)
                    {
                        IHTMLElement? e = IE.GetInstance().QuerySelector(qsc);
                        if (e != null)
                            n = IE.ToNode(e);
                        Scrape(m, n, t);
                    }
                }
            }
            if (t != null)
            {
                if (m == null && n == null)
                    Scrape(null, t.Root, t);
                else if (m == null && n != null)
                {
                    t.Current = n;
                    _ = t.ToFirst();
                    if (t.Current != null)
                    {
                        do
                        {
                            IHTMLDOMNode x = t.Current;
                            Scrape((CategoryMongoDoc)CategoryMongoDocFactory
                                .GetInstance().GetDoc(x).Save(), x, t);
                            t.Current = x;
                        }
                        while (t.ToNextSibling() != null);
                    }
                }
                else if (m != null && n != null)
                {
                    t.Current = n;
                    n = t.ToFirst();
                    while (n != null)
                    {
                        IHTMLDOMNode x = t.Current;
                        CategoryMongoDoc c = (CategoryMongoDoc)CategoryMongoDocFactory
                            .GetInstance().GetDoc(x);
                        m.AddChild(c);
                        c.SetParentId(m);
                        Scrape(c, x, t);
                        t.Current = x;
                        n = t.ToNextSibling();
                        if (n == null)
                        {
                                while(ScrapeItems(m))
                            {
                                continue;
                            }
                        }
                    }
                }
            }
        }
        public void StartNewScrape()
        {
            using System.Resources.ResourceSet resources = new ResourceSet(resourceFile);
            string? msg = resources.GetString(
                    "_msg_start_new");
            Console.WriteLine(msg);
            Db.NewCollection(new CategoryMongoDoc().GetColName());
            Db.NewCollection(new StockItemMongoDoc().GetColName());
            StartScrape();
        }
        public void StartScrape()
        {
            using System.Resources.ResourceSet resources = new ResourceSet(resourceFile);
            string? uh = resources.GetString(
                "_uri_home");
            string? qpsb = resources.GetString(
                "_query_product_search_btn");
            string? qecb = resources.GetString(
                    "_query_expand_categories_btn");
            if (uh != null && qpsb != null && qecb != null)
            {
                IE.GetInstance().Browse(uh
                    , new List<string?>()
                    {
                    qpsb,
                    qecb,
                    });
                Scrape(null, null, null);
            }
        }
        public void ResumeScrape()
        {
            using System.Resources.ResourceSet resources = new ResourceSet(resourceFile);
            Console.WriteLine(resources.GetString(
                    "_msg_resume"));
            CategoryMongoDoc lastDoc = (CategoryMongoDoc)Db.FindLast(new CategoryMongoDoc().GetCol());
            if (lastDoc != null)
            {
                if (lastDoc.Uri != null)
                    IE.GetInstance().Browse(lastDoc.Uri, new List<string?>()
                    {
                        resources.GetString(
                    "_query_expand_categories_btn"),
            });
                Scrape(lastDoc, null, null);
            }
        }
        public static void EndScrape()
        {
            IE.GetInstance().Quit();
        }
        public static void TestScrape()
        {
            if (Uri.TryCreate("file:///D:/frysfood.htm", UriKind.Absolute, out Uri? result))
                IE.GetInstance().Browse(result.AbsoluteUri, null);
            ScrapeItems((CategoryMongoDoc)new CategoryMongoDoc().Save());
        }
    }
}
