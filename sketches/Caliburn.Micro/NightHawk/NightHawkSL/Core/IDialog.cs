using System;

namespace NightHawkSL.Core
{
    public interface IDialog
    {
        event EventHandler<DialogResultEventArgs> Completed;
    }
}