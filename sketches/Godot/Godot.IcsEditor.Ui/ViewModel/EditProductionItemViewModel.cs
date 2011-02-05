using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
    public class ProductionItemChangedEvent : CompositePresentationEvent<ProductionItem>
    {
    }

    public class EditProductionItemViewModel : ResponsibleWorkspaceViewModel, IDataErrorInfo
    {
        private EditProductionItem _editProductionItem;

        public EditProductionItemViewModel(IDbConversation dbConversation, IEventAggregator eventAggregator)
            : base(dbConversation, eventAggregator)
        {
            ContainedObject = 0;
            _editProductionItem = new EditProductionItem(new ProductionItem());
            PreloadLists();
            base.DisplayName = Strings.ViewModel_SingleProductionItemViewModel_NewItem;
        }

        public EditProductionItemViewModel(int entityId, IDbConversation dbConversation, IEventAggregator eventAggregator)
            : base(dbConversation, eventAggregator)
        {
            ContainedObject = entityId;
            DbConversation.UsingTransaction(() =>
                                            {
                                                _editProductionItem =
                                                    new EditProductionItem(
                                                        DbConversation.GetById<ProductionItem>(entityId));
                                                PreloadLists();
                                            });
            base.DisplayName = _editProductionItem.Name;
        }

        void PreloadLists()
        {
            AllRecipeableItems = DbConversation.Query(new AllRecipeableItemsQuery()).ToList();
            AllPurchaseUnits = DbConversation.Query(new AllPurchaseUnitsQuery()).ToList();
            AllRecipeUnits = DbConversation.Query(new AllRecipeUnitsQuery()).ToList();

            RecipeItems = new ObservableCollection<SingleRecipeItemViewModel>(
                _editProductionItem.RecipeItems
                .Select(x => new SingleRecipeItemViewModel(x)));
            RecipeItems.CollectionChanged += OnRecipeItemsChanged;
        }

        public List<RecipeableItem> AllRecipeableItems { get; private set; }
        public List<Unit> AllPurchaseUnits { get; private set; }
        public List<Unit> AllRecipeUnits { get; private set; }
        public ObservableCollection<SingleRecipeItemViewModel> RecipeItems { get; private set; }

        public string Name
        {
            get { return _editProductionItem.Name; }
            set
            {
                if (value == _editProductionItem.Name)
                    return;
                _editProductionItem.Name = value;
                base.OnPropertyChanged("Name");
            }
        }

        public Unit RecipeUnit
        {
            get { return _editProductionItem.RecipeUnit; }
            set
            {
                if (value == _editProductionItem.RecipeUnit)
                    return;
                _editProductionItem.RecipeUnit = value;
                base.OnPropertyChanged("RecipeUnit");
            }
        }

        public string Id { get { return _editProductionItem.Id == 0 ? Strings.ViewModel_SingleProductionItemViewModel_NewId : _editProductionItem.Id.ToString(); } }

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
                                               if (Save()) CloseCommand.Execute(param);
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
        }
*/
        public bool Save()
        {
            try
            {
                DbConversation.UsingTransaction(() => DbConversation.InsertObjectOnCommit(_editProductionItem.ProductionItem));
                base.OnPropertyChanged("DisplayName");
                EventAggregator.GetEvent<ProductionItemChangedEvent>().Publish(_editProductionItem.ProductionItem);
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
            get { return _editProductionItem.IsValid; }
        }

        void OnRecipeItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (SingleRecipeItemViewModel rtvm in e.NewItems)
                    _editProductionItem.ProductionItem.AddRecipeItem(rtvm.UnderlayingObject());

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (SingleRecipeItemViewModel rtvm in e.OldItems)
                    _editProductionItem.ProductionItem.RemoveRecipeItem(rtvm.UnderlayingObject());
        }

        #endregion

        #region IDataErrorInfo Members

        public string this[string propertyName]
        {
            get
            {
                var error = (_editProductionItem as IDataErrorInfo)[propertyName];
                CommandManager.InvalidateRequerySuggested();
                return error;
            }
        }

        public string Error
        {
            get { return (_editProductionItem as IDataErrorInfo).Error; }
        }

        #endregion
    }

}