#nullable enable
using Data.Mongo.Interfaces;
using MSHTML;
using SHDocVw;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Com.DOM.Extensions
{
    public static class IMongoFactoryExtensions
    {
        public static IMongoFactory<TIn, TOut>? HookOneAfter<TIn, TOut>(
            this IMongoFactory<TIn, TOut>? f
            , Action<TIn?, TOut?>? hook)
        where TIn: class
        where TOut: class, IMongoDoc<TOut>
        {
            if (f != null
                && hook != null)
            {
                f.AfterOneEvent +=
                    ((TIn? input, TOut? output) evtArgs) =>
                        hook(evtArgs.input, evtArgs.output);
            }
            return f;
        }
        public static IMongoFactory<IHTMLElement, TOut>? PaginateList<TOut>(
             this IMongoFactory<IHTMLElement, TOut>? f
            , string? query
            , IWebBrowser2? browser
            , string? queryNext
            , Action<IHTMLElement?, TOut?>? hookOneAfter)
        where TOut : class, IMongoDoc<TOut>
        {
            if (f != null
                && !string.IsNullOrEmpty(
                    query))
            {
                f.AfterManyEvent +=
                    (IDictionary<IHTMLElement, TOut?>? evtArgs) =>
                    {
                        if (browser?.Go(
                            browser?.Query(
                                queryNext)) != null)
                        {
                            browser?.ScrapeList(
                                query
                                , (IMongoFactory<IHTMLElement, TOut>?)
                                f.GetType()
                                .GetConstructor(
                                    Array.Empty<Type>())
                                ?.Invoke(
                                    BindingFlags.CreateInstance
                                    , null, Array.Empty<object>()
                                    , CultureInfo.CurrentCulture)
                                , queryNext
                                , hookOneAfter);
                        }
                    };
                }
            return f;
        }
    }
}
