using System;

namespace NightHawkSL.Core
{
    public class DialogResultEventArgs : EventArgs
    {
        public Exception Error;
        public bool? DialogResult;
    }
}