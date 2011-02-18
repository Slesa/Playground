using System;
using System.ComponentModel.Composition;
using Caliburn.Micro;

namespace NightHawkSL.Ui.Core
{
    public class OpenDialogResult<TDialog> : IResult
        where TDialog : IDialog
    {
        private IDialog _dialog;
        private Action<TDialog> _onConfigure;

        private readonly Func<ActionExecutionContext, TDialog> _locateDialog = c => IoC.Get<TDialog>();

        public bool? DialogResult { get; set; }

        [Import]
        public IWindowManager WindowManager { get; set; }

        public OpenDialogResult() { }

        public OpenDialogResult(TDialog dialog)
        {
            _locateDialog = c => dialog;
        }

        public OpenDialogResult<TDialog> Configured(Action<TDialog> configure)
        {
            _onConfigure = configure;
            return this;
        }

        public void Execute(ActionExecutionContext context)
        {
            var tdialog = _locateDialog(context);

            if (_onConfigure != null)
                _onConfigure(tdialog);

            _dialog = tdialog;
            // Needs to be an IDialog so we can hook on the Completed event
            if (_dialog == null)
                throw new InvalidOperationException();

            _dialog.Completed += OnDialogCompleted;

            WindowManager.ShowDialog(_dialog);
        }

        void OnDialogCompleted(object sender, DialogResultEventArgs e)
        {
            DialogResult = e.DialogResult;

            //var screen = sender as Screen;
            //if (null == screen) throw new ArgumentException("sender");
            //screen.CanClose(a =>
            //{
            //    if (!a) return;
            //});
            //screen.TryClose();

            _dialog = sender as IDialog;

            if (null == _dialog) throw new ArgumentException("sender");

            _dialog.Completed -= OnDialogCompleted;
            //Application.Current.RootVisual.SetValue(Control.IsEnabledProperty, True)
            Completed(this, new ResultCompletionEventArgs
            {
                WasCancelled = false,
                Error = e.Error
            });
        }

        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };
    }
}