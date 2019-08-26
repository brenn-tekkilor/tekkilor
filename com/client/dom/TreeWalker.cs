#nullable enable
using com.client.dom.interfaces;
using MSHTML;
using System;
namespace com.client.dom
{
    public class TreeWalker : ITreeWalker
    {
        public IHTMLDOMNode Root { get; }
        public WhatToShow WhatToShow { get; }
        public NodeFilter NodeFilter { get; }
        public IHTMLDOMNode Current { get; set; }
        public TreeWalker(IHTMLDOMNode root, WhatToShow whatToShow, NodeFilter filter)
        {
            Root = root;
            WhatToShow = whatToShow;
            NodeFilter = filter;
            Current = root;
        }
        public IHTMLDOMNode? ToFirst()
        {
            IHTMLDOMNode? n = Current?.firstChild; // ref current>child0
            while (n != null) //while not null
            {
                NodeFilterResult? r = CheckNode(n); // check n
                if (r != null)
                {
                    if (r == NodeFilterResult.Accept)  // if accept
                    {
                        Current = n;  // set current
                        return n; // return node
                    }
                    else if (r == NodeFilterResult.Skip) // if skip
                    {
                        IHTMLDOMNode c = n.firstChild; // ref current>child0>child0

                        if (c != null) // if not null
                        {
                            n = c; // replace ref n
                            continue; // repeate
                        }
                    }
                }
                while (n != null)
                {
                    IHTMLDOMNode s = n.nextSibling;
                    if (s != null)
                    {
                        n = s;
                        break;
                    }
                    IHTMLDOMNode p = n.parentNode;
                    if (p == null || p == Root || p == Current)
                        return null;
                    n = p;
                }
            }
            return null;
        }
        public IHTMLDOMNode? ToLast()
        {
            IHTMLDOMNode? n = Current?.lastChild;
            while (n != null)
            {
                NodeFilterResult? r = CheckNode(n);
                if (r != null)
                {
                    if (r == NodeFilterResult.Accept)
                    {
                        Current = n;
                        return n;
                    }
                    else if (r == NodeFilterResult.Skip)
                    {
                        IHTMLDOMNode c = n.lastChild;
                        if (c != null)
                        {
                            n = c;
                            continue;
                        }
                    }
                }
                while (n != null)
                {
                    IHTMLDOMNode s = n.previousSibling;
                    if (s != null)
                    {
                        n = s;
                        break;
                    }
                    IHTMLDOMNode p = n.parentNode;
                    if (p == null || p == Root || p == Current)
                        return null;
                    n = p;
                }
            }
            return null;
        }
        public IHTMLDOMNode? ToNext()
        {
            IHTMLDOMNode n = Current;
            NodeFilterResult? r = NodeFilterResult.Accept;
            while (n != null)
            {
                while (r != NodeFilterResult.Reject && n.hasChildNodes())
                {
                    n = n.firstChild;
                    r = CheckNode(n);
                    if (r != null && r == NodeFilterResult.Accept)
                    {
                        Current = n;
                        return n;
                    }
                }
                while (n != Root)
                {
                    IHTMLDOMNode s = n.nextSibling;
                    if (s != null)
                    {
                        n = s;
                        break;
                    }
                    n = n.parentNode;
                }
                if (n == Root)
                    break;
                r = CheckNode(n);
                if (r != null && r == NodeFilterResult.Accept)
                {
                    Current = n;
                    return n;
                }
            }
            return null;
        }
        public IHTMLDOMNode? ToNextSibling()
        {
            IHTMLDOMNode? n = Current;
            if (n == Root)
                return null;
            if (n != null)
            {
                IHTMLDOMNode s = n.nextSibling;
                while (s != null)
                {
                    NodeFilterResult? r = CheckNode(s);
                    if (r != null)
                    {
                        if (r == NodeFilterResult.Accept)
                        {
                            Current = s;
                            return s;
                        }
                        if (r == NodeFilterResult.Reject || r == NodeFilterResult.Skip)
                            s = n.nextSibling;
                    }
                }
            }
            return null;
        }
        public IHTMLDOMNode? ToParent()
        {
            IHTMLDOMNode n = Current;

            while (n != null && n != Root)
            {
                n = n.parentNode;

                if (n != null && CheckNode(n) == NodeFilterResult.Accept)
                {
                    Current = n;
                    return n;
                }
            }

            return null;
        }
        public IHTMLDOMNode? ToPrevious()
        {
            IHTMLDOMNode n = Current;
            while (n != null && n != Root)
            {
                IHTMLDOMNode s = n.previousSibling;

                while (s != null)
                {
                    n = s;
                    NodeFilterResult? r = CheckNode(n);
                        while (r != null && r != NodeFilterResult.Reject && n.hasChildNodes())
                        {
                            n = n.lastChild;
                            r = CheckNode(n);

                            if (r == NodeFilterResult.Accept)
                            {
                                Current = n;
                                return n;
                            }
                        }
                    }

                    if (n == Root)
                        break;

                    IHTMLDOMNode p = n.parentNode;

                    if (p == null)
                        break;

                    if (CheckNode(n) == NodeFilterResult.Accept)
                    {
                        Current = n;
                        return n;
                    }
                }

            return null;
        }
        public IHTMLDOMNode? ToPreviousSibling()
        {
            IHTMLDOMNode n = Current;

            if (n == Root)
                return null;

            while (n != null)
            {
                IHTMLDOMNode s = n.previousSibling;

                while (s != null)
                {
                    n = s;
                    NodeFilterResult? r = CheckNode(n);

                    if (r != null && r == NodeFilterResult.Accept)
                    {
                        Current = n;
                        return n;
                    }

                    s = n.lastChild;

                    if (r != null && r == NodeFilterResult.Reject || s == null)
                        s = n.previousSibling;
                }

                n = n.parentNode;

                if (n == null || n == Root || CheckNode(n) == NodeFilterResult.Accept)
                    break;
            }
            return null;
        }
        public NodeFilterResult? FilterNode(IHTMLDOMNode n) => NodeFilter(n);
        public NodeFilterResult? CheckNode(IHTMLDOMNode? n)
        {
            if (n != null)
            {
                switch (WhatToShow)
                {
                    case WhatToShow.All:
                        return FilterNode(n);
                    case WhatToShow.Elements:
                        if (n.nodeType == 1)
                            return FilterNode(n);
                        return NodeFilterResult.Skip;
                    case WhatToShow.TextValues:
                        if (n.nodeType == 3)
                            return FilterNode(n);
                        return NodeFilterResult.Skip;
                    default:
                        return NodeFilterResult.Reject;
                }
            }
            return null;
        }
    }
}
