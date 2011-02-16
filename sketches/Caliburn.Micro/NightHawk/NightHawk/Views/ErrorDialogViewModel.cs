using System;
using System.ComponentModel.Composition;
using Caliburn.Micro;
using NightHawk.Core;
using NightHawk.Resources;

namespace NightHawk.Views
{
    [Export(typeof(ErrorDialogViewModel))]
    public class ErrorDialogViewModel : Screen, IDialog
    {
        public ErrorDialogViewModel()
        {
            DisplayName = AppStrings.StringError;
        }

        string _errorMessage;
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

        public event EventHandler<DialogResultEventArgs> Completed = delegate { };
    }
}