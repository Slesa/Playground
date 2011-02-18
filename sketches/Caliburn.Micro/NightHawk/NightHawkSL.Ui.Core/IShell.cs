using Caliburn.Micro;

namespace NightHawkSL.Ui.Core
{
    public interface IShell : IConductor
    {
        bool HasActiveDialog { get; set; }
    }
}