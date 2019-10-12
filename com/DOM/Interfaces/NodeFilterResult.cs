#nullable enable
using System;

namespace Com.DOM.Interfaces
{
    public enum NodeFilterResult : Int32
    {
        Accept = 1,
        Reject = 2,
        Skip = 3,
    }
}
