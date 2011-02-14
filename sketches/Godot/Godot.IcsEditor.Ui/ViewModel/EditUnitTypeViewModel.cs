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
    public class UnitTypeChangededEvent : CompositePresentationEvent<UnitType>
    {
    }

    public class EditUnitTypeViewModel : ResponsibleWorkspaceViewModel, IDataErrorInfo
    {
        private EditUnitType _editUnitType;

        public EditUnitTypeViewModel(IDbConversation dbConversation, IEventAggregator eventAggregator)
            : base(dbConversation, eventAggregator)
        {
            ContainedObject = 0;
            _editUnitType = new EditUnitType(new UnitType());
            base.DisplayName = Strings.ViewModel_SingleUnitTypeViewModel_NewItem;
        }

        public EditUnitTypeViewModel(int entityId, IDbConversation dbConversation, IEventAggregator eventAggregator)
            : base(dbConversation, eventAggregator)
        {
            ContainedObject = entityId;
            DbConversation.UsingTransaction(() =>
            {
                _editUnitType =
                    new EditUnitType(
                        DbConversation.GetById<UnitType>(entityId));
            });
            base.DisplayName = _editUnitType.Name;
        }

        public string Name
        {
            get { return _editUnitType.Name; }
            set
            {
                if (value == _editUnitType.Name)
                    return;
                _editUnitType.Name = value;
                base.OnPropertyChanged("Name");
            }
        }

        public string Id { get { return _editUnitType.Id == 0 ? Strings.ViewModel_SingleUnitTypeViewModel_NewId : _editUnitType.Id.ToString(); } }

        #region Commands

        ActionCommand _saveCommand;

        public ICommand SaveCommand
        {
            get
            {
                return _saveCommand ??
                       (_saveCommand = new ActionCommand(param =>
                       {
                           if( Save() ) CloseCommand.Execute(param);
                       }, param => CanSave));
            }
        }

        #endregion

        #region Public Methods

        public void Remove()
        {/*
            var query = from c in _repUnits.FindAll() where c.Parent == UnderlyingObject select c;
            var parent = query.FirstOrDefault();
            if (parent != null)
            {
                MessageBox.Show(string.Format(CultureInfo.CurrentCulture, "This unit is parent of \"{0}\"and can therefore not be removed.", parent.Name));
                return;
            }

            try
            {
                _repUnits.Delete(_editUnit.Id);
            }
            catch (GenericADOException)
            {
                MessageBox.Show(Strings.Error_Units_CannotDelete);
            }*/
        }

        public bool Save()
        {
            try
            {
                DbConversation.UsingTransaction(() => DbConversation.InsertObjectOnCommit(_editUnitType.UnitType));
                base.OnPropertyChanged("DisplayName");
                EventAggregator.GetEvent<UnitTypeChangededEvent>().Publish(_editUnitType.UnitType);
                return true;
            }
            catch (StaleObjectStateException)
            {
                MessageBox.Show("Object was changed outside this dialog."); //Strings.Error_PurchaseItems_CannotSave);
                return true;
            }
            catch
            {
                MessageBox.Show(Strings.Error_UnitTypes_CannotSave);
                return false;
            }
        }

        #endregion

        #region Private Helpers

        bool CanSave
        {
            get { return _editUnitType.IsValid; }
        }

        #endregion

        #region IDataErrorInfo Members

        public string this[string propertyName]
        {
            get
            {
                var error = (_editUnitType as IDataErrorInfo)[propertyName];
                // Dirty the commands registered with CommandManager,
                // such as our Save command, so that they are queried
                // to see if they can execute now.
                CommandManager.InvalidateRequerySuggested();
                return error;
            }
        }

        public string Error
        {
            get { return (_editUnitType as IDataErrorInfo).Error; }
        }

        #endregion
    }
        
}