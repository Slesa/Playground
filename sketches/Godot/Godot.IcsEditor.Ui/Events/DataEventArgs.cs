using System;

namespace Godot.IcsEditor.Ui.Events
{
    public class DataEventArgs<TData> : EventArgs
    {
        readonly TData _value;

        public DataEventArgs(TData data)
        {
            _value = data;
        }

        public TData Value { get { return _value; } }
    }
}