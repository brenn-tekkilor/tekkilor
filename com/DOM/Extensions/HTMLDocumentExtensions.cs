#nullable enable
using System;
using System.Linq;
using System.Collections.Generic;
using MSHTML;
using SHDocVw;
using Data.Mongo.Interfaces;
namespace Com.DOM.Extensions
{
    public static class HTMLDocumentExtensions
    {
        public static IWebBrowser2? Browser(
        this HTMLDocument? d)
        {
            IList<IWebBrowser2?> browsers =
            (from
                IWebBrowser2? browser
            in
                new ShellWindows()
             select
                 browser)
                .Where(
                    browser =>
                        browser.Document != null)
                .ToList();
            if (!string.IsNullOrEmpty(d?.url))
            {
                if (browsers?.Count > 1)
                {
                    IWebBrowser2? filtered =
                    (from
                        IWebBrowser2 ie
                    in
                         browsers
                     select
                     ie)
                     .Where(
                        ie =>
                            !string.IsNullOrEmpty(
                                ie.LocationURL))
                     .Where(
                        ie =>
                            ie.LocationURL.Equals(
                                d.url
                                , StringComparison
                                .CurrentCultureIgnoreCase))
                     .FirstOrDefault();
                    if (filtered != null)
                        return filtered;
                }
                if (!browsers
                    .FirstOrDefault()?
                    .LocationURL
                    .Equals(
                        d?.url
                        , StringComparison
                        .CurrentCultureIgnoreCase) ?? false)
                    browsers.FirstOrDefault()
                        ?.Go(d?.url);
            }
            return
                browsers.FirstOrDefault();
        }
        public static HTMLDocument? Click(
            this HTMLDocument? d
            , string? query)
        {
            return
            d.TryQuery(
                query
                , out IHTMLElement? e)
                ? e != null
                    ? e.TryNav(
                        out HTMLDocument? doc)
                        ? doc
                        ?? null
                    : null
                : null
            : null;
        }
        public static IEnumerable<HTMLDocument?>? Click(
            this HTMLDocument? d,
            IEnumerable<string?>? queries)
        {
            return
            queries != null
                ? queries
                .ToList()
                .Any()
                ? (from
                     string? q
                 in
                     queries
                   where
                   !string.IsNullOrEmpty(
                       q)
                   select
                   q)
                .Select(
                    query =>
                        d.TryClick(
                            query
                            , out HTMLDocument? doc)
                            ? doc
                        ?? null
                    : null)
                : null
            : null;
        }
        public static bool DocEquals(
            this HTMLDocument? d,
            HTMLDocument? other)
        {
            if (d == null || other == null)
                return false;
            return
                d?.Equals(other)
                ?? false
                    ? true
                : d.Link()
                ?.Equals(
                    other.Link()
                    , StringComparison
                    .CurrentCultureIgnoreCase)
                ?? false;
        }
        public static HTMLLocation? Location(
            this HTMLDocument? d)
        {
            return (HTMLLocation?)d?.location;
        }
        public static IHTMLElement? Query(
            this HTMLDocument? d
            , string? query)
        {
            return
                !string.IsNullOrEmpty(
                    query)
                    ? d?.querySelector(
                        query)
                    ?? null
                : null;
        }
        public static IEnumerable<IHTMLElement>? QueryAll(
            this HTMLDocument? d
            , string? query)
        {
            return
                !string
                .IsNullOrEmpty(query)
                    ? d?.querySelectorAll(
                    query)
                .Cast<IHTMLElement>()
                .ToList()
            : null;
        }
        public static void ScrapeMany<T>(
            this HTMLDocument? d
            , string queryAll
            , IMongoFactory
                <IHTMLElement, T>
                factory)
            where T : class, IMongoDoc<T>
        {
            if (factory != null)
            {
                IEnumerable
                <IHTMLElement>?
                    inputs =
                        d != null
                            ? !string.IsNullOrEmpty(
                                queryAll)
                                ? d.QueryAll(
                                    queryAll)
                            : null
                        : null;
                if (inputs != null
                    && inputs.Any())
                {
                    foreach (
                        IHTMLElement? i
                            in inputs)
                    {
                        _ = factory.Make(i);
                    }
                }
            }
        }
        public static void ScrapeHookMany<T>(
             this HTMLDocument? d
            , string queryAll
            , IMongoFactory
                <IHTMLElement, T>
            factory
            , Action<T?> action)
        where T : class, IMongoDoc<T>
        {
            if (factory != null
                && d != null
                && !string.IsNullOrEmpty(
                    queryAll))
            {
                IEnumerable
                    <IHTMLElement>?
                    inputs = d.QueryAll(
                                queryAll);
                if (inputs != null
                && inputs.Any())
                {
                    IDictionary<IHTMLElement, T?>? dict =
                            factory.Make(inputs);
                    if (dict != null
                        && dict.Any())
                    {
                        foreach (
                            KeyValuePair
                            <IHTMLElement, T?> kvp
                            in dict)
                        {
                            action(kvp.Value);
                        }
                    }
                }
            }
        }
        public static IEnumerable<T>? GetScrapeHookMany<T>(
            this HTMLDocument? d
            , string queryAll
            , IMongoFactory
                <IHTMLElement, T>
            factory
            , Func<T, T> func)
        where T : class, IMongoDoc<T>
        {
            IEnumerable
                <IHTMLElement?>?
                inputs =
                    d?.QueryAll(
                            queryAll);
            return
                inputs != null
                    ? inputs.Any()
                        ? factory != null
                            ?
                    (from
                        IHTMLElement? input
                    in
                         inputs
                     where
                        input != null
                     select
                         input)
                    .Select(
                        input =>
                            factory.Make(
                                input))
                    .Where(
                        dict =>
                            dict != null)
                    .Where(
                        dict =>
                            dict.Any())
                    .Select(
                        dict =>
                            dict?.Values)
                    .Where(
                        doc =>
                            doc != null)
                    .Cast<T>()
                    .Select(
                        doc =>
                            func(doc))
                    .AsEnumerable()
                    : null
                : null
            : null;
    }
        public static bool TryScrapeMany<T>(
            this HTMLDocument? d
            , string queryAll
            , IMongoFactory
                <IHTMLElement, T>
                factory
            , out IDictionary
                <IHTMLElement, T?>?
                result)
            where T: class, IMongoDoc<T>
        {
            result =
                d != null
                    ? string.IsNullOrEmpty(
                        queryAll)
                        ? factory?.Make(
                                    d.TryQueryAll(
                                        queryAll
                                        , out IEnumerable
                                        <IHTMLElement>?
                                        docs)
                                        ? docs
                                    : null)
                        : null
                    : null;
            return
                result != null
                    ? result.Any()
                        ? true
                    : false
                : false;
        }
        public static bool TryBrowser(
            this HTMLDocument? d,
            out IWebBrowser2? result)
        {
            result =
                d?.Browser();
            return
            result != null
                ? result is IWebBrowser2
                    ? true
                : false
            : false;
        }
        public static bool TryClick(
            this HTMLDocument? d
            , string? query
            , out HTMLDocument? result)
        {
            result =
                d?.Click(query);
            return
            result != null
                ? true
            : false;
        }
        public static bool TryClick(
            this HTMLDocument? d,
            IEnumerable<string?>? queries
            , out IEnumerable
            <HTMLDocument?>? result)
        {
            result =
                d?.Click(
                    queries);
            return
            result != null
                ? result.Any()
                    ? true
                : false
            : false;
        }
        public static bool TryQuery(
            this HTMLDocument? d
            , string? query
            , out IHTMLElement? result)
        {
            result =
             d?.Query(
                 query);
            return result != null
                ? true
            : false;
        }
        public static bool TryQueryAll(
            this HTMLDocument? d
            , string? query
            , out IEnumerable<IHTMLElement>? result)
        {
            result =
                d?.QueryAll(query);
            return
            result != null
                ? result.Any()
                    ? true
                : false
            : false;
        }
        public static Uri? URL(
            this HTMLDocument? d)
        {
            return
                d != null
                    ? Uri
                    .TryCreate(
                        d.url
                        , UriKind.RelativeOrAbsolute
                        , out Uri?
                            urlResult)
                    ? urlResult ?? (Uri
                    .TryCreate(
                        d.location.href
                        , UriKind.RelativeOrAbsolute
                        , out Uri?
                            hrefResult)
                    ? hrefResult != null
                        ? hrefResult
                            .IsAbsoluteUri
                            ? hrefResult
                        : Uri
                            .TryCreate(
                                d.baseUrl
                                , UriKind.Absolute
                                , out Uri?
                                baseResult)
                            ? baseResult
                                .IsAbsoluteUri
                                ? System.Uri
                                    .TryCreate(
                                        baseResult
                                        , hrefResult
                                        , out Uri?
                                            result)
                                    ? result
                                    ?? null
                                    : null
                                : null
                            : null
                        : null
                    : null)
                : null
            : null;
        }
        public static string? Link(
            this HTMLDocument? d)
        {
            return d?.url ?? string.Empty;
        }
    }
}
