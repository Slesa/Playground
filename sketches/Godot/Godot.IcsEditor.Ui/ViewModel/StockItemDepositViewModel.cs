using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Godot.IcsEditor.Ui.Events;
using Godot.IcsEditor.Ui.Localization;
using Godot.IcsEditor.Ui.Model;
using Godot.IcsModel;
using Godot.IcsModel.Entities;
using Godot.IcsModel.Queries;
using Godot.Model;

namespace Godot.IcsEditor.Ui.ViewModel
{
    public class StockItemDepositViewModel : WorkspaceViewModel, IDataErrorInfo
    {
        readonly IStockBooker _stockBooker;
        readonly MoveDepositStockItems _moveDepositStockItems;

        public StockItemDepositViewModel(IDbConversation dbConversation, IEventAggregator eventAggregator, IStockBooker stockBooker) 
            : base(dbConversation, eventAggregator)
        {
            _stockBooker = stockBooker;
            _moveDepositStockItems = new MoveDepositStockItems(dbConversation);
            PreloadLists();
            base.DisplayName = Strings.ViewModel_StockItemDepositViewModel_Title;
        }

        void PreloadLists()
        {
            AllStocks = DbConversation.Query(new AllStocksQuery()).ToList();
            AllUnits = DbConversation.Query(new AllPurchaseUnitsQuery()).ToList();
        }

        public List<Stock> AllStocks { get; private set; }
        public List<Unit> AllUnits { get; private set; }

        public string Reason
        {
            get { return _moveDepositStockItems.Reason; }
            set
            {
                if (value == _moveDepositStockItems.Reason)
                    return;
                _moveDepositStockItems.Reason = value;
                base.OnPropertyChanged("Reason");
            }
        }

        public void SetStock(int stockId)
        {
            var stock = DbConversation.GetById<Stock>(stockId);
            if (stock != null)
                Stock = stock;
        }

        public Stock Stock
        {
            get { return _moveDepositStockItems.Stock; }
            set
            {
                if (value == _moveDepositStockItems.Stock)
                    return;
                _moveDepositStockItems.Stock = value;
                base.OnPropertyChanged("Stock");
                base.OnPropertyChanged("ItemsToMove");
            }
        }

        public ObservableCollection<EditStockMovementItem> ItemsToMove
        {
            get { return _moveDepositStockItems.ItemsToMove; }
        }

        #region Commands

        ActionCommand _submitCommand;

        public ICommand SubmitCommand
        {
            get
            {
                return _submitCommand ??
                       (_submitCommand = new ActionCommand(
                                           param =>
                                           {
                                               if (Submit()) CloseCommand.Execute(param);
                                           },
                                           param => CanSubmit
                                           ));
            }
        }

        #endregion

        #region Public Methods

        public bool Submit()
        {
            try
            {
                DbConversation.UsingTransaction(() =>
                {
                    var moveTransfer = new StockMoveDeposit { OfStock = Stock, Reason = Reason, ExecutedAt = DateTime.Now };

                    foreach (var item in ItemsToMove.Where(c => c.QuantityToBook != 0.0m))
                    {
                        var stockItem = _stockBooker.BookItemIntoStock(Stock, item.QuantityToBook, item.UnitToBook, item.ItemToBook);
                        if (stockItem == null)
                            continue;
                        DbConversation.InsertObjectOnCommit(stockItem);

                        var moveItem = new StockMoveItem
                        {
                            Unit = item.UnitToBook,
                            Quantity = item.QuantityToBook,
                            RecipeableItem = item.ItemToBook,
                        };
                        moveTransfer.AddMoveItem(moveItem);
                    }

                    DbConversation.InsertObjectOnCommit(moveTransfer);
                });
                EventAggregator.GetEvent<StockChangedEvent>().Publish(Stock);
                return true;
            }
            catch
            {
                MessageBox.Show(Strings.Error_BookItems_CannotBook);
                return false;
            }
        }

        #endregion

        #region Private Helpers

        bool CanSubmit
        {
            get { return _moveDepositStockItems.IsValid; }
        }

        #endregion

        #region IDataErrorInfo Members

        public string this[string propertyName]
        {
            get
            {
                var error = (_moveDepositStockItems as IDataErrorInfo)[propertyName];
                // Dirty the commands registered with CommandManager,
                // such as our Save command, so that they are queried
                // to see if they can execute now.
                CommandManager.InvalidateRequerySuggested();
                return error;
            }
        }

        public string Error
        {
            get { return (_moveDepositStockItems as IDataErrorInfo).Error; }
        }

        #endregion

    }
}