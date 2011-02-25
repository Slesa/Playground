using Caliburn.Micro;

namespace NightOwl.Core
{
    public interface IModule : IScreen
    {
        string IconFileName { get; }
        string ToolTip { get; }
    }
}