using Caliburn.Micro;

namespace Lucifer.Ums.Editor
{
    public interface IUmsModule : IScreen
    {
        string ModuleName { get; }
        string IconFileName { get; }
        string ToolTip { get; }

        Conductor<IScreen>.Collection.OneActive ScreenManager { get; set; }
    }
}