using Caliburn.Micro;

namespace Nubis.Core
{
    public interface IModule : IScreen
    {
        string IconFileName { get; }
        string ToolTip { get; }
    }
}