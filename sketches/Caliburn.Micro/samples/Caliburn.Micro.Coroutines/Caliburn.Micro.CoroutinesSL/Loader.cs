using System;
using System.Windows;
using System.Windows.Controls;

namespace Caliburn.Micro.CoroutinesSL
{
    public class Loader : IResult
    {
        readonly bool _hide;
        readonly string _message;

        public Loader(string message)
        {
            _message = message;
        }

        public Loader(bool hide)
        {
            _hide = hide;
        }

        public void Execute(ActionExecutionContext context)
        {
            var view = context.View as FrameworkElement;
            while (view != null)
            {
                var busyIndicator = view as BusyIndicator;
                if (busyIndicator != null)
                {
                    if (!string.IsNullOrEmpty(_message))
                        busyIndicator.BusyContent = _message;
                    busyIndicator.IsBusy = !_hide;
                    break;
                }
                view = view.Parent as FrameworkElement;
            }
            Completed(this, new ResultCompletionEventArgs());
        }

        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };

        public static IResult Show(string message=null)
        {
            return new Loader(message);
        }

        public static IResult Hide()
        {
            return new Loader(true);
        }
    }
}