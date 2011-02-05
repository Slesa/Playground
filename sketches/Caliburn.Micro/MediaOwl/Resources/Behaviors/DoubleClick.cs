using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows.Interactivity;

namespace MediaOwl.Resources.Behaviors
{
    /// <summary>
    /// A DoubleClick-Behavior
    /// </summary>
    public class DoubleClick : TriggerBase<UIElement>
    {
        private readonly DispatcherTimer timer;
        private Point clickPosition;

        public DoubleClick()
        {
            timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 0, 0, 300)
            };

            timer.Tick += OnTimerTick;
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.MouseLeftButtonUp += AssociatedObject_MouseLeftButtonUp;
        }

        void AssociatedObject_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var element = sender as UIElement;

            if (timer.IsEnabled)
            {
                timer.Stop();
                Point position = e.GetPosition(element);

                if (Math.Abs(clickPosition.X - position.X) < 1 && Math.Abs(clickPosition.Y - position.Y) < 1)
                {
                    InvokeActions(null);
                }
            }
            else
            {
                timer.Start();
                clickPosition = e.GetPosition(element);
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.MouseLeftButtonUp -= AssociatedObject_MouseLeftButtonUp;
            if (timer.IsEnabled)
                timer.Stop();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            timer.Stop();
        }
    }
}