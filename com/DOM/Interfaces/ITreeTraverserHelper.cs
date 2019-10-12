#nullable enable
using Com.DOM.Interfaces;
using MSHTML;

namespace Com.DOM.Interfaces
{
    /*
         * Interface that when implemented
         * results in an event emitting
         * TreeTraverser.
         * 
         * Events are emitted bother
         * before and after all
         * traversal commands, and
         * return an evtArg object.
         * 
         * The evtArg object consists
         * of the following:
         * 
         * The event emitting tree.
         * The pointer before traversal.
         * The name of the method
         *      where in the event was
         *      fired and evtArgs created.
         */
    public interface ITreeTraverserHelper<T>
        where T: class
    {
        delegate void
            AfterTraversal(
                (ITreeTraverser<T>
                , T?
                , string?)
                evtArgs);
        event AfterTraversal?
            AfterTraversalEvent;
        delegate void
            BeforeTraversal(
                (ITreeTraverser<T>
                , T?
                , string?)
                evtArgs);
        event BeforeTraversal?
            BeforeTraversalEvent;
    }
}
