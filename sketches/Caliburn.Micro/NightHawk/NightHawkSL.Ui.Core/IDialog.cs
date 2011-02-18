using System;

namespace NightHawkSL.Ui.Core
{
    public interface IDialog
    {
        event EventHandler<DialogResultEventArgs> Completed;
    }
}