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
    public class AllPurchaseFamiliesViewModel : WorkspaceViewModel
    {
        const string ReportFile = @"Reports\PurchaseFamilyList.rpt";

        private readonly SubscriptionToken _purchaseFamilyInsertedToken;

        public AllPurchaseFamiliesViewModel(IDbConversation dbConversation, IEventAggregator eventAggregator)
            : base(dbConversation, eventAggregator)
        {
            base.DisplayName = Strings.ViewModel_AllPurchaseFamiliesViewModel_DisplayName;

            CreateAllPurchaseFamilies();
            _purchaseFamilyInsertedToken = EventAggregator.GetEvent<PurchaseFamilyChangedEvent>().Subscribe(OnPurchaseFamilyChanged);
        }

        public ObservableCollection<SinglePurchaseFamilyViewModel> AllPurchaseFamilies { get; private set; }

        void CreateAllPurchaseFamilies()
        {
            AllPurchaseFamilies = new ObservableCollection<SinglePurchaseFamilyViewModel>(DbConversation
                .Query(new AllPurchaseFamiliesQuery())
                .Select(x => new SinglePurchaseFamilyViewModel(x)));
        }

        public bool ItemSelected
        {
            get { return AllPurchaseFamilies.Where(pf => pf.IsSelected).Count() == 1 ? true : false; }
        }

        public bool ItemsSelected
        {
            get { return AllPurchaseFamilies.FirstOrDefault(pf => pf.IsSelected) != null ? true : false; }
        }

        protected override void OnDispose()
        {
            EventAggregator.GetEvent<PurchaseFamilyChangedEvent>().Unsubscribe(_purchaseFamilyInsertedToken);
        }

        #region Commands

        ActionCommand _newCommand;
        public ICommand NewCommand
        {
            get { return _newCommand ?? (_newCommand = new ActionCommand(param => NewPurchaseFamily())); }
        }

        void NewPurchaseFamily()
        {
            EventAggregator.GetEvent<AddPurchaseFamilyEvent>().Publish(null);
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
                return _editCommand ?? (_editCommand = new ActionCommand(param => EditPurchaseFamily(), param => ItemsSelected));
            }
        }

        void EditPurchaseFamily()
        {
            AllPurchaseFamilies.Where(pf => pf.IsSelected).ToList().ForEach(
                item => EventAggregator.GetEvent<EditPurchaseFamilyEvent>().Publish(item.Id));
        }

        ActionCommand _removeCommand;
        public ICommand RemoveCommand
        {
            get
            {
                return _removeCommand ?? (_removeCommand = new ActionCommand(param => RemovePurchaseFamily(), param => ItemsSelected));
            }
        }

        void RemovePurchaseFamily()
        {
            var toDelete = AllPurchaseFamilies.Where(vm => vm.IsSelected).ToList();
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
                MessageBox.Show(Strings.Error_PurchaseFamilies_CannotDelete);
                return;
            }
            for (var i = toDelete.Count - 1; i >= 0; i--)
            {
                AllPurchaseFamilies.Remove(toDelete[i]);
            }
        }

        ActionCommand _printCommand;
        public ICommand PrintCommand
        {
            get
            {
                return _printCommand ?? (_printCommand = new ActionCommand(param => PrintPurchaseFamilies(), param => System.IO.File.Exists(ReportFile)));
            }
        }

        void PrintPurchaseFamilies()
        {
            EventAggregator.GetEvent<CrystalReportPrintEvent>().Publish(new CrystalReportPrintEventArgs(ReportFile, Strings.ViewModel_AllPurchaseFamiliesViewModel_PrintHeader));
        }

        #endregion

        void OnPurchaseFamilyChanged(PurchaseFamily purchaseFamily)
        {
            var viewmodel = (from vm in AllPurchaseFamilies where vm.Id == purchaseFamily.Id select vm).FirstOrDefault();
            if (viewmodel == null)
            {
                viewmodel = new SinglePurchaseFamilyViewModel(purchaseFamily);
                AllPurchaseFamilies.Add(viewmodel);
            }
            else
            {
                viewmodel.ExchangeData(purchaseFamily);
            }
            OnPropertyChanged("ItemSelected");
            OnPropertyChanged("ItemsSelected");
        }
        /*
                void OnPurchaseFamilyRemoved(object sender, EntityRemovedEventArgs<PurchaseFamily> e)
                {
                    var viewModel = AllPurchaseFamilies.First(deletedItem => deletedItem.UnderlyingObject == e.RemovedEntity);
                    AllPurchaseFamilies.Remove(viewModel);
                    OnPropertyChanged("ItemSelected");
                    OnPropertyChanged("ItemsSelected");
                }

                void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
                {
                    if (e.NewItems != null && e.NewItems.Count != 0)
                        foreach (SinglePurchaseFamilyViewModel unitvm in e.NewItems)
                            unitvm.PropertyChanged += OnPurchaseFamiliesViewModelPropertyChanged;

                    if (e.OldItems != null && e.OldItems.Count != 0)
                        foreach (SinglePurchaseFamilyViewModel unitvm in e.OldItems)
                            unitvm.PropertyChanged -= OnPurchaseFamiliesViewModelPropertyChanged;
                }

                void OnPurchaseFamiliesViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
                {
                    const string isSelected = "IsSelected";

                    // Make sure that the property name we're referencing is valid.
                    // This is a debugging technique, and does not execute in a Release build.
                    var vm = (sender as SinglePurchaseFamilyViewModel);
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