using System;

namespace Nubis.Caliburn
{
    public class DialogResultEventArgs : EventArgs
    {
        public Exception Error;
        public bool? DialogResult;
    }
}