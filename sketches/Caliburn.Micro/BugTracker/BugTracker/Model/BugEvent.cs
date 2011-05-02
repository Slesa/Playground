using System;

namespace BugTracker.Model
{
    public class BugEvent : EventArgs
    {
        public BugEvent(Bug bug)
        {
            Bug = bug;
        }

        public Bug Bug { get; private set; }
    }
}