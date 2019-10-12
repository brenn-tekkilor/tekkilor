#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Data.Mongo.Interfaces;
using MSHTML;
using SHDocVw;

namespace Com.DOM.Extensions
{
    public static class IWebBrowser2Extensions
    {
        private const object _empty = null;
        private static void Sleep(int ms)
        {
            System
                .Threading
                .Thread
                .Sleep(ms);
        }
        private static IWebBrowser2? Wait(
            this IWebBrowser2? b
            , int checkMs
            , int extraMs)
        {
            if (b != null)
            {
                do
                {
                    Sleep(checkMs);
                }
                while (b.Busy);
                Sleep(extraMs);
            }
            return b;
        }
        public static HTMLDocument? Doc(
            this IWebBrowser2? b)
        {
            return
                b
                ?.Doc(
                    100
                    , 1000);
        }
        public static HTMLDocument? Doc(
            this IWebBrowser2? b
            , int checkMs, int extraMs)
        {
            return
            (HTMLDocument?)
            b
            ?.Wait(
                checkMs
                , extraMs)
            ?.Document;
        }
        public static HTMLDocument? Go(
            this IWebBrowser2 b
            , Uri? link)
        {
            return
                b != null
                    ? link != null
                        ? !string.IsNullOrEmpty(
                            link.AbsoluteUri)
                            ? b.Go(link.AbsoluteUri)
                    : null
                : null
            : null;
        }
        public static HTMLDocument? Go(
            this IWebBrowser2? b
            , string? link)
        {
            b?.Navigate2(
                link
                , _empty, _empty
                , _empty, _empty);
            return
                b?.Doc();
        }
        public static HTMLDocument? Go(
            this IWebBrowser2? b
            , IHTMLElement? link)
        {
            string? startLink =
                b?.Doc().Link();
            string? endLink =
            link?.Click() ?? false
                ? b?.Doc().Link()
            : null;
            return
                !string.IsNullOrEmpty(
                    endLink)
                    ? !startLink
                    ?.Equals(
                        endLink
                        , StringComparison
                        .CurrentCultureIgnoreCase)
                    ?? false
                        ? b?.Doc()
                        ?? null
                : null
            : null;
        }
        public static bool TryGo(
            this IWebBrowser2? b
            , IHTMLElement? link
            , out HTMLDocument? result)
        {
            HTMLDocument? response =
                b?.Go(link) ?? null;
            result =
                response ?? b?.Doc();
            return
                response != null
                    ? true
                : false;
        }
        public static void Quit(
            this IWebBrowser2 b)
        {
            b?.Quit();
        }
        public static IHTMLElement? Query(
            this IWebBrowser2? b
            , string? query)
        {
            return
                b?.Doc()
                    ?.Query(
                        query)
                    ?? null;
        }
        public static IEnumerable<IHTMLElement>? QueryAll(
            this IWebBrowser2? b
            , string? query)
        {
            return
                b?.Doc()
                    ?.QueryAll(
                        query)
                ?? null;
        }
        public static void ScrapeManyPages<T>(
            this IWebBrowser2 b
            , string queryAll
            , IMongoFactory
                <IHTMLElement, T>
                factory
            , string queryLink)
            where T : class, IMongoDoc<T>
        {
            if (factory != null
                && b != null
                && !string.IsNullOrEmpty(
                    queryAll))
            {
                HTMLDocument? oldDoc;
                HTMLDocument? newDoc = null;
                do
                {
                    oldDoc = newDoc;

                    if (newDoc == null)
                        newDoc = b.Doc();
                    else
                    {
                        IHTMLElement? link =
                            newDoc.Query(
                                queryLink);
                        newDoc =
                            link != null
                                ? b.Go(link)
                            : newDoc;
                    }
                    newDoc?
                        .ScrapeMany<T>(
                            queryAll
                            , factory);
                }
                while (!newDoc
                                .DocEquals(
                                    oldDoc));
            }
        }
        public static void ScrapeHookManyPages<T>(
            this IWebBrowser2 b
            , string queryAll
            , IMongoFactory
                <IHTMLElement, T>
                factory
            , string nextPage
            , Action<T?> action)
        where T : class, IMongoDoc<T>
        {
            if (factory != null
                && b != null
                && !string.IsNullOrEmpty(
                    queryAll))
            {
                HTMLDocument? oldDoc;
                HTMLDocument? newDoc = null;
                do
                {
                    oldDoc = newDoc;

                    if (newDoc == null)
                        newDoc = b.Doc(
                            100, 1000);
                    else
                    {
                        IHTMLElement? link =
                            newDoc.Query(
                                nextPage);
                        newDoc =
                            link != null
                                ? b.Go(link)
                            : newDoc;
                    }
                    newDoc?
                        .ScrapeHookMany<T>(
                            queryAll
                            , factory
                            , action);
                }
                while (!newDoc
                                .DocEquals(
                                    oldDoc));
            }
        }


