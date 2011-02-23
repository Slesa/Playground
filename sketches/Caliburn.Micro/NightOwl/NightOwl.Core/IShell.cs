using Caliburn.Micro;

namespace NightOwl.Core
{
    public interface IShell : IConductor
    {
        bool HasActiveDialog { get; set; }
    }
}