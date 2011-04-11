using Caliburn.Micro;

namespace Lucifer.Ics.Editor
{
    public interface IIcsModule : IScreen
    {
        string ModuleName { get; }
        string IconFileName { get; }
        string ToolTip { get; }

        Conductor<IScreen>.Collection.OneActive ScreenManager { get; set; }
    }
}