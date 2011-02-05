using System.Collections.Generic;
using Godot.IcsEditor.Ui.ViewModel;

namespace Godot.IcsEditor.Ui.Commands
{
    public interface IEntityCommands
    {
        int Priority { get; }
        List<CommandViewModel> SupportedCommands { get; }
    }
}