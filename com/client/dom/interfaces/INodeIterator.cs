using MSHTML;

namespace com.client.dom.interfaces
{
    public interface INodeIterator
    {
        IHTMLDOMNode Root { get; }
        IHTMLDOMNode Current { get; }
        IHTMLDOMNode ToNext();
        IHTMLDOMNode ToPrevious();
    }
}