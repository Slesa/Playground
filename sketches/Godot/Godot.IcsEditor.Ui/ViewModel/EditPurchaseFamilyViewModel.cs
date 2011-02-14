using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Godot.IcsEditor.Ui.Events;
using Godot.IcsEditor.Ui.Localization;
using Godot.IcsEditor.Ui.Model;
using Godot.IcsModel.Entities;
using Godot.Model;
using NHibernate;

namespace Godot.IcsEditor.Ui.ViewModel
{
    public class PurchaseFamilyChangedEvent : CompositePresentationEvent<PurchaseFamily>
    {
    }

    public class EditPurchaseFamilyViewModel : ResponsibleWorkspaceViewModel, IDataErrorInfo
    {
        private EditPurchaseFamily _editPurchaseFamily;

        public EditPurchaseFamilyViewModel(IDbConversation dbConversation, IEventAggregator eventAggregator)
            : base(dbConversation, eventAggregator)
        {
            ContainedObject = 0;
            _editPurchaseFamily = new EditPurchaseFamily(new PurchaseFamily());
            base.DisplayName = Strings.ViewModel_SinglePurchaseFamilyViewModel_NewItem;
        }

        public EditPurchaseFamilyViewModel(int entityId, IDbConversation dbConversation, IEventAggregator eventAggregator)
            : base(dbConversation, eventAggregator)
        {
            ContainedObject = entityId;
            DbConversation.UsingTransaction(() =>
            {
                _editPurchaseFamily =
                    new EditPurchaseFamily(
                        DbConversation.GetById<PurchaseFamily>(entityId));
            });
            base.DisplayName = _editPurchaseFamily.Name;
        }

        public string Name
        {
            get { return _editPurchaseFamily.Name; }
            set
            {
                if (value == _editPurchaseFamily.Name)
                    return;
                _editPurchaseFamily.Name = value;
                base.OnPropertyChanged("Name");
            }
        }

        public string Id { get { return _editPurchaseFamily.Id == 0 ? Strings.ViewModel_SinglePurchaseFamilyViewModel_NewId : _editPurchaseFamily.Id.ToString(); } }

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
                                               if( Save() ) CloseCommand.Execute(param);
                                           },
                                           param => CanSave
                                           ));
            }
        }

        #endregion

        #region Public Methods
/*
        public void Remove()
        {
            try
            {
                _repPurchaseFamily.Delete(_editFamily.Id);
            }
            catch (GenericADOException)
            {
                MessageBox.Show(Strings.Error_PurchaseFamilies_CannotDelete);
            }
        }
*/
        public bool Save()
        {
            try
            {
                DbConversation.UsingTransaction(() => DbConversation.InsertObjectOnCommit(_editPurchaseFamily.PurchaseFamily));
                base.OnPropertyChanged("DisplayName");
                EventAggregator.GetEvent<PurchaseFamilyChangedEvent>().Publish(_editPurchaseFamily.PurchaseFamily);
                return true;
            }
            catch (StaleObjectStateException)
            {
                MessageBox.Show("Object was changed outside this dialog."); //Strings.Error_PurchaseItems_CannotSave);
                return true;
            }
            catch
            {
                MessageBox.Show(Strings.Error_PurchaseFamilies_CannotSave);
                return false;
            }
        }

        #endregion

        #region Private Helpers

        bool CanSave
        {
            get { return _editPurchaseFamily.IsValid; }
        }

        #endregion

        #region IDataErrorInfo Members

        public string this[string propertyName]
        {
            get
            {
                var error = (_editPurchaseFamily as IDataErrorInfo)[propertyName];
                CommandManager.InvalidateRequerySuggested();
                return error;
            }
        }

        public string Error
        {
            get { return (_editPurchaseFamily as IDataErrorInfo).Error; }
        }

        #endregion
    }
}