using System;

namespace NightOwl.Core
{
    public class DialogResultEventArgs : EventArgs
    {
        public Exception Error;
        public bool? DialogResult;
    }
}