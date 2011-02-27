using Caliburn.Micro;

namespace Nubis.Core
{
    public interface IShell : IConductor
    {
        bool HasActiveDialog { get; set; }
    }
}