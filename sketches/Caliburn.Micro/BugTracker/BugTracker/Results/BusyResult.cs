using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Caliburn.Micro;
using Microsoft.Windows.Controls;

namespace BugTracker.Results
{
    public class BusyResult : IResult
    {
        private bool _hide;
        private string _message;

        #region IResult Members

        public void Execute(ActionExecutionContext context)
        {
            Caliburn.Micro.Execute.OnUIThread(UpdateBusyIndicator);

            Completed(this, new ResultCompletionEventArgs());
        }

        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };

        #endregion

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

        private void UpdateBusyIndicator()
        {
            var queue = new Queue<FrameworkElement>();
            queue.Enqueue(Application.Current.MainWindow);

            while (queue.Count > 0)
            {
                FrameworkElement current = queue.Dequeue();
                if (current == null)
                    continue;

                var indicator = current as BusyIndicator;
                if (indicator != null)
                {
                    indicator.IsBusy = !_hide;
                    indicator.BusyContent = _message ?? "Please Wait...";

                    break;
                }

                int count = VisualTreeHelper.GetChildrenCount(current);
                for (int i = 0; i < count; i++)
                {
                    queue.Enqueue(VisualTreeHelper.GetChild(current, i) as FrameworkElement);
                }
            }
        }
    }
}