using System;
using Caliburn.Micro;
using NightOwl.Core;
using NightOwl.Resources;

namespace NightOwl.ViewModels
{
    public class ErrorDialogViewModel : Screen, IDialog
    {
        public ErrorDialogViewModel()
        {
            DisplayName = AppStrings.Error;
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
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

        public event EventHandler<DialogResultEventArgs> Completed;

    }
}