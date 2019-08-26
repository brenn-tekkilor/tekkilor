#nullable enable
using MSHTML;
namespace com.client.dom.interfaces
{
    public interface ITreeWalker
    {
        NodeFilterResult? FilterNode(IHTMLDOMNode n);
        IHTMLDOMNode? ToParent();
        IHTMLDOMNode? ToFirst();
        IHTMLDOMNode? ToLast();
        IHTMLDOMNode? ToPreviousSibling();
        IHTMLDOMNode? ToNextSibling();
        IHTMLDOMNode? ToPrevious();
        IHTMLDOMNode? ToNext();
    }
}
