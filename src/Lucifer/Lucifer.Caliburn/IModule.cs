using System.Collections.Generic;
using Caliburn.Micro;

namespace Lucifer.Caliburn
{
    public interface IModule : IScreen
    {
        string ModuleName { get; }
        string IconFileName { get; }
        string ToolTip { get; }

        IEnumerable<IModule> SubModules { get; }
    }
}