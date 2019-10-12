#nullable enable
using MSHTML;

namespace Com.DOM.Extensions
{
    public static class IHTMLAttributeCollectionExtensions
    {
        public static IHTMLAttributeCollection? Collection(
            this IHTMLAttributeCollection3 c3)
        {
            return
                (IHTMLAttributeCollection?)c3;
        }
        public static bool TryCollection(
            this IHTMLAttributeCollection3 c3
            , out IHTMLAttributeCollection? result)
        {
            result =
                c3?.Collection();
            return
            result != null
                ? result is IHTMLAttributeCollection
                    ? true
                : false
            : false;
        }
    }
}
