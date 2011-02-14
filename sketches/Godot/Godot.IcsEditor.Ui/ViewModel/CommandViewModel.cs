using System;
using System.Drawing;
using System.Windows.Input;

namespace Godot.IcsEditor.Ui.ViewModel
{
    public class CommandViewModel : ViewModelBase
    {
        public CommandViewModel(string displayName, string icon, ICommand command)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            base.DisplayName = displayName;
            IconFileName = icon;
            Command = command;
        }

        public CommandViewModel(string displayName, ICommand command)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            base.DisplayName = displayName;
            Command = command;
        }

        public string IconFileName { get; set; }
        public ICommand Command { get; private set; }
    }
}