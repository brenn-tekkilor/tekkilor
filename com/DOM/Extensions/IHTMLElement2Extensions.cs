# nullable enable
using MSHTML;

namespace Com.DOM.Extensions
{
    public static class IHTMLElement2Extensions
    {
        public static bool Element(
            this IHTMLElement2 e2
            , out IHTMLElement? eResult)
        {
            eResult
                = e2 != null
                ? (IHTMLElement?)e2 : null;
            return
                eResult != null
                ? eResult is IHTMLElement
                ? true : false : false;
        }
    }
}
