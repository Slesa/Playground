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
    public class StockItemBookingViewModel : WorkspaceViewModel, IDataErrorInfo
    {
        readonly IStockBooker _stockBooker;
        readonly MoveBookStockItems _moveBookStockItems;

        public StockItemBookingViewModel(IDbConversation dbConversation, IEventAggregator eventAggregator, IStockBooker stockBooker)
            : base(dbConversation, eventAggregator)
        {
            _stockBooker = stockBooker;
            _moveBookStockItems = new MoveBookStockItems(dbConversation);
            PreloadLists();
            base.DisplayName = Strings.ViewModel_StockItemBookinViewModel_Title;
        }

        void PreloadLists()
        {
            AllStocks = DbConversation.Query(new AllStocksQuery()).ToList();
            AllUnits = DbConversation.Query(new AllPurchaseUnitsQuery()).ToList();
        }

        public List<Stock> AllStocks { get; private set; }
        public List<Unit> AllUnits { get; private set; }

        public void SetFromStock(int stockId)
        {
            var stock = DbConversation.GetById<Stock>(stockId);
            if (stock != null)
                FromStock = stock;
        }
        
        public void SetToStock(int stockId)
        {
            var stock = DbConversation.GetById<Stock>(stockId);
            if (stock != null)
                ToStock = stock;
        }

        public Stock FromStock
        {
            get { return _moveBookStockItems.FromStock; }
            set
            {
                if (value == _moveBookStockItems.FromStock)
                    return;
                _moveBookStockItems.FromStock = value;
                base.OnPropertyChanged("FromStock");
                base.OnPropertyChanged("ToStock");
                base.OnPropertyChanged("ItemsToMove");
            }
        }

        public Stock ToStock
        {
            get { return _moveBookStockItems.ToStock; }
            set
            {
                if (value == _moveBookStockItems.ToStock)
                    return;
                _moveBookStockItems.ToStock = value;
                base.OnPropertyChanged("FromStock");
                base.OnPropertyChanged("ToStock");
            }
        }

        public ObservableCollection<EditStockMovementItem> ItemsToMove
        {
            get { return _moveBookStockItems.ItemsToMove; }
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
                                               if( Submit() ) CloseCommand.Execute(param);
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
                        var moveTransfer = new StockMoveTransfer{FromStock = FromStock, OfStock = ToStock, ExecutedAt = DateTime.Now};

                        foreach (var item in ItemsToMove.Where(c => c.QuantityToBook != 0.0m))
                        {
                            var toStockItem = _stockBooker.BookItemIntoStock(ToStock, item.QuantityToBook, item.UnitToBook, item.ItemToBook);
                            if (toStockItem == null)
                                continue;
                            DbConversation.InsertObjectOnCommit(toStockItem);
                            var fromStockItem = _stockBooker.BookItemOutOfStock(FromStock, item.QuantityToBook, item.UnitToBook, item.ItemToBook);
                            if( fromStockItem!=null )
                                DbConversation.InsertObjectOnCommit(fromStockItem);
                        
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
                EventAggregator.GetEvent<StockChangedEvent>().Publish(ToStock);
                EventAggregator.GetEvent<StockChangedEvent>().Publish(FromStock);
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
            get { return _moveBookStockItems.IsValid; }
        }

        #endregion

        #region IDataErrorInfo Members

        public string this[string propertyName]
        {
            get
            {
                var error = (_moveBookStockItems as IDataErrorInfo)[propertyName];
                // Dirty the commands registered with CommandManager,
                // such as our Save command, so that they are queried
                // to see if they can execute now.
                CommandManager.InvalidateRequerySuggested();
                return error;
            }
        }

        public string Error
        {
            get { return (_moveBookStockItems as IDataErrorInfo).Error; }
        }

        #endregion
    }
}