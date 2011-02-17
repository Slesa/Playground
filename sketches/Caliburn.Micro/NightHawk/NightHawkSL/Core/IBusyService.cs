namespace NightHawkSL.Core
{
    public interface IBusyService
    {
        void MarkAsBusy(object sourceViewModel, object busyViewModel);
        void MarkAsNotBusy(object sourceViewModel);
    }
}