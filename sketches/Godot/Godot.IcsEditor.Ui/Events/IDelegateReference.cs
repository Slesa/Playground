using System;

namespace Godot.IcsEditor.Ui.Events
{
    public interface IDelegateReference
    {
        Delegate Target { get; }
    }
}