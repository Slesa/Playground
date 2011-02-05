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
    public class AllPurchaseItemsViewModel : WorkspaceViewModel
    {
        const string ReportFile = @"Reports\PurchaseItemList.rpt";

        private readonly SubscriptionToken _purchaseItemInsertedToken;

        public AllPurchaseItemsViewModel(IDbConversation dbConversation, IEventAggregator eventAggregator)
            : base(dbConversation, eventAggregator)
        {
            base.DisplayName = Strings.ViewModel_AllPurchaseItemsViewModel_DisplayName;

            CreateAllPurchaseItems();
            _purchaseItemInsertedToken = EventAggregator.GetEvent<PurchaseItemChangedEvent>().Subscribe(OnPurchaseItemChanged);
        }

        public ObservableCollection<SinglePurchaseItemViewModel> AllPurchaseItems { get; private set; }

        void CreateAllPurchaseItems()
        {
            AllPurchaseItems = new ObservableCollection<SinglePurchaseItemViewModel>(DbConversation
                .Query(new AllPurchaseItemsQuery())
                .Select(x => new SinglePurchaseItemViewModel(x)));
        }

        public bool ItemSelected
        {
            get { return AllPurchaseItems.Where(pf => pf.IsSelected).Count() == 1 ? true : false; }
        }

        public bool ItemsSelected
        {
            get { return AllPurchaseItems.FirstOrDefault(pf => pf.IsSelected) != null ? true : false; }
        }

        protected override void OnDispose()
        {
            EventAggregator.GetEvent<PurchaseItemChangedEvent>().Unsubscribe(_purchaseItemInsertedToken);
        }

        #region Commands

        ActionCommand _newCommand;
        public ICommand NewCommand
        {
            get { return _newCommand ?? (_newCommand = new ActionCommand(param => NewPurchaseItem())); }
        }

        void NewPurchaseItem()
        {
            EventAggregator.GetEvent<AddPurchaseItemEvent>().Publish(null);
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
                return _editCommand ?? (_editCommand = new ActionCommand(param => EditPurchaseItem(), param => ItemsSelected));
            }
        }

        void EditPurchaseItem()
        {
            AllPurchaseItems.Where(pf => pf.IsSelected).ToList().ForEach(
                item => EventAggregator.GetEvent<EditPurchaseItemEvent>().Publish(item.Id));
        }

        ActionCommand _removeCommand;
        public ICommand RemoveCommand
        {
            get
            {
                return _removeCommand ?? (_removeCommand = new ActionCommand(param => RemovePurchaseItem(), param => ItemsSelected));
            }
        }

        void RemovePurchaseItem()
        {
            var toDelete = AllPurchaseItems.Where(vm => vm.IsSelected).ToList();
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
                MessageBox.Show(Strings.Error_PurchaseItems_CannotDelete);
                return;
            }
            for (var i = toDelete.Count - 1; i >= 0; i--)
            {
                AllPurchaseItems.Remove(toDelete[i]);
            }
        }

        ActionCommand _printCommand;
        public ICommand PrintCommand
        {
            get
            {
                return _printCommand ?? (_printCommand = new ActionCommand(param => PrintPurchaseItems(), param => System.IO.File.Exists(ReportFile)));
            }
        }

        void PrintPurchaseItems()
        {
            EventAggregator.GetEvent<CrystalReportPrintEvent>().Publish(new CrystalReportPrintEventArgs(ReportFile, Strings.ViewModel_AllPurchaseItemsViewModel_PrintHeader));
        }

        #endregion

        void OnPurchaseItemChanged(PurchaseItem purchaseItem)
        {
            var viewmodel = (from vm in AllPurchaseItems where vm.Id == purchaseItem.Id select vm).FirstOrDefault();
            if (viewmodel == null)
            {
                viewmodel = new SinglePurchaseItemViewModel(purchaseItem);
                AllPurchaseItems.Add(viewmodel);
            }
            else
            {
                viewmodel.ExchangeData(purchaseItem);
            }
            OnPropertyChanged("ItemSelected");
            OnPropertyChanged("ItemsSelected");
        }
        /*
                void OnProductionItemRemoved(object sender, EntityRemovedEventArgs<ProductionItem> e)
                {
                    var viewModel = AllPurchaseItems.First(deletedItem => deletedItem.UnderlyingObject == e.RemovedEntity);
                    AllPurchaseItems.Remove(viewModel);
                    OnPropertyChanged("ItemSelected");
                    OnPropertyChanged("ItemsSelected");
                }

                void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
                {
                    if (e.NewItems != null && e.NewItems.Count != 0)
                        foreach (SinglePurchaseItemViewModel unitvm in e.NewItems)
                            unitvm.PropertyChanged += OnPurchaseItemsViewModelPropertyChanged;

                    if (e.OldItems != null && e.OldItems.Count != 0)
                        foreach (SinglePurchaseItemViewModel unitvm in e.OldItems)
                            unitvm.PropertyChanged -= OnPurchaseItemsViewModelPropertyChanged;
                }

                void OnPurchaseItemsViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
                {
                    const string isSelected = "IsSelected";

                    // Make sure that the property name we're referencing is valid.
                    // This is a debugging technique, and does not execute in a Release build.
                    var vm = (sender as SinglePurchaseItemViewModel);
                    if (vm != null)
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