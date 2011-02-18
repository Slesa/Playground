using Caliburn.Micro;

namespace NightHawkSL.Ui.Core
{
    public interface IChildScreen : IScreen
    {
        string ScreenId { get; }
    }

    public interface IChildScreen<TParent> : IChildScreen
        where TParent : IConductor
    {
        int? Order { get; }
    }
}