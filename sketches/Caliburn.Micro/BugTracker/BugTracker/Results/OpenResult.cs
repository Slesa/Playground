using System;
using Caliburn.Micro;

namespace BugTracker.Results
{
    public class OpenResult<T> : IResult
    {
        private readonly Func<ActionExecutionContext, T> _locateItem = c => IoC.Get<T>();
        private Action<T> _beforeActivation;
        private Func<ActionExecutionContext, IConductor> _locateConductor = c => c.Target as IConductor;

        public OpenResult()
        {
        }

        public OpenResult(T item)
        {
            _locateItem = c => item;
        }

        #region IResult Members

        public void Execute(ActionExecutionContext context)
        {
            IConductor conductor = _locateConductor(context);
            T item = _locateItem(context);

            EventHandler<ActivationProcessedEventArgs> processed = null;
            processed = (s, e) =>
                            {
                                conductor.ActivationProcessed -= processed;

                                if (e.Success)
                                {
                                    Completed(this, new ResultCompletionEventArgs());
                                }
                                else Completed(this, new ResultCompletionEventArgs { WasCancelled = true });
                            };

            conductor.ActivationProcessed += processed;

            if (_beforeActivation != null)
                _beforeActivation(item);

            conductor.ActivateItem(item);
        }

        public event EventHandler<ResultCompletionEventArgs> Completed;

        #endregion

        public OpenResult<T> In<TConductor>()
            where TConductor : IConductor
        {
            _locateConductor = c => IoC.Get<TConductor>();

            return this;
        }

        public OpenResult<T> BeforeActivation(Action<T> action)
        {
            _beforeActivation = action;

            return this;
        }

        public OpenResult<T> In(IConductor conductor)
        {
            _locateConductor = c => conductor;

            return this;
        }
    }
}