namespace MediaOwl.Core
{
    /// <summary>
    /// A Shortcut-Class that creates <see cref="OpenChildResult&lt;TChild&gt;"/> or
    /// <see cref="OpenDialogResult&lt;TDialog&gt;"/> or
    /// <see cref="BusyResult"/>./>
    /// </summary>
    /// <remarks>This class is a part of the Show-Class of the Caliburn Framework</remarks>
    public static class Show
    {

        public static OpenChildResult<TChild> Child<TChild>()
        {
            return new OpenChildResult<TChild>();
        }

        public static OpenChildResult<TChild> Child<TChild>(TChild child)
        {
            return new OpenChildResult<TChild>(child);
        }

        public static BusyResult Busy(object busyViewModel = null)
        {
            return new BusyResult(true, busyViewModel);
        }

        public static OpenDialogResult<TDialog> Dialog<TDialog>()
            where TDialog : IDialog
        {
            return new OpenDialogResult<TDialog>();
        }

        public static OpenDialogResult<TDialog> Dialog<TDialog>(TDialog dialog)
            where TDialog : IDialog
        {
            return new OpenDialogResult<TDialog>(dialog);
        }

        public static BusyResult NotBusy(object busyViewModel = null)
        {
            return new BusyResult(false, busyViewModel);
        }
    }
}