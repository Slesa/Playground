using System;
using Caliburn.Micro;

namespace MediaOwl.Core
{
    /// <summary>
    /// An <see cref="IResult"/>, that takes a Type of ChildScreen, 
    /// locates/instanciates it (<see cref="locateChild"/>), 
    /// locates its Parent (<see cref="In&lt;TParent&gt;"/>, 
    /// configures the ChildScreen (<see cref="Configured"/>) 
    /// and activates it in its Parent.
    /// </summary>
    /// <remarks>This class was taken from the Caliburn Framework</remarks>
    /// <typeparam name="TChild">The Child-Type</typeparam>
    public class OpenChildResult<TChild> : IResult
    {
        private Func<ActionExecutionContext, IConductor> locateParent;
        private Action<TChild> onConfigure;

        private readonly Func<ActionExecutionContext, TChild> locateChild =
            c => IoC.Get<TChild>();

        public OpenChildResult() { }

        public OpenChildResult(TChild child)
        {
            locateChild = c => child;
        }

        public OpenChildResult<TChild> In<TParent>()
        {
            locateParent = c => IoC.Get<TParent>() as IConductor;
            return this;
        }

        public OpenChildResult<TChild> In(object parent)
        {
            locateParent = c => parent as IConductor;
            return this;
        }

        public OpenChildResult<TChild> Configured(Action<TChild> configure)
        {
            onConfigure = configure;
            return this;
        }

        public void Execute(ActionExecutionContext context)
        {
            if (locateParent == null)
                locateParent = c => (IConductor)c.Target;

            var parent = locateParent(context);
            var child = locateChild(context);

            if (onConfigure != null)
                onConfigure(child);

            parent.ActivateItem(child);
            Completed(this, new ResultCompletionEventArgs());
        }

        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };
    }
}