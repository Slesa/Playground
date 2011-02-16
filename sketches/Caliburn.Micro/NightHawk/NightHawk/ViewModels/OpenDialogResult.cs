using System;
using System.ComponentModel.Composition;
using Caliburn.Micro;
using NightHawk.Core;
using Action = Caliburn.Micro.Action;

namespace NightHawk.ViewModels
{
    public class OpenDialogResult<TDialog> : IResult where TDialog : IDialog
    {
        IDialog dialog;
        Action<TDialog> _onConfigure;

        readonly Func<ActionExecutionContext, TDialog> _locateDialog = c => IoC.Get<TDialog>();

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

            dialog = tdialog;
            if( dialog==null )
                throw new InvalidOperationException();

            dialog.Completed += OnDialogCompleted;

            WindowManager.ShowDialog(dialog);
        }

        void OnDialogCompleted(object sender, DialogResultEventArgs e)
        {
            DialogResult = e.DialogResult;

            dialog = sender as IDialog;
            if( dialog==null ) throw new ArgumentException("sender");

            dialog.Completed -= OnDialogCompleted;
            Completed(this, new ResultCompletionEventArgs
                {
                    WasCancelled = false,
                    Error = e.Error
                });
        }

        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };
    }
}