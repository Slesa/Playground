using System;
using System.Diagnostics;
using System.Globalization;
using Caliburn.Micro;

namespace Nubis.Caliburn
{
    public class CaliburnLogger : ILog
    {
        Type _type;

        public CaliburnLogger(Type type)
        {
            _type = type;
        }

        public void Info(string format, params object[] args)
        {
            Debug.WriteLine(CreateLogMessage(format, args), "INFO");
        }

        public void Warn(string format, params object[] args)
        {
            Debug.WriteLine(CreateLogMessage(format, args), "WARN");
        }

        public void Error(Exception exception)
        {
            Debug.WriteLine(CreateLogMessage(exception.ToString()), "ERROR");
        }

        string CreateLogMessage(string format, params object[] args)
        {
            return string.Format(CultureInfo.CurrentCulture,
                                 "[{0}] {1} - [{2}]",
                                 DateTime.Now.ToString("o", CultureInfo.CurrentCulture),
                                 string.Format(format, args),
                                 _type);
        }
    }
}
