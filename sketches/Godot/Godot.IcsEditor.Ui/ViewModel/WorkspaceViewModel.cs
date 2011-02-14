using System;
using System.Windows.Input;
using Godot.IcsEditor.Ui.Events;
using Godot.Model;

namespace Godot.IcsEditor.Ui.ViewModel
{
    public abstract class ResponsibleWorkspaceViewModel : WorkspaceViewModel
    {
        public object ContainedObject { get; set; }

        protected ResponsibleWorkspaceViewModel(IDbConversation dbConversation, IEventAggregator eventAggregator) : base(dbConversation, eventAggregator)
        {
        }

        public override bool IsResponsibleFor(object askedObject)
        {
            return (ContainedObject != null && askedObject!=null && ContainedObject.ToString() == askedObject.ToString());
        }
    }

    public abstract class WorkspaceViewModel : ViewModelBase
    {

        protected WorkspaceViewModel(IDbConversation dbConversation, IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            DbConversation = dbConversation;
        }

        public virtual bool IsResponsibleFor(object askedObject)
        {
            return false;
        }

        public IEventAggregator EventAggregator { get; private set; }
        public IDbConversation DbConversation { get; private set; }

        ActionCommand _showOptionsCommand;
        public ICommand ShowOptionsCommand
        {
            get { return _showOptionsCommand ?? (_showOptionsCommand = new ActionCommand(param => OnShowOptions())); }
        }

        static void OnShowOptions()
        {
            /*
            var dlg = new DlgSettings();
            dlg.DataContext = new DlgSettingsViewModel(dlg);
            dlg.ShowDialog();
             * */
        }

        #region CloseCommand

        ActionCommand _closeCommand;
        /// <summary>
        /// Returns the command that, when invoked, attempts
        /// to remove this workspace from the user interface.
        /// </summary>
        public ICommand CloseCommand
        {
            get { return _closeCommand ?? (_closeCommand = new ActionCommand(param => OnRequestClose())); }
        }

        #endregion // CloseCommand

        #region RequestClose [event]

        /// <summary>
        /// Raised when this workspace should be removed from the UI.
        /// </summary>
        public event EventHandler RequestClose;

        void OnRequestClose()
        {
            EventHandler handler = this.RequestClose;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        #endregion // RequestClose [event]

        protected new virtual void OnDispose()
        {
            if( DbConversation!=null )
                DbConversation.Dispose();
            base.OnDispose();
        }
    }
}