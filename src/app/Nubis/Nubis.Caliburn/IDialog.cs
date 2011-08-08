using System;

namespace Nubis.Caliburn
{
    public interface IDialog
    {
        event EventHandler<DialogResultEventArgs> Completed;
    }
}