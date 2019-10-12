#nullable enable
using MSHTML;
using System.Collections.Generic;
using System.Linq;

namespace Com.DOM.Extensions
{
    public static class IHTMLDOMNodeExtensions
    {
    public static IEnumerable<string>? ClassList(
     this IHTMLDOMNode? n)
        {
            return
            n != null
                ? n.TryElement(
                    out IHTMLElement? e)
                    ? e?.ClassList()
                : null
            : null;
        }
        public static IHTMLElement? Element(
            this IHTMLDOMNode? n)
        {
            return
                n != null
                ? (n as IHTMLElement)
                    != null
                    ? (IHTMLElement?)
                        n != null
                        ? (IHTMLElement)n
                            is IHTMLElement
                            ? (IHTMLElement)n
                        : null
                    : null
                : null
            : null;
        }
        public static IHTMLElement2? Element2(
           this IHTMLDOMNode? n)
        {
            return
                n != null
                ? (n as IHTMLElement2)
                    != null
                    ? (IHTMLElement2?)
                        n != null
                        ? (IHTMLElement2)n
                            is IHTMLElement2
                            ? (IHTMLElement2)n
                        : null
                    : null
                : null
            : null;
        }
        public static IHTMLElement3? Element3(
           this IHTMLDOMNode? n)
        {
            return
                n != null
                ? (n as IHTMLElement3)
                    != null
                    ? (IHTMLElement3?)
                        n != null
                        ? (IHTMLElement3)n
                            is IHTMLElement3
                            ? (IHTMLElement3)n
                        : null
                    : null
                : null
            : null;
        }
        public static IHTMLElement4? Element4(
           this IHTMLDOMNode? n)
        {
            return
                n != null
                ? (n as IHTMLElement4)
                    != null
                    ? (IHTMLElement4?)
                        n != null
                        ? (IHTMLElement4)n
                            is IHTMLElement4
                            ? (IHTMLElement4)n
                        : null
                    : null
                : null
            : null;
        }
        public static IHTMLElement5? Element5(
           this IHTMLDOMNode? n)
        {
            return
                n != null
                ? (n as IHTMLElement5)
                    != null
                    ? (IHTMLElement5?)
                        n != null
                        ? (IHTMLElement5)n
                            is IHTMLElement5
                            ? (IHTMLElement5)n
                        : null
                    : null
                : null
            : null;
        }
        public static IHTMLElement6? Element6(
           this IHTMLDOMNode? n)
        {
            return
                n != null
                ? (n as IHTMLElement6)
                    != null
                    ? (IHTMLElement6?)
                        n != null
                        ? (IHTMLElement6)n
                            is IHTMLElement6
                            ? (IHTMLElement6)n
                        : null
                    : null
                : null
            : null;
        }
        public static IHTMLElement7? Element7(
           this IHTMLDOMNode? n)
        {
            return
                n != null
                ? (n as IHTMLElement7)
                    != null
                    ? (IHTMLElement7?)
                        n != null
                        ? (IHTMLElement7)n
                            is IHTMLElement7
                            ? (IHTMLElement7)n
                        : null
                    : null
                : null
            : null;
        }
        public static string? HTML(
            this IHTMLDOMNode? n)
        {
            return
                n?.Element()
                ?.innerHTML;
        }
        public static bool IsClickable(
            this IHTMLDOMNode? n)
        {
            return
                n
                ?.Element()
                ?.IsClickable()
                ?? false;
        }
        public static string? Tag(
            this IHTMLDOMNode? n)
        {
            return
                n?.Element()
                ?.tagName;
        }
        public static bool TryElement(
            this IHTMLDOMNode? n
            , out IHTMLElement? result)
        {
            result =
            n?.Element();
            return
                result != null
                ? result is IHTMLElement
                    ? true
                : false
            : false;
        }

        public static bool TryElement(
           this IHTMLDOMNode? n
           , out IHTMLElement2? result)
        {
            result =
                n?.Element2();
            return
                result != null
                ? result is IHTMLElement2
                    ? true
                : false
            : false;
        }

        public static bool TryElement(
           this IHTMLDOMNode? n
           , out IHTMLElement3? result)
        {
            result =
                n?.Element3();
            return
                result != null
                ? result is IHTMLElement3
                    ? true
                : false
            : false;
        }

        public static bool TryElement(
           this IHTMLDOMNode? n
           , out IHTMLElement4? result)
        {
            result =
                n?.Element4();
            return
                result != null
                ? result is IHTMLElement4
                    ? true
                : false
            : false;
        }

        public static bool TryElement(
           this IHTMLDOMNode? n
           , out IHTMLElement5? result)
        {
            result =
                n?.Element5();
            return
                result != null
                ? result is IHTMLElement5
                    ? true
                : false
            : false;
        }

        public static bool TryElement(
           this IHTMLDOMNode? n
           , out IHTMLElement6? result)
        {
            result =
                n?.Element6();
            return
                result != null
                ? result is IHTMLElement6
                    ? true
                : false
            : false;
        }

        public static bool TryElement(
           this IHTMLDOMNode? n
           , out IHTMLElement7? result)
        {
            result =
                n?.Element7();
            return
                result != null
                ? result is IHTMLElement7
                    ? true
                : false
            : false;
        }
    }
}
