using Caliburn.Micro;
namespace NightHawkSL.Core
{
    public interface IShell : IConductor
    {
        bool HasActiveDialog { get; set; }
    }
}