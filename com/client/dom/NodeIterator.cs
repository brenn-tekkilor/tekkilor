#nullable enable
using System;
using MSHTML;
using com.client.dom.interfaces;

namespace com.client.dom
{
    public class NodeIterator : INodeIterator
    {
        public IHTMLDOMNode Root { get; private set; }
        public WhatToShow WhatToShow { get; private set; }
        public bool PointerBeforeReferenceNode { get; private set; }
        public IHTMLDOMNode? Current { get; private set; }
        public NodeFilter NodeFilter { get; }
        public NodeIterator(IHTMLDOMNode root, WhatToShow whatToShow, NodeFilter filter)
        {
            Root = Current = root;
            WhatToShow = whatToShow;
            PointerBeforeReferenceNode = true;
            NodeFilter = filter;
        }

        private IHTMLDOMNode? Traverse(bool directionNext)
        {
            IHTMLDOMNode? next = null;
            bool beforeNode = PointerBeforeReferenceNode;

            while (true)
            {
                if (directionNext)
                {
                    if (beforeNode)
                    {
                        beforeNode = false;
                    }
                    else
                    {
                        if (Current != null)
                            next = Current.nextSibling;
                        if (next == null)
                        {
                            return null;
                        }
                    }
                }
                else
                {
                    if (beforeNode)
                    {
                        if (Current != null)
                            next = Current.previousSibling;
                        if (next == null)
                        {
                            return null;
                        }
                    }
                    else
                    {
                        beforeNode = true;
                    }
                }
                if (next != null)
                {

                    NodeFilterResult? result = FilterNode(next);
                    if (result != null && result == NodeFilterResult.Accept)
                    {
                        break;
                    }
                }
            }
            PointerBeforeReferenceNode = beforeNode;
            Current = next;
            return next;
        }

        #region INodeIterator
        public IHTMLDOMNode? ToNext()
        {
            return Traverse(true);
        }
        public IHTMLDOMNode? ToPrevious()
        {
            return Traverse(false);
        }
        #endregion
        public NodeFilterResult? FilterNode(IHTMLDOMNode n) => NodeFilter(n);
        public NodeFilterResult? CheckNode(IHTMLDOMNode? n)
        {
            if (n != null)
            {
                return WhatToShow switch
                {
                    WhatToShow.All => FilterNode(n),
                    WhatToShow.Elements => n.nodeType == 1
                            ? FilterNode(n)
                            : NodeFilterResult.Skip,
                    WhatToShow.TextValues => n.nodeType == 3
                            ? FilterNode(n)
                            : NodeFilterResult.Skip,
                    WhatToShow.ProcessingInstructions => NodeFilterResult.Skip,
                    WhatToShow.Comments => NodeFilterResult.Skip,
                    WhatToShow.Documents => NodeFilterResult.Skip,
                    WhatToShow.DocumentTypes => NodeFilterResult.Skip,
                    WhatToShow.DocumentFragments => NodeFilterResult.Skip,
                    _ => NodeFilterResult.Reject,
                };
            }
            return null;
        }

    };
}
