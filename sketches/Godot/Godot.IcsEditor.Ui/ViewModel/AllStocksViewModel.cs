using System;
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
    public class StockViewModelEditEventArgs : EventArgs
    {
        public StockViewModelEditEventArgs(SingleStockViewModel viewModel)
        {
            EditSingleStockViewModel = viewModel;
        }

        public SingleStockViewModel EditSingleStockViewModel { get; private set; }
    }

    public class AllStocksViewModel : WorkspaceViewModel
    {
        const string ReportFile = @"Reports\StockList.rpt";

        private readonly SubscriptionToken _stockInsertedToken;

        public AllStocksViewModel(IDbConversation dbConversation, IEventAggregator eventAggregator)
            : base(dbConversation, eventAggregator)
        {
            base.DisplayName = Strings.ViewModel_AllStocksViewModel_DisplayName;

            CreateAllStocks();
            _stockInsertedToken = EventAggregator.GetEvent<StockChangedEvent>().Subscribe(OnStockChanged);
        }

        public ObservableCollection<SingleStockViewModel> AllStocks { get; private set; }

        void CreateAllStocks()
        {
            AllStocks = new ObservableCollection<SingleStockViewModel>(DbConversation
                .Query(new AllStocksQuery())
                .Select(x => new SingleStockViewModel(x)));
        }

        public bool ItemSelected
        {
            get { return AllStocks.Where(pi => pi.IsSelected).Count() == 1 ? true : false; }
        }

        public bool ItemsSelected
        {
            get { return AllStocks.FirstOrDefault(pi => pi.IsSelected) != null ? true : false; }
        }

        protected override void OnDispose()
        {
            EventAggregator.GetEvent<StockChangedEvent>().Unsubscribe(_stockInsertedToken);
        }

        #region Commands

        public event EventHandler<EventArgs> StockMainBearing;
        ActionCommand _showMainBearingCommand;
        public ICommand ShowMainBearingCommand
        {
            get
            {
                return _showMainBearingCommand ?? (_showMainBearingCommand = new ActionCommand(param => ShowMainBearing()));
            }
        }

        ActionCommand _newCommand;
        public ICommand NewCommand
        {
            get { return _newCommand ?? (_newCommand = new ActionCommand(param => NewStock())); }
        }

        void NewStock()
        {
            EventAggregator.GetEvent<AddStockEvent>().Publish(null);
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
                return _editCommand ?? (_editCommand = new ActionCommand(param => EditStock(), param => ItemsSelected));
            }
        }

        void EditStock()
        {
            AllStocks.Where(st => st.IsSelected).ToList().ForEach(
                item => EventAggregator.GetEvent<EditStockEvent>().Publish(item.Id));
        }

        ActionCommand _removeCommand;
        public ICommand RemoveCommand
        {
            get
            {
                return _removeCommand ?? (_removeCommand = new ActionCommand(param => RemoveStock(), param => ItemsSelected));
            }
        }

        void RemoveStock()
        {
            var toDelete = AllStocks.Where(vm => vm.IsSelected).ToList();
            var msg = toDelete.Aggregate(Strings.ViewModel_AllStocksViewModel_AskToDelete, (current, item) => current + ("\n" + item.DisplayName));
            if (MessageBox.Show(msg, Strings.ViewModel_AllStocksViewModel_DeleteItems, MessageBoxButton.YesNo) != MessageBoxResult.Yes)
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
                MessageBox.Show(Strings.Error_Stocks_CannotDelete);
                return;
            }
            for (var i = toDelete.Count - 1; i >= 0; i--)
            {
                AllStocks.Remove(toDelete[i]);
            }
        }

        ActionCommand _printCommand;
        public ICommand PrintCommand
        {
            get
            {
                return _printCommand ?? (_printCommand = new ActionCommand(param => PrintStocks(), param => System.IO.File.Exists(ReportFile)));
            }
        }

        void PrintStocks()
        {
            EventAggregator.GetEvent<CrystalReportPrintEvent>().Publish(new CrystalReportPrintEventArgs(ReportFile, Strings.ViewModel_AllStocksViewModel_PrintHeader));
        }

        ActionCommand _bookFromCommand;
        public ICommand BookFromCommand
        {
            get
            {
                return _bookFromCommand ?? (_bookFromCommand = new ActionCommand(param => BookFromStock(), param => ItemsSelected));
            }
        }

        void BookFromStock()
        {
            AllStocks.Where(st => st.IsSelected).ToList().ForEach(
                item => EventAggregator.GetEvent<BookFromStockEvent>().Publish(item.Id));
        }

        ActionCommand _sendToCommand;
        public ICommand SendToCommand
        {
            get
            {
                return _sendToCommand ?? (_sendToCommand = new ActionCommand(param => SendToStock(), param => ItemsSelected));
            }
        }

        void SendToStock()
        {
            AllStocks.Where(st => st.IsSelected).ToList().ForEach(
                item => EventAggregator.GetEvent<SendToStockEvent>().Publish(item.Id));
        }

        ActionCommand _depositCommand;
        public ICommand DepositCommand
        {
            get
            {
                return _depositCommand ?? (_depositCommand = new ActionCommand(param => DepositStock(), param => ItemsSelected));
            }
        }

        void DepositStock()
        {
            AllStocks.Where(st => st.IsSelected).ToList().ForEach(
                item => EventAggregator.GetEvent<DepositStockEvent>().Publish(item.Id));
        }

        ActionCommand _removalCommand;
        public ICommand RemovalCommand
        {
            get
            {
                return _removalCommand ?? (_removalCommand = new ActionCommand(param => RemovalStock(), param => ItemsSelected));
            }
        }

        void RemovalStock()
        {
            AllStocks.Where(st => st.IsSelected).ToList().ForEach(
                item => EventAggregator.GetEvent<RemovalStockEvent>().Publish(item.Id));
        }

        #endregion

        #region Public Methods

        public void ShowMainBearing()
        {
            if (StockMainBearing != null)
            {
                StockMainBearing(this, null);
            }
        }
        #endregion


        void OnStockChanged(Stock stock)
        {
            var viewmodel = (from vm in AllStocks where vm.Id == stock.Id select vm).FirstOrDefault();
            if (viewmodel == null)
            {
                viewmodel = new SingleStockViewModel(stock);
                AllStocks.Add(viewmodel);
            }
            else
            {
                viewmodel.ExchangeData(stock);
            }
            OnPropertyChanged("ItemSelected");
            OnPropertyChanged("ItemsSelected");
        }
        /*
        void OnStockRemoved(object sender, EntityRemovedEventArgs<Stock> e)
        {
            var viewModel = AllStocks.First(deletedItem => deletedItem.UnderlyingObject == e.RemovedEntity);
            AllStocks.Remove(viewModel);
            OnPropertyChanged("ItemSelected");
            OnPropertyChanged("ItemsSelected");
        }

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (SingleStockViewModel stvm in e.NewItems)
                    stvm.PropertyChanged += OnStockViewModelPropertyChanged;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (SingleStockViewModel stvm in e.OldItems)
                    stvm.PropertyChanged -= OnStockViewModelPropertyChanged;
        }

        void OnStockViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            const string isSelected = "IsSelected";

            // Make sure that the property name we're referencing is valid.
            // This is a debugging technique, and does not execute in a Release build.
            var vm = (sender as SingleStockViewModel);
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