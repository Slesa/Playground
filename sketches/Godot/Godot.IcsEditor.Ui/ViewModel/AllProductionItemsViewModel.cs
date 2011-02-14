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
    public class AllProductionItemsViewModel : WorkspaceViewModel
    {
        const string ReportFile = @"Reports\ProductionItemList.rpt";

        readonly SubscriptionToken _productionItemInsertedToken;

        public AllProductionItemsViewModel(IDbConversation dbConversation, IEventAggregator eventAggregator) 
            : base(dbConversation, eventAggregator)
        {
            base.DisplayName = Strings.ViewModel_AllProductionItemsViewModel_DisplayName;

            CreateAllProductionItems();
            _productionItemInsertedToken = EventAggregator.GetEvent<ProductionItemChangedEvent>().Subscribe(OnProductionItemChanged);
        }

        public ObservableCollection<SingleProductionItemViewModel> AllProductionItems { get; private set; }

        void CreateAllProductionItems()
        {
            AllProductionItems = new ObservableCollection<SingleProductionItemViewModel>(DbConversation
                .Query(new AllProductionItemsQuery())
                .Select(x => new SingleProductionItemViewModel(x)));
        }

        public bool ItemSelected
        {
            get { return AllProductionItems.Where(pi => pi.IsSelected).Count() == 1 ? true : false; } 
        }

        public bool ItemsSelected
        {
            get { return AllProductionItems.FirstOrDefault(pi => pi.IsSelected) != null ? true : false; } 
        }

        protected override void OnDispose()
        {
            EventAggregator.GetEvent<ProductionItemChangedEvent>().Unsubscribe(_productionItemInsertedToken);
        }

        #region Commands

        ActionCommand _newCommand;
        public ICommand NewCommand
        {
            get { return _newCommand ?? (_newCommand = new ActionCommand(param => NewProductionItem())); }
        }

        void NewProductionItem()
        {
            EventAggregator.GetEvent<AddProductionItemEvent>().Publish(null);
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
                return _editCommand ?? (_editCommand = new ActionCommand(param => EditProductionItem(), param => ItemsSelected));
            }
        }

        void EditProductionItem()
        {
            AllProductionItems.Where(st => st.IsSelected).ToList().ForEach(
                item => EventAggregator.GetEvent<EditProductionItemEvent>().Publish(item.Id));
        }

        ActionCommand _removeCommand;
        public ICommand RemoveCommand
        {
            get
            {
                return _removeCommand ?? (_removeCommand = new ActionCommand(param => RemoveProductionItem(), param => ItemsSelected));
            }
        }

        void RemoveProductionItem()
        {
            var toDelete = AllProductionItems.Where(vm => vm.IsSelected).ToList();
            var msg = toDelete.Aggregate(Strings.ViewModel_AllPurchaseItemsViewModel_AskToDelete, (current, item) => current + ("\n" + item.DisplayName));
            if (MessageBox.Show(msg, Strings.ViewModel_AllPurchaseItemsViewModel_DeleteItems, MessageBoxButton.YesNo) != MessageBoxResult.Yes)
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
                MessageBox.Show(Strings.Error_ProductionItems_CannotDelete);
                return;
            }
            for (var i = toDelete.Count - 1; i >= 0; i--)
            {
                AllProductionItems.Remove(toDelete[i]);
            }
        }

        ActionCommand _printCommand;
        public ICommand PrintCommand
        {
            get
            {
                return _printCommand ?? (_printCommand = new ActionCommand(param => PrintProductionItem(), param => System.IO.File.Exists(ReportFile)));
            }
        }

        void PrintProductionItem()
        {
            EventAggregator.GetEvent<CrystalReportPrintEvent>().Publish(new CrystalReportPrintEventArgs(ReportFile, Strings.ViewModel_AllPurchaseItemsViewModel_PrintHeader));
        }

        #endregion


        void OnProductionItemChanged(ProductionItem productionItem)
        {
            var viewmodel = (from vm in AllProductionItems where vm.Id == productionItem.Id select vm).FirstOrDefault();
            if (viewmodel == null)
            {
                viewmodel = new SingleProductionItemViewModel(productionItem);
                AllProductionItems.Add(viewmodel);
            }
            else
            {
                viewmodel.ExchangeData(productionItem);
            }
            OnPropertyChanged("ItemSelected");
            OnPropertyChanged("ItemsSelected");
        }
        /*
        void OnProductionItemRemoved(object sender, EntityRemovedEventArgs<ProductionItem> e)
        {
            var viewModel = AllProductionItems.First(deletedItem => deletedItem.UnderlyingObject == e.RemovedEntity);
            AllProductionItems.Remove(viewModel);
            OnPropertyChanged("ItemSelected");
            OnPropertyChanged("ItemsSelected");
        }
        
        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (SingleProductionItemViewModel pivm in e.NewItems)
                    pivm.PropertyChanged += OnProductionItemViewModelPropertyChanged;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (SingleProductionItemViewModel pivm in e.OldItems)
                    pivm.PropertyChanged -= OnProductionItemViewModelPropertyChanged;
        }

        void OnProductionItemViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            const string isSelected = "IsSelected";

            // Make sure that the property name we're referencing is valid.
            // This is a debugging technique, and does not execute in a Release build.
            var vm = (sender as SingleProductionItemViewModel);
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
