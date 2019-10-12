#nullable enable
using Com.DOM.Interfaces;
using Data.Mongo.Interfaces;
using MSHTML;
using SHDocVw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Utility.Extensions;
using System.Globalization;

namespace Com.DOM.Extensions
{
    public static class IHTMLElementExtensions  
    {
        public static IHTMLAttributeCollection3? Attributes(
            this IHTMLElement? e)
        {
            return
                e != null
                    ? e.Element5()
                        ?.attributes ?? null
                    : null;
        }
        public static IHTMLDOMAttribute? Attribute(
            this IHTMLElement? e
            , string? name)
        {
            return
                !string.IsNullOrEmpty(
                    name)
                    ? e?.Attributes()
                    ?.getNamedItem(
                        name)
                    ?? null
                : null;
        }
        public static string AttributeValue(
            this IHTMLElement? e
            , string? name)
        {
            return
                !string.IsNullOrEmpty(
                    name)
                    ? e?.Attribute(
                        name)
                        ?.nodeValue
                        .ToString()
                    ?? string.Empty
                : string.Empty;
        }
        public static IWebBrowser2? Browser(
            this IHTMLElement? e)
        {
            return
            (from
                IWebBrowser2 browser
            in
                 new ShellWindows()
             select
                 browser)
            .Where(
                browser =>
                    browser.Doc()?
                        .DocEquals(
                            (HTMLDocument?)
                            e?.document)
                        ?? false)
                   ?.FirstOrDefault();
        }
        public static IEnumerable<IHTMLElement>? Children(
            this IHTMLElement? e)
        {
            return
            e != null
                ? e.children != null
                    ? (IEnumerable<object>?)
                        e.children != null
                        ? (e as IEnumerable<object>)
                        .Cast<IHTMLElement>()
                        .ToList()
                    : null
                : null
            : null;
        }
        public static IEnumerable<string>? ClassList(
            this IHTMLElement? e)
        {

            return
            e?.className?.Split(
                ' ', StringSplitOptions
                .RemoveEmptyEntries)
            .Where(
                n => !string.IsNullOrEmpty(n))
            .Distinct()
            .Select<string, string>(
                n => n.Trim())
            .ToList();
        }        
        public static IEnumerable<IHTMLElement>? ClassFamily(
            this IHTMLElement? e
            , bool any)
        {
            if (e == null)
                return null;
            else if (any)
            {
                List<IHTMLElement> list =
                    new List<IHTMLElement>();
                (from string name
                 in e.ClassList()
                 where e.ClassList() != null
                 where e.ClassList().Any()
                 select name)
                 .Select(
                    n =>
                        e.ClassFamily(
                            n))
                 ?.Where(
                     f => f != null)
                 .Where(
                     f => f.Any())
                 .ToList()
                 .ForEach(
                     delegate (
                        IEnumerable
                        <IHTMLElement>? elements)
                     {
                         if (elements != null
                         && elements.Any())
                             elements.Union(list);
                     });
                return list;
            }
            else
            {
                return
                    e?.ClassFamily(
                        e.className);
            }
        }
        public static IEnumerable<IHTMLElement>? ClassFamily(
           this IHTMLElement? e
            , string? name)
        {
            IHTMLElementCollection? col =
                !string.IsNullOrEmpty(
                    name)
                    ? e?.ClassFamilyCollection(name)
                    ?? null
                : null;
            return
                col != null
                ? col.length > 0
                    ? col.Cast<IHTMLElement>()
                    .ToList()
                : null
            : null;
        }
        public static IHTMLElementCollection? ClassFamilyCollection(
            this IHTMLElement? e
            , string? name)
        {
            return
            !string.IsNullOrEmpty(
                name)
                ? e?.Element6()
                ?.getElementsByClassName(
                    name)
                ?? null
            : null;
        }
        public static IHTMLElement? ClassFamilyFirst(
            this IHTMLElement? e
            , string? name)
        {
            IEnumerable<IHTMLElement>? family =
                !string.IsNullOrEmpty(
                    name)
                    ? e?.ClassFamily(name)
                    ?? null
                : null;
            return
                family != null
                    ? family.Any()
                        ? family.FirstOrDefault()
                    : null
                : null;
        }
        public static bool Click(
            this IHTMLElement? e)
        {
            if (e?.IsClickable() ?? false)
            {
                e?.click();
                return true;
            }
            else
            {
                IHTMLDOMNode? root =
                    e?.Node();
                IHTMLDOMNode? node =
                    root;                
                bool result = false;
                while (node != null)
                {
                    while (node != null
                            && !result
                            && node.hasChildNodes())
                    {
                        node = node.firstChild;
                        if (node.IsClickable())
                        {
                            node.Element()?.click();
                            return true;
                        }
                    }
                    while (node != null
                                && node != root)
                    {
                        IHTMLDOMNode? sibling =
                           node.nextSibling;
                        if (sibling != null)
                        {
                            node = sibling;
                            break;
                        }
                        node = node.parentNode;
                    }
                    if (node == root)
                        break;
                    if (node.IsClickable())
                    {
                        node.Element()?.click();
                        return true;
                    }
                }
                return false;
            }
        }
        public static HTMLDocument? Doc(
            this IHTMLElement? e)
        {
            return
            (e?.document as HTMLDocument ?? null)
            .TryBrowser(
                out IWebBrowser2? ie)
                ? ie?.Doc()
                ?? (HTMLDocument?)e?.document
                : (HTMLDocument?)e?.document;
        }
        public static IHTMLElement2? Element2(
                    this IHTMLElement? e)
        {
            return
                e != null
                ? (e as IHTMLElement2)
                    != null
                    ? (IHTMLElement2?)
                        e != null
                        ? (IHTMLElement2)e
                            is IHTMLElement2
                            ? (IHTMLElement2)e
                        : null
                    : null
                : null
            : null;
        }
        public static IHTMLElement3? Element3(
           this IHTMLElement? e)
        {
            return
                e != null
                ? (e as IHTMLElement3)
                    != null
                    ? (IHTMLElement3?)
                        e != null
                        ? (IHTMLElement3)e
                            is IHTMLElement3
                            ? (IHTMLElement3)e
                        : null
                    : null
                : null
            : null;
        }
        public static IHTMLElement4? Element4(
           this IHTMLElement? e)
        {
            return
                e != null
                ? (e as IHTMLElement4)
                    != null
                    ? (IHTMLElement4?)
                        e != null
                        ? (IHTMLElement4)e
                            is IHTMLElement4
                            ? (IHTMLElement4)e
                        : null
                    : null
                : null
            : null;
        }
        public static IHTMLElement5? Element5(
           this IHTMLElement? e)
        {
            return
                e != null
                ? (e as IHTMLElement5)
                    != null
                    ? (IHTMLElement5?)
                        e != null
                        ? (IHTMLElement5)e
                            is IHTMLElement5
                            ? (IHTMLElement5)e
                        : null
                    : null
                : null
            : null;
        }
        public static IHTMLElement6? Element6(
           this IHTMLElement? e)
        {
            return
                e != null
                ? (e as IHTMLElement6)
                    != null
                    ? (IHTMLElement6?)
                        e != null
                        ? (IHTMLElement6)e
                            is IHTMLElement6
                            ? (IHTMLElement6)e
                        : null
                    : null
                : null
            : null;
        }
        public static IHTMLElement7? Element7(
           this IHTMLElement? e)
        {
            return
                e != null
                ? (e as IHTMLElement7)
                    != null
                    ? (IHTMLElement7?)
                        e != null
                        ? (IHTMLElement7)e
                            is IHTMLElement7
                            ? (IHTMLElement7)e
                        : null
                    : null
                : null
            : null;
        }
        public static IHTMLElement? FirstChild(
            this IHTMLElement? e)
        {
            return
                e != null
                ? e.children != null
                    ? (IEnumerable<object>?)
                        e.children != null
                        ? ((IEnumerable<object>)
                            e.children)
                            .Cast<IHTMLElement>()
                            .ToList()
                            .FirstOrDefault()
                    : null
                : null
            : null;
        }
        public static Match? GetMatch(
            this IHTMLElement? e
            , string? pattern)
        {
            return
                e?.innerText.GetMatch(
                    pattern)
                ?? null;
        }
        public static MatchCollection? GetMatchCollection
            ( this IHTMLElement? e
            , string? pattern)
        {
            return
                e?.innerText.GetMatchCollection(
                    pattern)
                ?? null;
        }
        public static string GetMatchValue(
            this IHTMLElement? e
            , string? pattern
            , string? key)
        {
            return
                e?.innerText.GetMatchValue(
                    pattern
                    , key)
                ?? string.Empty;
        }
        public static IDictionary<string, string>? GetMatchValues(
            this IHTMLElement? e
            , string? pattern
            , IEnumerable<string>? keys)
        {
            return
                e?.innerText.GetMatchValues(
                    pattern
                    , keys)
                ?? null;
        }
        public static HTMLDocument? Go(
            this IHTMLElement? e)
        {
            return
                e?.Browser()
                ?.Go(e)
            ?? null;
        }
        public static bool HasChildren(
            this IHTMLElement? e)
        {
            return
                e != null
                    ? e.children != null
                        ? true
                    : false
                : false;
        }
        public static IHTMLInputElement? Input(
            this IHTMLElement? e)
        {
            return
                e != null
                ? (e as IHTMLInputElement)
                    != null
                    ? (IHTMLInputElement?)
                        e != null
                        ? (IHTMLInputElement)e
                            is IHTMLInputElement
                            ? (IHTMLInputElement)e
                        : null
                    : null
                : null
            : null;
        }
        public static string? InputValue(
            this IHTMLElement? e)
        {
            return e?.Input()?.value;
        }
        public static bool IsClickable(
            this IHTMLElement? e)
        {
            return
            e != null
                ? !(e.onclick is DBNull)
            : false;
        }
        public static IHTMLElement? LastChild(
           this IHTMLElement? e)
        {
            return
            e != null
                ? e.HasChildren()
                    ? e.Children().Last()
                : null
            : null;
        }
        public static string Link(
            this IHTMLElement? e)
        {
            return
                e?.Doc()?.Link()
                ?? string.Empty;
        }
        public static HTMLLocation? Location(
            this IHTMLElement? e)
        {
            return
                e?.Doc()?.Location();
        }
        public static bool Mongo
            <TOut>(
                this IHTMLElement? e
                , IMongoFactory
                    <IHTMLElement, TOut>
                        factory
                , out IDictionary
                    <IHTMLElement, TOut?>?
                        output)
            where TOut: class
                , IMongoDoc<TOut>
        {
            output =
                    factory?.Make(e);
            return
                    output?.Any()
                        ?? false    
                    ? true
                : false;
        }
        public static IHTMLElement? NextSibling
            (this IHTMLElement? e)
        {
            return
            e != null
                ? e.TryNode(
                    out IHTMLDOMNode? node)
                    ? node?.nextSibling != null
                        ? node?.nextSibling?.Element()
                    : null
                : null
            : null;
        }
        public static IHTMLDOMNode? Node(
           this IHTMLElement? e)
        {
            return
                e != null
                ? (e as IHTMLDOMNode)
                    != null
                    ? (IHTMLDOMNode?)
                        e != null
                        ? (IHTMLDOMNode)e
                            is IHTMLDOMNode
                            ? (IHTMLDOMNode)e
                        : null
                    : null
                : null
            : null;
        }
        public static IHTMLDOMNode2? Node2(
           this IHTMLElement? e)
        {
            return
                e != null
                ? (e as IHTMLDOMNode2)
                    != null
                    ? (IHTMLDOMNode2?)
                        e != null
                        ? (IHTMLDOMNode2)e
                            is IHTMLDOMNode2
                            ? (IHTMLDOMNode2)e
                        : null
                    : null
                : null
            : null;
        }
        public static IHTMLDOMNode3? Node3(
           this IHTMLElement? e)
        {
            return
                e != null
                ? (e as IHTMLDOMNode3)
                    != null
                    ? (IHTMLDOMNode3?)
                        e != null
                        ? (IHTMLDOMNode3)e
                            is IHTMLDOMNode3
                            ? (IHTMLDOMNode3)e
                        : null
                    : null
                : null
            : null;
        }
        public static int? NodeType(
            this IHTMLElement? e)
        {
            return
            e != null
                ? e.TryNode(
                    out IHTMLDOMNode? node)
                    ? node?.nodeType != null
                        ? node?.nodeType
                    : null
                : null
            : null;
        }
        public static IHTMLElement? Parent(
            this IHTMLElement? e)
        {
            return
            e != null
                ? e.TryNode(
                    out IHTMLDOMNode? node)
                    ? node != null
                        ? node.parentNode != null
                            ? node.parentNode.TryElement(
                                out IHTMLElement? element)
                                ? element
                            : null
                        : null
                    : null
                : null
            : null;
        }
        public static IHTMLElement? PreviousSibling(
            this IHTMLElement? e)
        {
            return
            e != null
                ? e.TryNode(
                    out IHTMLDOMNode? node)
                    ? node?.nextSibling != null
                        ? node?.nextSibling?.Element()
                    : null
                : null
            : null;
        }
        public static IEnumerable<IHTMLElement>? TagFamily(
            this IHTMLElement? e)
        {
            return
                e?.TagFamily(
                    e.tagName);
        }
        public static IEnumerable<IHTMLElement>? TagFamily(
            this IHTMLElement? e
            , string? name)
        {
            IHTMLElementCollection ? col =
                e != null
                ? !string.IsNullOrEmpty(
                    name)
                    ? e.TagFamilyCollection(
                        name)
                : null
            : null;
            return
                col?.Cast
                <IHTMLElement>()
                .AsEnumerable();
        }
        public static IHTMLElementCollection? TagFamilyCollection(
            this IHTMLElement? e
            , string? name)
        {
            return
            e != null
               ? !string.IsNullOrEmpty(
                   name)
                   ? e.TryElement(
                       out IHTMLElement2? e2)
                        ? e2?.getElementsByTagName(
                            name)
                    : null
                : null
            : null;
        }
        public static IHTMLElement? TagFamilyFirst(
            this IHTMLElement? e
            , string? name)
        {
            IEnumerable<IHTMLElement>?
            family =
                e != null
                ? !string.IsNullOrEmpty(
                    name)
                    ? e.TagFamily(
                        name)
                    : null
                : null;
            return
                family != null
                ? family.Any()
                    ? family.FirstOrDefault()
                : null
            : null;
        }
        public static bool TryAttributes(
            this IHTMLElement? e
            , out IHTMLAttributeCollection3? result)
        {

            result =
                e?.Attributes();
            return
                result != null
                ? true
            : false;
        }
        public static bool TryAttributeValue(
            this IHTMLElement? e
            , string? name
            , out string result)
        {
            result =
                e != null
                ? !string.IsNullOrEmpty(
                    name)
                    ? e.AttributeValue(
                        name)
                    : string.Empty
                : string.Empty;
            return
            !string.IsNullOrEmpty(
                result)
                ? true
            : false;
        }
        public static bool TryBrowser(
            this IHTMLElement? e
            , out IWebBrowser2? result)
        {
            result =
                e?.Browser();
            return
            result != null
                ? result is IWebBrowser2
                    ? true
                : false
            : false;
        }
        public static bool TryClassFamily(
            this IHTMLElement? e
            , bool any
            , out IEnumerable
                <IHTMLElement>?
                    result)
        {
            result =
                e?.ClassFamily(any);
            return
            result != null
                ? result.Any()
                    ? true
                : false
            : false;
        }
        public static bool TryClassFamily(
            this IHTMLElement? e
            , string? name
            , out IHTMLElement? result)
        {
            result =
                e != null
                    ? !string.IsNullOrEmpty(
                        name)
                        ? e.ClassFamilyFirst(
                            name)
                    : null
                : null;

            return
                result != null
                    ? true
                : false;
            
        }
        public static bool TryClassFamily(
            this IHTMLElement? e
            , string? name
            , out IHTMLElementCollection? result)
        {
            result =
                e != null
                    ? !string.IsNullOrEmpty(name)
                        ? e.ClassFamilyCollection(name)
                    : null
                : null;
            return
                result != null
                    ? true
                : false;
        }
        public static bool TryClassFamily(
           this IHTMLElement? e
            , string? name
            , out IEnumerable
                <IHTMLElement>?
                    result)
        {
            result =
                e?.ClassFamily(
                    name);
            return
            result != null
                ? result.Any()
                    ? true
                : false
            : false;
        }
        public static bool TryElement(
            this IHTMLElement? e
            , out IHTMLElement2? result)
        {
            result =
                e?.Element2();
            return
                result != null
                ? result is IHTMLElement2
                    ? true
                : false
            : false;
        }