        public static void ScrapeList<TOut>(
            this IWebBrowser2? b
            , string? query
            , IMongoFactory<IHTMLElement, TOut>? factory
            , string? queryNext
            , Action<IHTMLElement?, TOut?>? hookOneAfter)
        where TOut : class, IMongoDoc<TOut>
        {
            if (b?.Doc() != null)
            {
                if (factory != null)
                {
                    factory =
                        factory.HookOneAfter(
                            hookOneAfter)
                        .PaginateList(
                            query
                            , b
                            , queryNext
                            , hookOneAfter);
                    factory?.Make(
                        b?.QueryAll(
                            query));
                }
            }
        }
        public static bool TryScrapeManyPages<T>(
            this IWebBrowser2 b
            , string queryAll
            , IMongoFactory
                <IHTMLElement, T>
                factory
            , string queryLink
            , out IDictionary
            <Uri
            , IDictionary
            <IHTMLElement
            , T?>?>? result)
        where T : class, IMongoDoc<T>
        {
            HTMLDocument?
                lastDoc = null;
            HTMLDocument?
                currentDoc =
                    b.TryDoc(
                        out HTMLDocument? d)
                        ? d : null;
            IDictionary<Uri, IDictionary
           <IHTMLElement, T?>?> work =
           new Dictionary<Uri, IDictionary
            <IHTMLElement, T?>?>();
            while (currentDoc != null
                && !currentDoc.DocEquals(
                lastDoc))
            {
                Uri? uri =
                    currentDoc.URL();
                if (uri == null
                    || work.ContainsKey(
                        uri))
                    break;
                work.Add(
                    uri
                    , currentDoc.TryScrapeMany<T>(
                       queryAll
                       , factory
                       , out IDictionary
                       <IHTMLElement, T?>?
                       scrape)
                    ? scrape : null);
                lastDoc =
                    currentDoc;
                currentDoc =
                    currentDoc.TryQuery(
                        queryLink
                        , out IHTMLElement?
                        link)
                    ? link != null
                        ? link.TryNav(
                            out HTMLDocument?
                                doc)
                            ? doc
                        : null
                    : null
                : null;

            }
            result =
                       work != null
                           ? work.Any()
                               ? work
                           : null
                       : null;
            return
                result != null
                    ? true
                : false;
        }
        public static bool TryDoc(
            this IWebBrowser2 b
            , out HTMLDocument? result)
        {
            result =
                b?.Doc();
            return
            b != null
                ? b is HTMLDocument
                    ? true
                : false
            : false;
        }
        public static bool TryWaitForDoc(
            this IWebBrowser2 b
            , int checkMs, int extraMs
            , out HTMLDocument? result)
        {
            result =
            b?.Doc(
                    checkMs
                    , extraMs);
            return
                result != null
                    ? true
                : false;
        }
    }
}