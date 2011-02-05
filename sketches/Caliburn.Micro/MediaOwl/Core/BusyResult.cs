using System;
using Caliburn.Micro;

namespace MediaOwl.Core
{
    /// <summary>
    /// An <see cref="IResult"/> that calls the MarkAsBusy- or MarkAsNotBusy-Method of an <see cref="IBusyService"/>.
    /// When executed, the class uses an <see cref="IBusyService"/>.
    /// This class was taken from the Caliburn Framework.
    /// </summary>
    public class BusyResult : IResult
    {
        private readonly bool isBusy;
        private readonly object busyViewModel;

        /// <summary>
        /// The Constructor
        /// </summary>
        /// <param name="isBusy">Mark as busy or not.</param>
        /// <param name="busyViewModel">The concerned ViewModel.
        /// If null, the <see cref="ActionExecutionContext"/>-Target is taken.</param>
        public BusyResult(bool isBusy, object busyViewModel)
        {
            this.isBusy = isBusy;
            this.busyViewModel = busyViewModel;
        }

        public void Execute(ActionExecutionContext context)
        {
            var sourceViewModel = busyViewModel ?? context.Target;

            if (isBusy)
                IoC.Get<IBusyService>().MarkAsBusy(sourceViewModel, busyViewModel);
            else
                IoC.Get<IBusyService>().MarkAsNotBusy(sourceViewModel);

            Completed(this, new ResultCompletionEventArgs());
        }

        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };
    }
}