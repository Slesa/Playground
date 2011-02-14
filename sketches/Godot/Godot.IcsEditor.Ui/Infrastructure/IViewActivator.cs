using Godot.IcsEditor.Ui.ViewModel;

namespace Godot.IcsEditor.Ui.Infrastructure
{
    public interface IViewActivator
    {
        TView Display<TView>() where TView : WorkspaceViewModel;
        TView Display<TView>(int id) where TView : WorkspaceViewModel;
        TView Display<TView>(object arguments) where TView : WorkspaceViewModel;
    }
}