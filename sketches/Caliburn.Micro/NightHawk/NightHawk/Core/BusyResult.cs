﻿using System;
using Caliburn.Micro;

namespace NightHawk.Core
{
    public class BusyResult : IResult
    {
        readonly bool _isBusy;
        readonly object _busyViewModel;

        public BusyResult(bool isBusy, object busyViewModel)
        {
            _isBusy = isBusy;
            _busyViewModel = busyViewModel;
        }

        public void Execute(ActionExecutionContext context)
        {
            var sourceViewModel = _busyViewModel ?? context.Target;

            if (_isBusy)
                IoC.Get<IBusyService>().MarkAsBusy(sourceViewModel, _busyViewModel);
            else
                IoC.Get<IBusyService>().MarkAsNotBusy(sourceViewModel);

            Completed(this, new ResultCompletionEventArgs());
        }

        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };
    }
}