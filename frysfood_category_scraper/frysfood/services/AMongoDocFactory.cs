#nullable enable
using System;
using data.mongodb.interfaces;
using MSHTML;
using SHDocVw;
using System.Linq;
using System.Collections.Generic;

namespace scraper.frysfood.services
{
    public abstract class AMongoDocFactory
    {
        public static string? GetInnerText(dynamic? d)
        {
            return d != null ? ((IHTMLElement)d).innerText : null;
        }
        public static dynamic? GetAttribute(IHTMLElement6? e, string v)
        {
            return e != null ? ((IHTMLElement5)e).attributes.getNamedItem(v)
                : throw new ArgumentNullException(nameof(e));
        }
        public static dynamic? GetAttributeValue(dynamic? d)
        {
            return d != null ? ((IHTMLDOMAttribute)d)?.nodeValue : throw new ArgumentNullException(nameof(d));
        }
        public static dynamic? GetAttributeValue(IHTMLElement6? e, string v)
        {
            return e != null ? GetAttributeValue(GetAttribute(e, v))
                : throw new ArgumentNullException(nameof(e));
        }
        public static IHTMLElement6? GetElementByClass(IHTMLElement6? e, string v)
        {
            return e != null ? (IHTMLElement6?)e.getElementsByClassName(v)
                .Cast<IHTMLElement6>().FirstOrDefault<IHTMLElement6>()
                : throw new ArgumentNullException(nameof(e));
        }
        public static IHTMLElement6? GetElementByTag(IHTMLElement6? e, string v)
        {
            return e != null ? (IHTMLElement6?)e.getElementsByTagNameNS("*", v).GetEnumerator().Current
                : throw new ArgumentNullException(nameof(e));
        }
        public static Uri? GetUri(dynamic? d)
        {
            return d != null ? Uri.TryCreate(
                ((IHTMLDocument2)((IHTMLElement)d).document).url, UriKind.Absolute, out Uri? result) ? result : null
                : null;
        }

        public abstract IMongoDoc GetDoc(IHTMLDOMNode n);
    }
}
