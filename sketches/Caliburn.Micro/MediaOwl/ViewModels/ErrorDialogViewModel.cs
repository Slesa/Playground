using System;
using System.ComponentModel.Composition;
using Caliburn.Micro;
using MediaOwl.Core;

namespace MediaOwl.ViewModels
{
    [Export(typeof(ErrorDialogViewModel))]
    public class ErrorDialogViewModel : Screen, IDialog
    {
        public ErrorDialogViewModel()
        {
            DisplayName = "Error";
        }

        private string errorMessage;
        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                errorMessage = value;
                NotifyOfPropertyChange(() => ErrorMessage);
            }
        }

        public void WithError(string message)
        {
            ErrorMessage = message;
        }

        protected override void OnDeactivate(bool close)
        {
            Completed(this, new DialogResultEventArgs());
            base.OnDeactivate(close);
        }

        #region Implementation of IDialog

        public event EventHandler<DialogResultEventArgs> Completed;

        #endregion
    }
}