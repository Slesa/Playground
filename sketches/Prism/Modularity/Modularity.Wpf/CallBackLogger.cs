using System;
using System.Collections.Generic;
using Microsoft.Practices.Prism.Logging;

namespace Modularity.Wpf
{
    public class CallbackLogger : ILoggerFacade
    {
        Queue<Tuple<string, Category, Priority>> _savedLogs = new Queue<Tuple<string, Category, Priority>>();

        Action<string, Category, Priority> _callback;
        public Action<string, Category, Priority> Callback
        {
            get { return _callback; }
            set { _callback = value; }
        }

        public void Log(string message, Category category, Priority priority)
        {
            if (Callback != null)
                Callback(message, category, priority);
            else
                _savedLogs.Enqueue(new Tuple<string, Category, Priority>(message, category, priority));
        }

        public void ReplaySavedLogs()
        {
            if (Callback == null) return;

            while (_savedLogs.Count > 0)
            {
                var log = _savedLogs.Dequeue();
                Callback(log.Item1, log.Item2, log.Item3);
            }
        }
    }
}