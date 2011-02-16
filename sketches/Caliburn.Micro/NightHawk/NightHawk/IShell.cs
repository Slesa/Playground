using Caliburn.Micro;

namespace NightHawk
{
    public interface IShell : IConductor
    {
        bool HasActiveDialog { get; set; }
    }
}