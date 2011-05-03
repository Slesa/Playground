using System;
using Caliburn.Micro;
using Action = Caliburn.Micro.Action;

namespace Lucifer.Editor.Results
{
    public class OpenResult<T> : IResult
    {
        readonly Func<ActionExecutionContext, T> _locateItem = c => IoC.Get<T>();
        Action<T> _beforeActivation;
        Func<ActionExecutionContext, IConductor> _locateConductor = c => c.Target as IConductor;

        public OpenResult()
        {
        }

        public OpenResult(T item)
        {
            _locateItem = c => item;
        }

        public void Execute(ActionExecutionContext context)
        {
            var conductor = _locateConductor(context);
            var item = _locateItem(context);

            EventHandler<ActivationProcessedEventArgs> processed = null;
            processed = (s, e) =>
                {
                    conductor.ActivationProcessed -= processed;
                    Completed(this,
                              e.Success
                                  ? new ResultCompletionEventArgs()
                                  : new ResultCompletionEventArgs {WasCancelled = true});
                };

            conductor.ActivationProcessed += processed;

            if (_beforeActivation != null)
                _beforeActivation(item);

            conductor.ActivateItem(item);
        }

        public event EventHandler<ResultCompletionEventArgs> Completed;

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