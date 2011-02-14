using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Godot.IcsEditor.Ui.Commands;
using Godot.IcsEditor.Ui.Events;
using Godot.IcsEditor.Ui.Localization;
using Godot.IcsModel.Entities;
using Godot.IcsModel.Queries;
using Godot.Model;

namespace Godot.IcsEditor.Ui.ViewModel
{
    public class AllRecipesViewModel : WorkspaceViewModel
    {
        const string ReportFile = @"Reports\RecipeList.rpt";

        private readonly SubscriptionToken _recipeInsertedToken;

        public AllRecipesViewModel(IDbConversation dbConversation, IEventAggregator eventAggregator)
            : base(dbConversation, eventAggregator)
        {
            base.DisplayName = Strings.ViewModel_AllRecipesViewModel_DisplayName;

            CreateAllRecipes();
            _recipeInsertedToken = EventAggregator.GetEvent<RecipeChangedEvent>().Subscribe(OnRecipeChanged);
        }

        public ObservableCollection<SingleRecipeViewModel> AllRecipes { get; private set; }

        void CreateAllRecipes()
        {
            AllRecipes = new ObservableCollection<SingleRecipeViewModel>(DbConversation
                .Query(new AllRecipesQuery())
                .Select(x => new SingleRecipeViewModel(x)));
        }

        public bool ItemSelected
        {
            get { return AllRecipes.Where(rec => rec.IsSelected).Count() == 1 ? true : false; }
        }

        public bool ItemsSelected
        {
            get { return AllRecipes.FirstOrDefault(rec => rec.IsSelected) != null ? true : false; }
        }

        protected override void OnDispose()
        {
            EventAggregator.GetEvent<RecipeChangedEvent>().Unsubscribe(_recipeInsertedToken);
        }

        #region Commands

        ActionCommand _newCommand;
        public ICommand NewCommand
        {
            get { return _newCommand ?? (_newCommand = new ActionCommand(param => NewRecipe())); }
        }

        void NewRecipe()
        {
            EventAggregator.GetEvent<AddRecipeEvent>().Publish(null);
        }

        public ICommand DoubleClickCommand
        {
            get { return EditCommand; }
        }

        ActionCommand _editCommand;
        public ICommand EditCommand
        {
            get
            {
                return _editCommand ?? (_editCommand = new ActionCommand(param => EditRecipe(), param => ItemsSelected));
            }
        }

        public void EditRecipe()
        {
            AllRecipes.Where(st => st.IsSelected).ToList().ForEach(
                item => EventAggregator.GetEvent<EditRecipeEvent>().Publish(item.Id));
        }

        ActionCommand _removeCommand;
        public ICommand RemoveCommand
        {
            get
            {
                return _removeCommand ?? (_removeCommand = new ActionCommand(param => RemoveRecipe(), param => ItemsSelected));
            }
        }

        public void RemoveRecipe()
        {
            var toDelete = AllRecipes.Where(vm => vm.IsSelected).ToList();
            var msg = toDelete.Aggregate(Strings.ViewModel_AllRecipesViewModel_AskToDelete, (current, item) => current + ("\n" + DisplayName));
            if (MessageBox.Show(msg, Strings.ViewModel_AllRecipesViewModel_DeleteItems, MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;
            try
            {
                DbConversation.UsingTransaction(() =>
                {
                    foreach (var instance in toDelete)
                        DbConversation.Delete(instance.UnderlayingObject());
                });
            }
            catch
            {
                MessageBox.Show(Strings.Error_Recipes_CannotDelete);
                return;
            }
            for (var i = toDelete.Count - 1; i >= 0; i--)
            {
                AllRecipes.Remove(toDelete[i]);
            }
        }

        ActionCommand _printCommand;
        public ICommand PrintCommand
        {
            get
            {
                return _printCommand ?? (_printCommand = new ActionCommand(param => PrintRecipes(), param => System.IO.File.Exists(ReportFile)));
            }
        }

        void PrintRecipes()
        {
            EventAggregator.GetEvent<CrystalReportPrintEvent>().Publish(new CrystalReportPrintEventArgs(ReportFile, Strings.ViewModel_AllRecipesViewModel_PrintHeader));
        }

        #endregion

        void OnRecipeChanged(Recipe recipe)
        {
            var viewmodel = (from vm in AllRecipes where vm.Id == recipe.Id select vm).FirstOrDefault();
            if (viewmodel == null)
            {
                viewmodel = new SingleRecipeViewModel(recipe);
                AllRecipes.Add(viewmodel);
            }
            else
            {
                viewmodel.ExchangeData(recipe);
            }
            OnPropertyChanged("ItemSelected");
            OnPropertyChanged("ItemsSelected");
        }
/*
        void OnRecipeRemoved(object sender, EntityRemovedEventArgs<Recipe> e)
        {
            var viewModel = AllRecipes.First(deletedItem => deletedItem.UnderlyingObject == e.RemovedEntity);
            AllRecipes.Remove(viewModel);
            OnPropertyChanged("ItemSelected");
            OnPropertyChanged("ItemsSelected");
        }

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (SingleRecipeViewModel recvm in e.NewItems)
                    recvm.PropertyChanged += OnRecipeViewModelPropertyChanged;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (SingleRecipeViewModel recvm in e.OldItems)
                    recvm.PropertyChanged -= OnRecipeViewModelPropertyChanged;
        }

        void OnRecipeViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            const string isSelected = "IsSelected";

            // Make sure that the property name we're referencing is valid.
            // This is a debugging technique, and does not execute in a Release build.
            var vm = (sender as SingleRecipeViewModel);
            if( vm!=null )
                vm.VerifyPropertyName(isSelected);

            // When a customer is selected or unselected, we must let the
            // world know that the TotalSelectedSales property has changed,
            // so that it will be queried again for a new value.
            if (e.PropertyName == isSelected)
            {
                //this.OnPropertyChanged("TotalSelectedSales");
                OnPropertyChanged("ItemSelected");
                OnPropertyChanged("ItemsSelected");
            }
        }*/
    }
}