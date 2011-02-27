using System;
using System.Diagnostics;
using Caliburn.Micro;

namespace Nubis.Core.Logging
{
    public class DebugLog : ILog
    {
        private readonly Type type;

        public DebugLog(Type type)
        {
            this.type = type;
        }

        public void Info(string format, params object[] args)
        {
            if(format.StartsWith("No bindable"))
                return;
            Debug.WriteLine("INFO: " + format, args);
        }

        public void Warn(string format, params object[] args)
        {
            Debug.WriteLine("WARN: " + format, args);
        }

        public void Error(Exception exception)
        {
            Debug.WriteLine("ERROR: {0}\n{1}", type.Name, exception);
        }
    }
}