        public static bool TryElement(
           this IHTMLElement? e
           , out IHTMLElement3? result)
        {
            result =
                e?.Element3();
            return
                result != null
                ? result is IHTMLElement3
                    ? true
                : false
            : false;
        }

        public static bool TryElement(
           this IHTMLElement? e
           , out IHTMLElement4? result)
        {
            result =
                e?.Element4();
            return
                result != null
                ? result is IHTMLElement4
                    ? true
                : false
            : false;
        }

        public static bool TryElement(
           this IHTMLElement? e
           , out IHTMLElement5? result)
        {
            result =
                e?.Element5();
            return
                result != null
                ? result is IHTMLElement5
                    ? true
                : false
            : false;
        }

        public static bool TryElement(
           this IHTMLElement? e
           , out IHTMLElement6? result)
        {
            result =
                e?.Element6();
            return
                result != null
                ? result is IHTMLElement6
                    ? true
                : false
            : false;
        }

        public static bool TryElement(
           this IHTMLElement? e
           , out IHTMLElement7? result)
        {
            result =
                e?.Element7();
            return
                result != null
                ? result is IHTMLElement7
                    ? true
                : false
            : false;
        }
        public static bool TryFirstChild(
            this IHTMLElement? e
            , out IHTMLElement?
                result)
        {
            result =
                e?.FirstChild();
            return
                result != null
                ? true
            : false;
        }
        public static bool TryInput(
            this IHTMLElement? e
            , out IHTMLInputElement?
                result)
        {
            result =
                e?.Input();
            return
                result != null
                ? result is IHTMLInputElement
                    ? true
                : false
            : false;
        }
        public static bool TryNav(
            this IHTMLElement? e
            , out HTMLDocument?
                result)
        {
            IWebBrowser2? browser =
                e?.Browser();
            result =
            e != null
                ? browser?.Go(e)
            : null;
            return
            result != null
                ? true
            : false;
        }
        public static bool TryNextSibling(
           this IHTMLElement? e
            , out IHTMLElement?
                result)
        {
            result =
                e?.NextSibling();
            return
                result != null
                ? true
            : false;
        }
        public static bool TryNode(
            this IHTMLElement? e
            , out IHTMLDOMNode? result)
        {
            result =
                e?.Node();
            return
                result != null
                ? result is IHTMLDOMNode
                    ? true
                : false
            : false;
        }

