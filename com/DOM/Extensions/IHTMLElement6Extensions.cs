#nullable enable
using MSHTML;

namespace Com.DOM.Extensions
{
    public static class IHTMLElement6Extensions
    {
        public static bool Element(
            this IHTMLElement6 e6,
            out IHTMLElement? eResult)
        {
            eResult
                = e6 != null
                ? (IHTMLElement?)e6 : null;
            return
                eResult != null
                ? eResult is IHTMLElement
                ? true : false : false;
        }
    }
}
