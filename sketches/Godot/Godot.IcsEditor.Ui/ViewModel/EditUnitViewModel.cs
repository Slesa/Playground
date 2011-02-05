using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Godot.IcsEditor.Ui.Events;
using Godot.IcsEditor.Ui.Localization;
using Godot.IcsEditor.Ui.Model;
using Godot.IcsModel.Entities;
using Godot.IcsModel.Queries;
using Godot.Model;
using NHibernate;

namespace Godot.IcsEditor.Ui.ViewModel
{
    public class UnitChangedEvent : CompositePresentationEvent<Unit>
    {
    }

    public class EditUnitViewModel : ResponsibleWorkspaceViewModel, IDataErrorInfo
    {
        private EditUnit _editUnit;

        public EditUnitViewModel(IDbConversation dbConversation, IEventAggregator eventAggregator)
            : base(dbConversation, eventAggregator)
        {
            ContainedObject = 0;
            _editUnit = new EditUnit(new Unit());
            PreloadLists();
            base.DisplayName = Strings.ViewModel_SingleUnitViewModel_NewItem;
        }

        public EditUnitViewModel(int entityId, IDbConversation dbConversation, IEventAggregator eventAggregator)
            : base(dbConversation, eventAggregator)
        {
            ContainedObject = entityId;
            DbConversation.UsingTransaction(() =>
            {
                _editUnit =
                    new EditUnit(
                        DbConversation.GetById<Unit>(entityId));
                PreloadLists();
            });
            base.DisplayName = _editUnit.Name;
        }

        private void PreloadLists()
        {
            AllUnits = new List<Unit> {new Unit()};
            AllUnits.AddRange(DbConversation.Query(new AllUnitsQuery()).ToList());
            AllUnitTypes = DbConversation.Query(new AllUnitTypesQuery()).ToList();
        }

        public List<Unit> AllUnits { get; private set; }
        public List<UnitType> AllUnitTypes { get; private set; }

        public string Name
        {
            get { return _editUnit.Name; }
            set
            {
                if (value == _editUnit.Name)
                    return;
                _editUnit.Name = value;
                base.OnPropertyChanged("Name");
            }
        }

        public string Contraction
        {
            get { return _editUnit.Contraction; }
            set
            {
                if (value == _editUnit.Contraction)
                    return;
                _editUnit.Contraction = value;
                base.OnPropertyChanged("Contraction");
            }
        }

        /* Wenn editierbar
        UnitType _slectedUnitType;
        public UnitType SelectedUnitType
        {
            get { return _slectedUnitType; }
            set
            {
                if (value == SelectedUnitType)
                    return;
                _slectedUnitType = value;
                base.OnPropertyChanged("Parent");
                base.OnPropertyChanged("UnitType");
            }
        } */

        public UnitType UnitType
        {
            get { return _editUnit.UnitType; }
            set
            {
                if (value == _editUnit.UnitType)
                    return;
                _editUnit.UnitType = value;
                base.OnPropertyChanged("Parent");
                base.OnPropertyChanged("UnitType");
            }
        }

        /* Wnn editierbar
        Unit _selectedParent;
        public Unit SelectedParent
        {
            get { return _selectedParent;  }
            set
            {
                if( value==_selectedParent)
                    return;
                _selectedParent = value;
                base.OnPropertyChanged("Parent");
                base.OnPropertyChanged("UnitType");
            }
        } */

        public Unit Parent
        {
            get { return _editUnit.Parent; }
            set
            {
                if (value == _editUnit.Parent)
                    return;
 
                if (_editUnit.Parent != null)
                    _editUnit.Parent.RemoveChild(_editUnit.Unit);

                _editUnit.Parent = value.Id==0 ? null : value;

                if (_editUnit.Parent != null)
                    _editUnit.Parent.AddChild(_editUnit.Unit);

                base.OnPropertyChanged("Parent");
                base.OnPropertyChanged("UnitType");
            }
        }

        public string FactorToParent
        {
            get { return _editUnit.FactorToParent; }
            set
            {
                if (value == _editUnit.FactorToParent)
                    return;
                _editUnit.FactorToParent = value;
                base.OnPropertyChanged("FactorToParent");
            }
        }

        public bool Purchasing
        {
            get { return _editUnit.Purchasing; }
            set
            {
                if (value == _editUnit.Purchasing)
                    return;
                _editUnit.Purchasing = value;
                base.OnPropertyChanged("Purchasing");
            }
        }

        public bool Reciping
        {
            get { return _editUnit.Reciping; }
            set
            {
                if (value == _editUnit.Reciping)
                    return;
                _editUnit.Reciping = value;
                base.OnPropertyChanged("Reciping");
            }
        }

        public string Id { get { return _editUnit.Id == 0 ? Strings.ViewModel_SingleUnitViewModel_NewId : _editUnit.Id.ToString(); } }

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
                DbConversation.UsingTransaction(() => DbConversation.InsertObjectOnCommit(_editUnit.Unit));
                base.OnPropertyChanged("DisplayName");
                EventAggregator.GetEvent<UnitChangedEvent>().Publish(_editUnit.Unit);
                return true;
            }
            catch (StaleObjectStateException)
            {
                MessageBox.Show("Object was changed outside this dialog."); //Strings.Error_PurchaseItems_CannotSave);
                return true;
            }
            catch
            {
                MessageBox.Show(Strings.Error_Units_CannotSave);
                return false;
            }
        }

        #endregion

        #region Private Helpers

        bool CanSave
        {
            get { return _editUnit.IsValid; }
        }

        #endregion

        #region IDataErrorInfo Members

        public string this[string propertyName]
        {
            get
            {
                var error = (_editUnit as IDataErrorInfo)[propertyName];
                // Dirty the commands registered with CommandManager,
                // such as our Save command, so that they are queried
                // to see if they can execute now.
                CommandManager.InvalidateRequerySuggested();
                return error;
            }
        }

        public string Error
        {
            get { return (_editUnit as IDataErrorInfo).Error; }
        }

        #endregion
    }
}