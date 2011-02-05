using Caliburn.Micro;

namespace MediaOwl.Core
{
    /// <summary>
    /// This interface extends <see cref="IScreen"/> with a <see cref="ScreenId"/>.
    /// It is used to identify existing, non-shared (<see cref="System.ComponentModel.Composition.PartCreationPolicyAttribute"/>) childscreens.
    /// </summary>
    public interface IChildScreen : IScreen
    {
        string ScreenId { get; }
    }

    /// <summary>
    /// This interface extends <see cref="IChildScreen"/> with a nullable Order-Property.
    /// </summary>
    /// <typeparam name="TParent">The type of the Parent, must be an <see cref="IConductor"/></typeparam>
    public interface IChildScreen<TParent> : IChildScreen
        where TParent : IConductor
    {
        int? Order { get; }
    }
}