using System;

namespace Godot.IcsEditor.Ui.ViewModel
{
    public class ViewModelEditEventArgs : EventArgs
    {
        public int EntitiyId { get; private set; }

        public ViewModelEditEventArgs(int entitiyId)
        {
            EntitiyId = entitiyId;
        }
    }
}