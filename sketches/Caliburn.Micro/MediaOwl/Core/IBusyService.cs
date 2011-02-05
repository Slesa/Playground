namespace MediaOwl.Core
{
    /// <summary>
    /// This interface marks a class as an <see cref="IBusyService"/>
    /// </summary>
    /// <remarks>
    /// This class is implemented in <see cref="DefaultBusyService"/>.
    /// This interface was taken from the Caliburn Framework.
    /// </remarks>
    public interface IBusyService
    {
        void MarkAsBusy(object sourceViewModel, object busyViewModel);
        void MarkAsNotBusy(object sourceViewModel);
    }
}