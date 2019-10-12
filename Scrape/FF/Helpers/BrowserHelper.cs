#nullable enable
using System;
using Com.DOM.Extensions;
using Data.Mongo.Interfaces;
using MSHTML;
using Retail;
using Retail.Interfaces;
using SHDocVw;

namespace Scrape.FF.Helpers
{
    public class BrowserHelper
    {
        private const string _baseUrl = @"https://www.frysfood.com";
        //private const string _searchAllBtn = @"button.kds-SolitarySearch-button";
        private const string _searchAllUrl = @"https://www.frysfood.com/search?query=&searchType=natural";
        private const string _categoryMenuToggle =
            @"section.CollapsibleFacetContainer--Categories > a.CollapsibleFacetContainer-collapseLink";

        private static InternetExplorer? _ie;
        private static readonly Uri _baseUri = new Uri(_baseUrl);
        private static BrowserHelper? _instance;
        private BrowserHelper()
        {
        }
        public static BrowserHelper Instance()
        {
            if (_instance == null)
                _instance = new BrowserHelper();
            return
                _instance;

        }
        public static InternetExplorer IE()
        {
            if (_ie == null)
                _ie = new InternetExplorer();
            return
                _ie;
        }
        public static Uri Url => _baseUri;
        public static bool Visible
        {
            get =>
                IE().Visible;
            set =>
                IE().Visible =
                    value;
        }
        public static HTMLDocument? Doc => IE().Doc();
        public static bool Reset()
        {
            return
                IE().Go(
                    _searchAllUrl)
                    ?.Query(
                        _categoryMenuToggle)
                    ?.Click()
                ?? false;
        }
        public static bool Close()
        {
            while (_ie != null)
                _ie.Quit();
            return
                _ie == null
                    ? true
                : false;
        }
    }
}
