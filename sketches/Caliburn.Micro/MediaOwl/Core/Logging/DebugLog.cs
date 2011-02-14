using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using Caliburn.Micro;

namespace MediaOwl.Core.Logging
{
    /// <summary>A Logger writing its logs to the debugger</summary>
    /// <seealso cref="Caliburn.Micro.ILog">Take a look at the Interface</seealso>
    /// <seealso cref="Caliburn.Micro.LogManager">This class can be used by the LogManager</seealso>
    /// <seealso cref="MediaOwlBootstrapper">Look at the ctor of the bootstrapper to see how the logger gets activated</seealso>
    [Export(typeof(ILog))]
    public class DebugLog : ILog
    {
        private readonly Type type;

        /// <summary>
        /// The Constructor 
        /// </summary>
        /// <param name="type">The type that calls this class</param>
        public DebugLog(Type type)
        {
            this.type = type;
        }

        /// <summary>
        /// Logs the message as info.
        /// </summary>
        /// <param name="format">A formatted message.</param>
        /// <param name="args">Parameters to be injected into the formatted message.</param>
        public void Info(string format, params object[] args)
        {
            if(format.StartsWith("No bindable"))
                return;
            Debug.WriteLine("INFO: " + format, args);
        }

        /// <summary>
        /// Logs the message as a warning.
        /// </summary>
        /// <param name="format">A formatted message.</param>
        /// <param name="args">Parameters to be injected into the formatted message.</param>
        public void Warn(string format, params object[] args)
        {
            Debug.WriteLine("WARN: " + format, args);
        }

        /// <summary>
        /// Logs the exception. Logs the ctor-param <see cref="type">type</see> as well.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public void Error(Exception exception)
        {
            Debug.WriteLine("ERROR: {0}\n{1}", type.Name, exception);
        }
    }
}