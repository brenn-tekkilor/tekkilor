#nullable enable
using MSHTML;
using Com.DOM.Interfaces;
using Com.DOM.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Runtime.CompilerServices;
namespace Com.DOM
{
    public class TreeTraverser<T> : ITreeTraverser<T>
        where T : class
    {
        private readonly IHTMLDOMNode _root;
        private readonly WhatToShow _whatToShow;
        private readonly Func<IHTMLDOMNode, NodeFilterResult> _filter;
        private IHTMLDOMNode? _current;
        public event
            ITreeTraverserHelper<T>
            .AfterTraversal?
            AfterTraversalEvent;
        public event
            ITreeTraverserHelper<T>
            .BeforeTraversal?
            BeforeTraversalEvent;
        public TreeTraverser(
            IHTMLDOMNode root
            , WhatToShow whatToShow
            , Func<IHTMLDOMNode, NodeFilterResult> filter)
        {
            _root = root;
            _whatToShow = whatToShow;
            _filter = filter;
            _current = root;
        }
        public T Root => (T)_root;
        public WhatToShow WhatToShow => _whatToShow;
        public Func<IHTMLDOMNode, NodeFilterResult> Filter => _filter;
        public T? Current
        {
            get => CastOut(_current);
            set => _current = CastIn(value);
        }
        public bool HasCurrentChildren
        {
            get
            {
                IHTMLDOMNode? current =
                    _current;
                IHTMLDOMNode? child
                = current != null
                    ? ToFirstNode()
                : null;
                IHTMLDOMNode? parent
                = child != null
                    ? ToParentNode()
                : null;
                _current = current;
                return
                parent != null
                    ? parent.Equals(current)
                : false;
            }
        }

        public IEnumerable<T?>? CurrentChildren
        {
            get
            {
                IEnumerable<IHTMLDOMNode?>? children =
                    CurrentChildNodes();
                return
                children != null
                    ? children.Any()
                        ? children.Select(
                            n => CastOut(n))
                        .AsEnumerable()
                    : null
                : null;
            }
        }

        public bool IsFirstChild
        {
            get
            {
                IHTMLDOMNode? current =
                    _current;
                bool result =
                    ToPreviousSiblingNode()
                        == null
                        ? true
                    : false;
                _current = current;
                return
                    result;
            }
        }

        public bool IsLastChild
        {
            get
            {
                IHTMLDOMNode? current =
                     _current;
                bool result =
                    ToNextSiblingNode()
                        == null
                        ? true
                    : false;
                _current = current;
                return
                    result;
            }
        }

        public T? PeekFirst()
        {
            IHTMLDOMNode? current =
               _current;
            T? result =
                    CastOut(
                        ToFirstNode());
            _current = current;
            return
               result;
        }
        public T? PeekFirst(T? target)
        {
            IHTMLDOMNode? current =
               _current;
            _current = CastIn(target);
            T? result =
               PeekParent();
            _current = current;
            return
               result;
        }
        public T? PeekLast()
        {
            IHTMLDOMNode? current =
               _current;
            T? result =
               CastOut(
                   ToLastNode());
            _current = current;
            return
               result;
        }
        public T? PeekLast(T? target)
        {
            IHTMLDOMNode? current =
               _current;
            _current = CastIn(target);
            T? result =
               PeekParent();
            _current = current;
            return
               result;
        }
        public T? PeekNext()
        {
            IHTMLDOMNode? current =
               _current;
            T? result =
               CastOut(
                   ToNextNode());
            _current = current;
            return
               result;
        }
        public T? PeekNext(T? target)
        {
            IHTMLDOMNode? current =
               _current;
            _current = CastIn(target);
            T? result =
               PeekParent();
            _current = current;
            return
               result;
        }
        public T? PeekNextSibling()
        {
            IHTMLDOMNode? current =
               _current;
            T? result =
               CastOut(
                   ToNextSiblingNode());
            _current = current;
            return
               result;
        }
        public T? PeekNextSibling(T? target)
        {
            IHTMLDOMNode? current =
               _current;
            _current = CastIn(target);
            T? result =
               PeekParent();
            _current = current;
            return
               result;
        }
        public T? PeekParent()
        {
            IHTMLDOMNode? current =
               _current;
            T? result =
               CastOut(
                   ToParentNode());
            _current = current;
            return
               result;
        }
        public T? PeekParent(T? target)
        {
            IHTMLDOMNode? current =
               _current;
            _current = CastIn(target);
            T? result =
               PeekParent();
            _current = current;
            return
               result;
        }
        public T? PeekPrevious()
        {
            IHTMLDOMNode? current =
               _current;
            T? result =
               CastOut(
                   ToPreviousNode());
            _current = current;
            return
               result;
        }
        public T? PeekPrevious(T? target)
        {
            IHTMLDOMNode? current =
               _current;
            _current = CastIn(target);
            T? result =
               PeekParent();
            _current = current;
            return
               result;
        }
        public T? PeekPreviousSibling()
        {
            IHTMLDOMNode? current =
               _current;
            T? result =
               CastOut(
                   ToPreviousSiblingNode());
            _current = current;
            return
               result;
        }
        public T? PeekPreviousSibling(T? target)
        {
            IHTMLDOMNode? current =
               _current;
            _current = CastIn(target);
            T? result =
               PeekParent();
            _current = current;
            return
               result;
        }
        public T? Reset()
        {
            string? method =
                GetCurrentMethod();
            T? current =
               Current;
            BeforeTraversalEvent?.Invoke((
                this
                , current
                , method));
            T? result =
               CastOut(ResetNodes());
            AfterTraversalEvent?.Invoke((
                this
                , current
                , method));
            return result;
        }
        public T? ToFirst()
        {
            string? method =
               GetCurrentMethod();
            T? current =
               Current;
            BeforeTraversalEvent?.Invoke((
               this
               , current
               , method));
            T? result =
               CastOut(ToFirstNode());
            AfterTraversalEvent?.Invoke((
               this
               , current
               , method));
            return result;
        }
        public T? ToLast()
        {
            string? method =
               GetCurrentMethod();
            T? current =
               Current;
            BeforeTraversalEvent?.Invoke((
               this
               , current
               , method));
            T? result =
               CastOut(ToLastNode());
            AfterTraversalEvent?.Invoke((
               this
               , current
               , method));
            return result;
        }
        public T? ToNext()
        {
            string? method =
               GetCurrentMethod();
            T? current =
               Current;
            BeforeTraversalEvent?.Invoke((
               this
               , current
               , method));
            T? result =
               CastOut(ToNextNode());
            AfterTraversalEvent?.Invoke((
               this
               , current
               , method));
            return result;
        }
        public T? ToNextSibling()
        {
            string? method =
               GetCurrentMethod();
            T? current =
               Current;
            BeforeTraversalEvent?.Invoke((
               this
               , current
               , method));
            T? result =
               CastOut(ToNextSiblingNode());
            AfterTraversalEvent?.Invoke((
               this
               , current
               , method));
            return result;
        }
        public T? ToParent()
        {
            string? method =
               GetCurrentMethod();
            T? current =
               Current;
            BeforeTraversalEvent?.Invoke((
               this
               , current
               , method));
            T? result =
               CastOut(ToParentNode());
            AfterTraversalEvent?.Invoke((
               this
               , current
               , method));
            return result;
        }
        public T? ToPrevious()
        {
            string? method =
               GetCurrentMethod();
            T? current =
               Current;
            BeforeTraversalEvent?.Invoke((
               this
               , current
               , method));
            T? result =
               CastOut(ToPreviousNode());
            AfterTraversalEvent?.Invoke((
               this
               , current
               , method));
            return result;
        }
        public T? ToPreviousSibling()
        {
            string? method =
               GetCurrentMethod();
            T? current =
               Current;
            BeforeTraversalEvent?.Invoke((
               this
               , current
               , method));
            T? result =
               CastOut(ToPreviousSiblingNode());
            AfterTraversalEvent?.Invoke((
               this
               , current
               , method));
            return result;
        }
        private IEnumerable<IHTMLDOMNode?>? CurrentChildNodes()
        {
            IHTMLDOMNode? current = _current;
            List<IHTMLDOMNode?> result =
                new List<IHTMLDOMNode?>();
            if (HasCurrentChildren)
                result.Add(ToFirstNode());
            while(ToNextSiblingNode() != null)
            {
                result.Add(_current);
            }
            _current = current;
            return
                result.Any()
                    ?
                    (from
                        IHTMLDOMNode
                            node
                    in
                         result
                     where
                         node != null
                     select
                        node)
                    .Where(
                        node => node != null)
                    .AsEnumerable()
                : null;
        }
        private IHTMLDOMNode? ResetNodes()
        {
            _current = _root;
            return
                ToFirstNode();
        }
        private IHTMLDOMNode? ToFirstNode()
        {
            IHTMLDOMNode? node =
                _current?.firstChild;
            while (node != null)
            {
                NodeFilterResult result =
                    CheckNode(node);
                if (result == NodeFilterResult.Accept)
                {
                    _current = node;
                    return node;
                }
                else if (result == NodeFilterResult.Skip)
                {
                    IHTMLDOMNode? child =
                        node?.firstChild;
                    if (child != null)
                    {
                        node = child;
                        continue;
                    }
                }
                while (node != null)
                {
                    IHTMLDOMNode? sibling =
                       node.nextSibling;
                    if (sibling != null)
                    {
                        node = sibling;
                        break;
                    }
                    IHTMLDOMNode? parent =
                       node.parentNode;
                    if (parent == null
                        || parent == _root
                        || parent == _current)
                    {
                        node = null;
                        break;
                    }
                    node = parent;
                }
            }
            return null;
        }
        private IHTMLDOMNode? ToLastNode()
        {
            IHTMLDOMNode? node =
               _current?.lastChild;
            while (node != null)
            {
                NodeFilterResult result =
                   CheckNode(node);
                if (result == NodeFilterResult.Accept)
                {
                    _current = node;
                    return node;
                }
                else if (result == NodeFilterResult.Skip)
                {
                    IHTMLDOMNode? child =
                       node.lastChild;
                    if (child != null)
                    {
                        node = child;
                        continue;
                    }
                }
                while (node != null)
                {
                    IHTMLDOMNode? sibling =
                       node.previousSibling;
                    if (sibling != null)
                    {
                        node = sibling;
                        break;
                    }
                    IHTMLDOMNode? parent =
                       node.parentNode;
                    if (parent == null
                        || parent == _root
                        || parent == _current)
                    {
                        node = null;
                        break;
                    }
                    node = parent;
                }
            }
            return null;
        }
        private IHTMLDOMNode? ToNextNode()
        {
            IHTMLDOMNode? node =
               _current;
            NodeFilterResult result =
               NodeFilterResult.Accept;
            while (node != null)
            {
                while (node != null
                        && result != NodeFilterResult.Reject
                        && node.hasChildNodes())
                {
                    node = node.firstChild;
                    result = CheckNode(node);
                    if (result == NodeFilterResult.Accept)
                    {
                        _current = node;
                        return node;
                    }
                }
                while (node != null
                            && node != _root)
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
                if (node == _root)
                {
                    break;
                }
                result = CheckNode(node);
                if (result == NodeFilterResult.Accept)
                {
                    _current = node;
                    return node;
                }
            }
            return null;
        }
        private IHTMLDOMNode? ToNextSiblingNode()
        {
            IHTMLDOMNode? node =
               _current;
            if (node != _root)
            {
                while (node != null)
                {
                    IHTMLDOMNode? sibling =
                       node.nextSibling;
                    while (sibling != null)
                    {
                        node = sibling;
                        NodeFilterResult result =
                           CheckNode(node);
                        if (result == NodeFilterResult.Accept)
                        {
                            _current = node;
                            return node;
                        }
                        sibling = node.firstChild;
                        if (result == NodeFilterResult.Reject
                            || sibling == null)
                        {
                            sibling = node.nextSibling;
                        }
                    }
                    node = node.parentNode;
                    if (node == null
                        || node == _root
                        || CheckNode(node) == NodeFilterResult.Accept)
                    {
                        break;
                    }
                }
            }
            return null;
        }
        private IHTMLDOMNode? ToParentNode()
        {
            IHTMLDOMNode? node =
               _current;
            while (node != null
                        && node != _root)
            {
                node = node.parentNode;
                if (node != null
                    && CheckNode(node) == NodeFilterResult.Accept)
                {
                    _current = node;
                    return node;
                }
            }
            return null;
        }
        private IHTMLDOMNode? ToPreviousNode()
        {
            IHTMLDOMNode? node =
               _current;
            while (node != null
                        && node != _root)
            {
                IHTMLDOMNode? sibling =
                   node.previousSibling;
                while (sibling != null)
                {
                    node = sibling;
                    NodeFilterResult result =
                       CheckNode(node);
                    while (node != null
                                && result != NodeFilterResult.Reject
                                && node.hasChildNodes())
                    {
                        node = node.lastChild;
                        result = CheckNode(node);
                        if (result == NodeFilterResult.Accept)
                        {
                            _current = node;
                            return node;
                        }
                    }
                }
                if (node == _root)
                    break;
                IHTMLDOMNode? parent =
                   node?.parentNode;
                if (parent == null)
                    break;
                if (CheckNode(node) == NodeFilterResult.Accept)
                {
                    _current = node;
                    return node;
                }
            }
            return null;
        }
        private IHTMLDOMNode? ToPreviousSiblingNode()
        {
            IHTMLDOMNode? node =
               _current;
            if (node != _root)
            {
                while (node != null)
                {
                    IHTMLDOMNode? sibling =
                       node.previousSibling;
                    while (sibling != null)
                    {
                        node = sibling;
                        NodeFilterResult result =
                           CheckNode(node);
                        if (result == NodeFilterResult.Accept)
                        {
                            _current = node;
                            return node;
                        }
                        sibling = node.lastChild;
                        if ((result == NodeFilterResult.Reject
                            || sibling == null))
                        {
                            sibling = node.previousSibling;
                        }
                    }
                    node = node.parentNode;
                    if (node == null
                        || node == _root
                        || CheckNode(node) == NodeFilterResult.Accept)
                    {
                        break;
                    }
                }
            }
            return null;
        }
        private NodeFilterResult FilterNode(IHTMLDOMNode node) => _filter(node);
        private NodeFilterResult CheckNode(IHTMLDOMNode? node)
        {
            if (node != null)
            {
                switch (_whatToShow)
                {
                    case WhatToShow.All:
                        return FilterNode(node);
                    case WhatToShow.Elements:
                        if (node.nodeType == 1)
                            return FilterNode(node);
                        break;
                    case WhatToShow.TextValues:
                        if (node.nodeType == 3)
                            return FilterNode(node);
                        break;
                    default:
                        break;
                }
            }
            return NodeFilterResult.Reject;
        }
        private IHTMLDOMNode? CastIn(T? inCast)
        {
            return
            inCast != null
                ? (inCast as IHTMLDOMNode) != null
                    ? (IHTMLDOMNode?)inCast
                : null
            : null;
        }
        private T? CastOut(IHTMLDOMNode? OutCast)
        {
            return
            OutCast != null
                ? (OutCast as T) != null
                    ? (T?)OutCast
                : null
            : null;
        }
        [MethodImpl(MethodImplOptions.NoInlining)]
        private string? GetCurrentMethod()
        {
            var st =
                new StackTrace();
            var sf =
                st.GetFrame(1);
            return
            sf?.GetMethod()?.Name;
        }
    }
}
