#nullable enable
using System;
using System.Collections.Generic;

namespace Utility.Interfaces
{
    public interface IFactoryHelper
        <TIn, TOut>
    : IFactory
        <TIn, TOut>
    where TIn : class
    where TOut :class
    {
        delegate void
            AfterMany(
                IDictionary<TIn, TOut?>?
                evtArgs);
        event AfterMany?
            AfterManyEvent;
        delegate void
            AfterOne(
                ValueTuple<TIn?, TOut?>
                    evtArgs);
        event AfterOne?
            AfterOneEvent;
        delegate void
            BeforeMany(
                IEnumerable<TIn>?
                evtArgs);
        event BeforeMany?
            BeforeManyEvent;
        delegate void
            BeforeOne(
                    TIn? evtArgs);
        event BeforeOne?
            BeforeOneEvent;

    }
}
