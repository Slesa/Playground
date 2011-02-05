using System;
using System.Windows.Threading;

namespace MediaOwl.Core
{
    /// <summary>
    /// The StopWatch uses a <see cref="DispatcherTimer"/> to fire an <see cref="EventHandler"/>
    /// when a <see cref="TimeSpan"/> passed.
    /// </summary>
    public class StopWatch
    {
        private readonly DispatcherTimer timer;
        private readonly int interval;
        private long counter;
        private TimeSpan stop;

        /// <summary>
        /// The Constructor
        /// </summary>
        /// <param name="stop">The milliseconds to fire the <see cref="StopWatchEnded"/>-Event when passed.</param>
        /// <param name="interval">After each milliseconds-interval it is controlled wether to fire the <see cref="StopWatchEnded"/>-Event or not.</param>
        public StopWatch(int stop, int interval = 1000)
        {
            counter = 0;
            this.stop = new TimeSpan(0, 0, 0, 0, stop);
            this.interval = interval;
            timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, interval) };
            timer.Tick += StopWatchTick;
        }

        /// <summary>
        /// Start the StopWatch. The <see cref="StopWatchStarting"/>-Event is fired./>
        /// </summary>
        public void Start()
        {
            StopWatchStarting(this, new EventArgs());
            timer.Start();
        }

        private void StopWatchTick(object sender, EventArgs eventArgs)
        {
            counter += interval;
            if (counter >= stop.TotalMilliseconds)
            {
                Stop();
                StopWatchEnded(this, new EventArgs());
            }
        }

        /// <summary>
        /// Stops the <see cref="StopWatch"/> early. Does not fire the <see cref="StopWatchEnded"/>-Event.
        /// </summary>
        public void Stop()
        {
            timer.Stop();
            timer.Tick -= StopWatchTick;
        }

        public event EventHandler StopWatchEnded = delegate { };
        public event EventHandler StopWatchStarting = delegate { };
    }
}