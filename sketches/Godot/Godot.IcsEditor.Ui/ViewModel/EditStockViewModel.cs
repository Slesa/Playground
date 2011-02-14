using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Godot.IcsEditor.Ui.Commands;
using Godot.IcsEditor.Ui.Events;
using Godot.IcsEditor.Ui.Localization;
using Godot.IcsEditor.Ui.Model;
using Godot.IcsModel.Entities;
using Godot.IcsModel.Queries;
using Godot.Model;
using NHibernate;

namespace Godot.IcsEditor.Ui.ViewModel
{

    public class StockChangedEvent : CompositePresentationEvent<Stock>
    {
    }

    public class EditStockViewModel : ResponsibleWorkspaceViewModel, IDataErrorInfo
    {
        const string ReportFile = @"Reports\StockItemList.rpt";

        EditStock _editStock;

        public EditStockViewModel(IDbConversation dbConversation, IEventAggregator eventAggregator)
            : base(dbConversation, eventAggregator)
        {
            ContainedObject = 0;
            _editStock = new EditStock(new Stock());
            PreloadLists();
            base.DisplayName = Strings.ViewModel_SingleStockViewModel_NewItem;
        }

        public EditStockViewModel(int entityId, IDbConversation dbConversation, IEventAggregator eventAggregator)
            : base(dbConversation, eventAggregator)
        {
            ContainedObject = entityId;
            DbConversation.UsingTransaction(() =>
            {
                _editStock =
                    new EditStock(
                        DbConversation.GetById<Stock>(entityId));
                PreloadLists();
            });
            base.DisplayName = _editStock.Name;
        }

        void PreloadLists()
        {
            AllRecipeableItems = DbConversation.Query(new AllRecipeableItemsQuery()).ToList();
            AllUnits = DbConversation.Query(new AllUnitsQuery()).ToList();
            StockItems = new ObservableCollection<SingleStockItemViewModel>(
                _editStock.StockItems
                .Select(x => new SingleStockItemViewModel(x)));
            StockItems.CollectionChanged += OnStockItemsChanged;
        }

        public List<RecipeableItem> AllRecipeableItems { get; private set; }
        public List<Unit> AllUnits { get; private set; }
        public ObservableCollection<SingleStockItemViewModel> StockItems { get; private set; }

        public string Name
        {
            get { return _editStock.Name; }
            set
            {
                if (value == _editStock.Name)
                    return;
                _editStock.Name = value;
                base.OnPropertyChanged("Name");
            }
        }

        public string Id { get { return _editStock.Id == 0 ? Strings.ViewModel_SingleStockViewModel_NewId : _editStock.Id.ToString(); } }


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

        ActionCommand _bookFromCommand;
        public ICommand BookFromCommand
        {
            get
            {
                return _bookFromCommand ?? (_bookFromCommand = new ActionCommand(param => BookFromStock()));
            }
        }

        public void BookFromStock()
        {
            EventAggregator.GetEvent<BookFromStockEvent>().Publish(_editStock.Id);
        }

        ActionCommand _sendToCommand;
        public ICommand SendToCommand
        {
            get
            {
                return _sendToCommand ?? (_sendToCommand = new ActionCommand(param => SendToStock()));
            }
        }

        public void SendToStock()
        {
            EventAggregator.GetEvent<SendToStockEvent>().Publish(_editStock.Id);
        }

        ActionCommand _depositCommand;
        public ICommand DepositCommand
        {
            get
            {
                return _depositCommand ?? (_depositCommand = new ActionCommand(param => DepositStock()));
            }
        }

        public void DepositStock()
        {
            EventAggregator.GetEvent<DepositStockEvent>().Publish(_editStock.Id);
        }

        ActionCommand _removalCommand;
        public ICommand RemovalCommand
        {
            get
            {
                return _depositCommand ?? (_removalCommand = new ActionCommand(param => RemovalStock()));
            }
        }

        public void RemovalStock()
        {
            EventAggregator.GetEvent<RemovalStockEvent>().Publish(_editStock.Id);
        }

        ActionCommand _printCommand;
        public ICommand PrintCommand
        {
            get
            {
                return _printCommand ?? (_printCommand = new ActionCommand(param => PrintStockItems(), param => System.IO.File.Exists(ReportFile)));
            }
        }

        void PrintStockItems()
        {
            EventAggregator.GetEvent<CrystalReportPrintEvent>().Publish(new CrystalReportPrintEventArgs(ReportFile, Strings.ViewModel_SingleStockViewModel_PrintHeader));
        }

        #endregion

        #region Public Methods

        public void Remove()
        {/*
            try
            {
                _repStocks.Delete(_editStock.Id);
            }
            catch (GenericADOException)
            {
                MessageBox.Show(Strings.Error_Stocks_CannotDelete);
            }*/
        }

        public bool Save()
        {
            try
            {
                DbConversation.UsingTransaction(() => DbConversation.InsertObjectOnCommit(_editStock.Stock));
                base.OnPropertyChanged("DisplayName");
                EventAggregator.GetEvent<StockChangedEvent>().Publish(_editStock.Stock);
                return true;
            }
            catch (StaleObjectStateException)
            {
                MessageBox.Show("Object was changed outside this dialog."); //Strings.Error_PurchaseItems_CannotSave);
                return true;
            }
            catch
            {
                MessageBox.Show(Strings.Error_Stocks_CannotSave);
                return false;
            }
        }

        #endregion

        #region Private Helpers

        bool CanSave
        {
            //get { return true; /*String.IsNullOrEmpty(this.ValidateCustomerType()) &&  _productionItem.IsValid; */ }
            get { return _editStock.IsValid; }
        }

        void OnStockItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (SingleStockItemViewModel stvm in e.NewItems)
                    _editStock.Stock.AddStockItem(stvm.UnderlayingObject());

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (SingleStockItemViewModel stvm in e.OldItems)
                    _editStock.Stock.RemoveStockItem(stvm.UnderlayingObject());
        }

        #endregion

        #region IDataErrorInfo Members

        public string this[string propertyName]
        {
            get
            {
                var error = (_editStock as IDataErrorInfo)[propertyName];
                // Dirty the commands registered with CommandManager,
                // such as our Save command, so that they are queried
                // to see if they can execute now.
                CommandManager.InvalidateRequerySuggested();
                return error;
            }
        }

        public string Error
        {
            get { return (_editStock as IDataErrorInfo).Error; }
        }

        #endregion
    }
}