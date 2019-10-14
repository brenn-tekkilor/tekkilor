#nullable enable

using MSHTML;
using System;
using System.Collections.Generic;

namespace Com.DOM.Interfaces
{
    public interface ITreeTraverser<T> : ITreeTraverserHelper<T>
        where T : class
    {
        T? Current { get; set; }
        Func<IHTMLDOMNode, NodeFilterResult> Filter { get; }
        T Root { get; }
        WhatToShow WhatToShow { get; }

        IEnumerable<T?>? CurrentChildren { get; }

        bool HasCurrentChildren { get; }

        bool IsFirstChild { get; }

        bool IsLastChild { get; }

        T? PeekFirst();

        T? PeekFirst(T? target);

        T? PeekLast();

        T? PeekLast(T? target);

        T? PeekNext();

        T? PeekNext(T? target);

        T? PeekNextSibling();

        T? PeekNextSibling(T? target);

        T? PeekParent();

        T? PeekParent(T? target);

        T? PeekPrevious();

        T? PeekPrevious(T? target);

        T? PeekPreviousSibling();

        T? PeekPreviousSibling(T? target);

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