using System;

namespace NightHawkSL.Ui.Core
{
    public class DialogResultEventArgs : EventArgs
    {
        public Exception Error;
        public bool? DialogResult;
    }
}