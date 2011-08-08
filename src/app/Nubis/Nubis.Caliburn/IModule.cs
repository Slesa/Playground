using Caliburn.Micro;

namespace Nubis.Caliburn
{
    public interface IModule : IScreen
    {
        string IconFileName { get; }
        string ToolTip { get; }
    }
}