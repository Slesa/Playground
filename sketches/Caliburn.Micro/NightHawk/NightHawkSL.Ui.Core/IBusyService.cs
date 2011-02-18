namespace NightHawkSL.Ui.Core
{
    public interface IBusyService
    {
        void MarkAsBusy(object sourceViewModel, object busyViewModel);
        void MarkAsNotBusy(object sourceViewModel);
    }
}