using System;

namespace MediaOwl.Core
{
    public class DialogResultEventArgs : EventArgs
    {
        public Exception Error;
        public bool? DialogResult;
    }
}