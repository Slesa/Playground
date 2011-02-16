using System;

namespace NightHawk.Core
{
    public interface IDialog
    {
        event EventHandler<DialogResultEventArgs> Completed;
    }

    public class DialogResultEventArgs : EventArgs
    {
        public Exception Error;
        public bool? DialogResult;
    }
}