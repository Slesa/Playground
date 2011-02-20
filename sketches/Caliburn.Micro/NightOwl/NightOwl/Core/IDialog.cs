using System;

namespace NightOwl.Core
{
    public interface IDialog
    {
        event EventHandler<DialogResultEventArgs> Completed;
    }
}