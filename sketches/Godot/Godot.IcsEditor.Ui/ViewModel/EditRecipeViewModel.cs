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
using Godot.PmsModel.Entities;
using Godot.PmsModel.Queries;
using NHibernate;

namespace Godot.IcsEditor.Ui.ViewModel
{
    public class RecipeChangedEvent : CompositePresentationEvent<Recipe>
    {
    }

    public class EditRecipeViewModel : ResponsibleWorkspaceViewModel, IDataErrorInfo
    {
        private EditRecipe _editRecipe;

        public EditRecipeViewModel(IDbConversation dbConversation, IEventAggregator eventAggregator)
            : base(dbConversation, eventAggregator)
        {
            ContainedObject = 0;
            _editRecipe = new EditRecipe(new Recipe());
            PreloadLists();
            base.DisplayName = Strings.ViewModel_SingleRecipeViewModel_NewItem;
        }

        public EditRecipeViewModel(int entityId, IDbConversation dbConversation, IEventAggregator eventAggregator) 
            : base(dbConversation, eventAggregator)
        {
            ContainedObject = entityId;
            DbConversation.UsingTransaction(() =>
            {
                _editRecipe =
                    new EditRecipe(
                        DbConversation.GetById<Recipe>(entityId));
                PreloadLists();
            });
            base.DisplayName = string.Format(Strings.ViewModel_SingleRecipeViewModel_EditItem, _editRecipe.Plu);
        }

        void PreloadLists()
        {
            AllRecipeableItems = DbConversation.Query(new AllRecipeableItemsQuery()).ToList();
            AllRecipeUnits = DbConversation.Query(new AllRecipeUnitsQuery()).ToList();
            AllSalesItems = DbConversation.Query(new AllSalesItemsQuery()).ToList();

            RecipeItems = new ObservableCollection<SingleRecipeItemViewModel>(
                _editRecipe.RecipeItems
                .Select(x => new SingleRecipeItemViewModel(x)));
            RecipeItems.CollectionChanged += OnRecipeItemsChanged;
        }

        public List<RecipeableItem> AllRecipeableItems { get; private set; }
        public List<Unit> AllRecipeUnits { get; private set; }
        public List<SalesItem> AllSalesItems { get; private set; }
        public ObservableCollection<SingleRecipeItemViewModel> RecipeItems { get; private set; }


        public int Plu
        {
            get { return _editRecipe.Plu; }
            set
            {
                if (value == _editRecipe.Plu)
                    return;
                _editRecipe.Plu = value;
                base.OnPropertyChanged("Plu");
            }
        }
        public string Id { get { return _editRecipe.Id == 0 ? Strings.ViewModel_SingleRecipeViewModel_NewId : _editRecipe.Id.ToString(); } }

        public bool RecipeItemsSelected
        {
            get { return false; } // AllProductionItems.FirstOrDefault(pi => pi.IsSelected) != null ? true : false; }
        }

        #region Commands

        ActionCommand _removeRecipeItem;
        public ICommand RemoveRecipeItem
        {
            get
            {
                return _removeRecipeItem ?? (_removeRecipeItem = new ActionCommand(param => RemoveProductionItem(), param => RecipeItemsSelected));
            }
        }

        public void RemoveProductionItem()
        {/*
            for (var i = AllProductionItems.Count - 1; i >= 0; i--)
            {
                if (AllProductionItems.SourceCollection.[i].IsSelected)
                {
                    AllProductionItems[i].Remove();
                }
            }*/
        }

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

        public void Remove()
        {/*
            try
            {
                _repRecipes.Delete(_editRecipe.Id);
            }
            catch (GenericADOException)
            {
                MessageBox.Show(Strings.Error_Recipes_CannotDelete);
            }*/
        }

        public bool Save()
        {
            try
            {
                DbConversation.UsingTransaction(() => DbConversation.InsertObjectOnCommit(_editRecipe.Recipe));
                base.OnPropertyChanged("DisplayName");
                EventAggregator.GetEvent<RecipeChangedEvent>().Publish(_editRecipe.Recipe);
                return true;
            }
            catch (StaleObjectStateException)
            {
                MessageBox.Show("Object was changed outside this dialog."); //Strings.Error_PurchaseItems_CannotSave);
                return true;
            }
            catch
            {
                MessageBox.Show(Strings.Error_Recipes_CannotSave);
                return false;
            }
        }

        #endregion

        #region Private Helpers

        bool CanSave
        {
            get { return _editRecipe.IsValid; }
        }

        void OnRecipeItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (SingleRecipeItemViewModel rtvm in e.NewItems)
                    _editRecipe.Recipe.AddRecipeItem(rtvm.UnderlayingObject());

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (SingleRecipeItemViewModel rtvm in e.OldItems)
                    _editRecipe.Recipe.RemoveRecipeItem(rtvm.UnderlayingObject());
        }
        
        #endregion

 
        #region IDataErrorInfo Members

        public string this[string propertyName]
        {
            get
            {
                var error = (_editRecipe as IDataErrorInfo)[propertyName];
                // Dirty the commands registered with CommandManager,
                // such as our Save command, so that they are queried
                // to see if they can execute now.
                CommandManager.InvalidateRequerySuggested();
                return error;
            }
        }

        public string Error
        {
            get { return (_editRecipe as IDataErrorInfo).Error; }
        }

        #endregion
    }
}