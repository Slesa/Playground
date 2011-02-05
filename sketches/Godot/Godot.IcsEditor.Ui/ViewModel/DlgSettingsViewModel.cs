using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Input;
using Godot.IcsEditor.Localization;
using Godot.IcsEditor.Model;
using Godot.IcsEditor.View;

namespace Godot.IcsEditor.ViewModel
{
    // http://www.hanselman.com/blog/LearningWPFWithBabySmashConfigurationWithDataBinding.aspx
    // http://www.tutorials.de/net-wpf-silverlight/301280-wpf-validieren-einer-textboxeingabe.html

    public class DlgSettingsViewModel : ViewModelBase, IDataErrorInfo
    {
        readonly EditSettings _editSettings = new EditSettings();
        readonly DlgSettings _dialog;

        public DlgSettingsViewModel(DlgSettings dialog)
        {
            _dialog = dialog;
        }

        public string MatrixPath
        {
            get { return _editSettings.MatrixPath; }
            set
            {
                if (value == _editSettings.MatrixPath)
                    return;
                _editSettings.MatrixPath = value;
                OnPropertyChanged("MatrixPath");
            }
        }

        public string DbServer
        {
            get { return _editSettings.DbServer; }
            set
            {
                if (value == _editSettings.DbServer)
                    return;
                _editSettings.DbServer = value;
                OnPropertyChanged("DbServer");
            }
        }

        public bool DbUseLogin { get { return !DbIsIntegrated; } }
        public bool DbIsIntegrated
        {
            get { return _editSettings.DbIntegrated; }
            set
            {
                if (value == _editSettings.DbIntegrated)
                    return;
                _editSettings.DbIntegrated = value;
                OnPropertyChanged("DbIsIntegrated");
                OnPropertyChanged("DbUseLogin");
                OnPropertyChanged("DbUser");
            }
        }

        public string DbUser
        {
            get { return _editSettings.DbUser; }
            set
            {
                if (value == _editSettings.DbUser)
                    return;
                _editSettings.DbUser = value;
                OnPropertyChanged("DbUser");
            }
        }

        public string DbPassword
        {
            get { return _editSettings.DbPassword; }
            set
            {
                if (value == _editSettings.DbPassword)
                    return;
                _editSettings.DbPassword = value;
                OnPropertyChanged("DbPassword");
            }
        }

        #region Commands

        ActionCommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                return _saveCommand ??
                       (_saveCommand = new ActionCommand(
                                           param =>
                                           {
                                               Save(); _dialog.Close();
                                           },
                                           param => CanSave
                                           ));
            }
        }

        ActionCommand _cancelCommand;
        public ICommand CancelCommand
        {
            get { return _cancelCommand ?? (_cancelCommand = new ActionCommand(param => { Cancel(); _dialog.Close(); })); }
        }

        ActionCommand _searchMatrixPathCommand;
        public ICommand SearchMatrixPathCommand
        {
            get
            {
                return _searchMatrixPathCommand ??
                       (_searchMatrixPathCommand = new ActionCommand(param => SearchMatrixPath()));
            }
        }

        #endregion

        #region Public Methods

        void Cancel()
        {
            _editSettings.ResetSettings();
        }

        void Save()
        {
            _editSettings.TakeOverSettings();
        }

        void SearchMatrixPath()
        {
            var dlg = new FolderBrowserDialog();
            dlg.Description = Strings.View_DlgSettingsViewModel_BrowseMatrixPath;
            dlg.SelectedPath = _editSettings.MatrixPath;

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                MatrixPath = dlg.SelectedPath; // .FileName;
//                _editSettings.MatrixPath = dlg.SelectedPath; // .FileName;
        }

        #endregion

        #region Private Helpers

        bool CanSave
        {
            get { return _editSettings.IsValid; }
        }

        #endregion

        #region IDataErrorInfo Members

        public string this[string propertyName]
        {
            get
            {
                var error = (_editSettings as IDataErrorInfo)[propertyName];
                CommandManager.InvalidateRequerySuggested();
                return error;
            }
        }

        public string Error
        {
            get { return (_editSettings as IDataErrorInfo).Error; }
        }

        #endregion
    }
}