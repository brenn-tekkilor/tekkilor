#nullable enable
using System;

namespace com.client.dom.interfaces
{
    public enum NodeFilterResult : Int32
    {
        Accept = 1,
        Reject = 2,
        Skip = 3,
    }
}
