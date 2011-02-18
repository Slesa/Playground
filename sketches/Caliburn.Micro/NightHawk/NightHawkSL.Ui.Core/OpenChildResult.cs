using System;
using Caliburn.Micro;

namespace NightHawkSL.Ui.Core
{
    public class OpenChildResult<TChild> : IResult
    {
        private Func<ActionExecutionContext, IConductor> _locateParent;
        private Action<TChild> _onConfigure;

        private readonly Func<ActionExecutionContext, TChild> _locateChild = c => IoC.Get<TChild>();

        public OpenChildResult() { }

        public OpenChildResult(TChild child)
        {
            _locateChild = c => child;
        }

        public OpenChildResult<TChild> In<TParent>()
        {
            _locateParent = c => IoC.Get<TParent>() as IConductor;
            return this;
        }

        public OpenChildResult<TChild> In(object parent)
        {
            _locateParent = c => parent as IConductor;
            return this;
        }

        public OpenChildResult<TChild> Configured(Action<TChild> configure)
        {
            _onConfigure = configure;
            return this;
        }

        public void Execute(ActionExecutionContext context)
        {
            if (_locateParent == null)
                _locateParent = c => (IConductor)c.Target;

            var parent = _locateParent(context);
            var child = _locateChild(context);

            if (_onConfigure != null)
                _onConfigure(child);

            parent.ActivateItem(child);
            Completed(this, new ResultCompletionEventArgs());
        }

        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };
    }
}