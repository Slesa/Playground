using System;
using System.ComponentModel.Composition;
using Caliburn.Micro;

namespace MediaOwl.Core
{
    /// <summary>
    /// An <see cref="IResult"/>, that takes a Type of <see cref="IDialog"/> (or the instance itself), 
    /// locates/instanciates it (<see cref="locateDialog"/>), 
    /// configures it (<see cref="Configured"/>) 
    /// and activates it via the <see cref="WindowManager"/>.
    /// </summary>
    /// <typeparam name="TDialog">The Dialog-Type</typeparam>
    public class OpenDialogResult<TDialog> : IResult
        where TDialog : IDialog
    {
        private IDialog dialog;
        private Action<TDialog> onConfigure;

        private readonly Func<ActionExecutionContext, TDialog> locateDialog =
            c => IoC.Get<TDialog>();

        public bool? DialogResult { get; set; }

        [Import]
        public IWindowManager WindowManager { get; set; }

        public OpenDialogResult() { }

        public OpenDialogResult(TDialog dialog)
        {
            locateDialog = c => dialog;
        }

        public OpenDialogResult<TDialog> Configured(Action<TDialog> configure)
        {
            onConfigure = configure;
            return this;
        }

        public void Execute(ActionExecutionContext context)
        {
            var tdialog = locateDialog(context);

            if (onConfigure != null)
                onConfigure(tdialog);

            dialog = tdialog;
            // Needs to be an IDialog so we can hook on the Completed event
            if (dialog == null)
                throw new InvalidOperationException();

            dialog.Completed += OnDialogCompleted;

            WindowManager.ShowDialog(dialog);
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

            dialog = sender as IDialog;

            if (null == dialog) throw new ArgumentException("sender");

            dialog.Completed -= OnDialogCompleted;
            //Application.Current.RootVisual.SetValue(Control.IsEnabledProperty, True)
            Completed(this, new ResultCompletionEventArgs
            {
                WasCancelled = false,
                Error = e.Error
            });
        }

        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };
    }

    public class DialogResultEventArgs : EventArgs
    {
        public Exception Error;
        public bool? DialogResult;
    }

    public interface IDialog
    {
        event EventHandler<DialogResultEventArgs> Completed;
    }
}