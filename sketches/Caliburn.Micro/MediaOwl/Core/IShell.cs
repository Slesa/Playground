using Caliburn.Micro;

namespace MediaOwl.Core
{
    /// <summary>
    /// This interface is just a marker for the <see cref="MediaOwlBootstrapper"/>.
    /// </summary>
    public interface IShell : IConductor
    {
        bool HasActiveDialog { get; set; }
    }
}