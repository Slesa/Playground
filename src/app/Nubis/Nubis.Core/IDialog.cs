using System;

namespace Nubis.Core
{
    public interface IDialog
    {
        event EventHandler<DialogResultEventArgs> Completed;
    }
}