        public static bool TryNode(
           this IHTMLElement? e
           , out IHTMLDOMNode2? result)
        {
            result =
                e?.Node2();
            return
                result != null
                ? result is IHTMLDOMNode2
                    ? true
                : false
            : false;
        }
        public static bool TryNode(
           this IHTMLElement? e
           , out IHTMLDOMNode3? result)
        {
            result =
                e?.Node3();
            return
                result != null
                ? result is IHTMLDOMNode3
                    ? true
                : false
            : false;
        }
        public static bool TryMatch(
           this IHTMLElement? e
            , string? pattern
            , out Match?
                result)
        {
            result =
                e?.GetMatch(
                    pattern)
                ?? null;
            return
                result?.Success
                ?? false;
        }
        public static bool TryMatch(
           this IHTMLElement? e
            , string? pattern
            , out MatchCollection? result)
        {
            result =
                e?.GetMatchCollection(
                    pattern)
                ?? null;
            return
                result?[0]?.Success
                ?? false;
        }
        public static bool TryMatch(
           this IHTMLElement? e
            , string? pattern
            , string? key
            , out string
                result)
        {
            result =
                e.GetMatchValue(
                    pattern
                    , key);                
            return
                !string.IsNullOrEmpty(
                    result)
                    ? true
                : false;
        }
        public static bool TryMatch(
            this IHTMLElement? e
            , string? pattern
            , IEnumerable
                <string>? keys
            , out IDictionary
                <string, string>?
                    result)
        {
            result =
                e?.GetMatchValues(
                    pattern
                    , keys)
                ?? null;
            return
                result != null
                ? true
            : false;
        }
        public static bool TryParent(
            this IHTMLElement? e
            , out IHTMLElement? result)
        {
            result =
                e?.Parent();
            return
            result != null
                ? true
            : false;
        }
        public static bool TryTagFamily(
            this IHTMLElement? e
            , string? name
            , out IHTMLElement? result)
        {
            result =
                e != null
                ? !string.IsNullOrEmpty(
                    name)
                    ? e.TagFamilyFirst(
                        name)
                : null
            : null;
            return
                result != null
                ? true
            : false;
        }
        public static bool TryTagFamily(
            this IHTMLElement? e
            , string? name
            , out IEnumerable
                <IHTMLElement>?
                    result)
        {
            result =
                e != null
                ? !string.IsNullOrEmpty(
                    name)
                    ? e?.TagFamily(
                        name)
                : null
            : null;
            return
            result != null
                ? result.Any()
                    ? true
                : false
            : false;
        }
        public static bool TryTagFamily(
           this IHTMLElement? e
            , out IEnumerable
                <IHTMLElement>?
                    result)
        {
            result =
                 e?.TagFamily(
                     e.tagName);
            return
            result != null
                ? result.Any()
                    ? true
                : false
            : false;
        }
        public static bool TryTagFamily(
            this IHTMLElement? e
            , string? name
            , out IHTMLElementCollection? result)
        {
            result =
                e != null
                ? !string.IsNullOrEmpty(
                    name)
                    ? e.TagFamilyCollection(
                        name)
                : null
            : null;
            return
                result != null
                ? true
            : false;
        }
        public static Uri? URL(
        this IHTMLElement? e)
        {
            return e?.Doc()?.URL();
        }
    }
}