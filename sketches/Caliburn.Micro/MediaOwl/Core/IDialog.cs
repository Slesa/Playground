using System;

namespace MediaOwl.Core
{
    public interface IDialog
    {
        event EventHandler<DialogResultEventArgs> Completed;
    }
}