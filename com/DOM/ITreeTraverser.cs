#nullable enable
using System;
using System.Collections.Generic;
using Com.DOM.Interfaces;
using MSHTML;

namespace Com.DOM
{
    public interface ITreeTraverser<T> : ITreeTraverserHelper<T>
        where T : class
    {
        T? Current { get; set; }
        Func<IHTMLDOMNode, NodeFilterResult> Filter { get; }
        T Root { get; }
        WhatToShow WhatToShow { get; }
        IEnumerable<T?>? CurrentChildren();
        bool HasCurrentChildren();
        bool IsFirstChild();
        bool IsLastChild();
        T? PeekFirst();
        T? PeekLast();
        T? PeekNext();
        T? PeekNextSibling();
        T? PeekParent();
        T? PeekPrevious();
        T? PeekPreviousSibling();
        T? Reset();
        T? ToFirst();
        T? ToLast();
        T? ToNext();
        T? ToNextSibling();
        T? ToParent();
        T? ToPrevious();
        T? ToPreviousSibling();
    }
}