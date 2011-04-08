using Caliburn.Micro;

namespace Lucifer.Pms.Editor
{
    public interface IPmsModule : IScreen
    {
        string ModuleName { get; }
        string IconFileName { get; }
        string ToolTip { get; }
        
    }
}