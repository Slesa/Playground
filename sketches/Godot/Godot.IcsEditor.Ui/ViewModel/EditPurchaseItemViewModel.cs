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
    public class PurchaseItemChangedEvent : CompositePresentationEvent<PurchaseItem>
    {
    }

    public class EditPurchaseItemViewModel : ResponsibleWorkspaceViewModel, IDataErrorInfo
    {
        private EditPurchaseItem _editPurchaseItem;

        public EditPurchaseItemViewModel(IDbConversation dbConversation, IEventAggregator eventAggregator)
            : base(dbConversation, eventAggregator)
        {
            ContainedObject = 0;
            _editPurchaseItem = new EditPurchaseItem(new PurchaseItem());
            PreloadLists();
            base.DisplayName = Strings.ViewModel_SinglePurchaseItemViewModel_NewItem;
        }

        public EditPurchaseItemViewModel(int entityId, IDbConversation dbConversation, IEventAggregator eventAggregator)
            : base(dbConversation, eventAggregator)
        {
            ContainedObject = entityId;
            DbConversation.UsingTransaction(() =>
            {
                _editPurchaseItem =
                    new EditPurchaseItem(
                        DbConversation.GetById<PurchaseItem>(entityId));
                PreloadLists();
            });
            base.DisplayName = _editPurchaseItem.Name;
        }

        /*
        public CollectionView AllSalesItems
        {
            get { return new CollectionView(_repSalesItems.FindAll()); }
        }*/

        void PreloadLists()
        {
            AllFamilies = DbConversation.Query(new AllPurchaseFamiliesQuery()).ToList();
            AllPurchaseUnits = DbConversation.Query(new AllPurchaseUnitsQuery()).ToList();
            AllRecipeUnits = DbConversation.Query(new AllRecipeUnitsQuery()).ToList(); 
        }

        public List<PurchaseFamily> AllFamilies { get; private set; }
        public List<Unit> AllPurchaseUnits { get; private set; }
        public List<Unit> AllRecipeUnits { get; private set; }

        public string Name
        {
            get { return _editPurchaseItem.Name; }
            set
            {
                if (value == _editPurchaseItem.Name)
                    return;
                _editPurchaseItem.Name = value;
                base.OnPropertyChanged("Name");
            }
        }

        public PurchaseFamily PurchaseFamily
        {
            get { return _editPurchaseItem.PurchaseFamily; }
            set
            {
                if (value == _editPurchaseItem.PurchaseFamily)
                    return;
                _editPurchaseItem.PurchaseFamily = value;
                base.OnPropertyChanged("PurchaseFamily");
            }
        }

        public Unit PurchaseUnit
        {
            get { return _editPurchaseItem.PurchaseUnit; }
            set
            {
                if (value == _editPurchaseItem.PurchaseUnit)
                    return;
                _editPurchaseItem.PurchaseUnit = value;
                base.OnPropertyChanged("PurchaseUnit");
            }
        }

        public Unit RecipeUnit
        {
            get { return _editPurchaseItem.RecipeUnit; }
            set
            {
                if (value == _editPurchaseItem.RecipeUnit)
                    return;
                _editPurchaseItem.RecipeUnit = value;
                base.OnPropertyChanged("RecipeUnit");
            }
        }

        public string Id { get { return _editPurchaseItem.Id == 0 ? Strings.ViewModel_SinglePurchaseItemViewModel_NewId : _editPurchaseItem.Id.ToString(); } }

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
                _repProductionItems.Delete(_editItem.Id);
            }
            catch (GenericADOException)
            {
                MessageBox.Show(Strings.Error_PurchaseItems_CannotDelete);
            }
        }*/

        public bool Save()
        {
           try
            {
                DbConversation.UsingTransaction(() => DbConversation.InsertObjectOnCommit(_editPurchaseItem.PurchaseItem));
                base.OnPropertyChanged("DisplayName");
                EventAggregator.GetEvent<PurchaseItemChangedEvent>().Publish(_editPurchaseItem.PurchaseItem);
                return true;
            }
            catch (StaleObjectStateException)
            {
                MessageBox.Show("Object was changed outside this dialog."); //Strings.Error_PurchaseItems_CannotSave);
                return true;
            }
            catch
            {
                MessageBox.Show(Strings.Error_PurchaseItems_CannotSave);
                return false;
            }
        }

        #endregion

        #region Private Helpers

        bool CanSave
        {
            get { return _editPurchaseItem.IsValid; }
        }

        #endregion

        #region IDataErrorInfo Members

        public string this[string propertyName]
        {
            get
            {
                var error = (_editPurchaseItem as IDataErrorInfo)[propertyName];
                CommandManager.InvalidateRequerySuggested();
                return error;
            }
        }

        public string Error
        {
            get { return (_editPurchaseItem as IDataErrorInfo).Error; }
        }

        #endregion
    }
}