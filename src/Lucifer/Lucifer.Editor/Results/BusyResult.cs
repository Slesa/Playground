using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Caliburn.Micro;
using Lucifer.Editor.Resources;
using Microsoft.Windows.Controls;

namespace Lucifer.Editor.Results
{
    public class BusyResult : IResult
    {
        bool _hide;
        string _message;

        public void Execute(ActionExecutionContext context)
        {
            Caliburn.Micro.Execute.OnUIThread(UpdateBusyIndicator);
            Completed(this, new ResultCompletionEventArgs());
        }

        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };

        public BusyResult Show()
        {
            _hide = false;
            return this;
        }

        public BusyResult Hide()
        {
            _hide = true;
            return this;
        }

        public BusyResult WithMessage(string message)
        {
            _message = message;
            return this;
        }

        void UpdateBusyIndicator()
        {
            var queue = new Queue<FrameworkElement>();
            queue.Enqueue(Application.Current.MainWindow);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (current == null)
                    continue;

                var indicator = current as BusyIndicator;
                if (indicator != null)
                {
                    indicator.IsBusy = !_hide;
                    indicator.BusyContent = _message ?? Strings.Message_Busy_Indicator;
                    break;
                }

                var count = VisualTreeHelper.GetChildrenCount(current);
                for(var i=0; i<count; i++)
                    queue.Enqueue(VisualTreeHelper.GetChild(current, i) as FrameworkElement);
            }
        }
    }
}