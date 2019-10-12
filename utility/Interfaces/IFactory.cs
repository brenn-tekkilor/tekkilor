#nullable enable

using System.Collections.Generic;

namespace Utility.Interfaces
{
    public interface IFactory<TIn, TOut>
        where TIn: class
        where TOut: class
    {
        IDictionary
            <TIn, TOut?>?
            Make(
                TIn?
                    input);
        IDictionary
            <TIn, TOut?>?
            Make(
                IEnumerable<TIn>?
                    input);
    }
}
