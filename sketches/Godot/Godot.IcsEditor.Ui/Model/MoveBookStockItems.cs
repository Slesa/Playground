using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Godot.IcsEditor.Ui.Localization;
using Godot.IcsModel.Entities;
using Godot.Model;

namespace Godot.IcsEditor.Ui.Model
{
    public class MoveBookStockItems : EditStockMovements, IDataErrorInfo
    {
        ObservableCollection<EditStockMovementItem> _itemsToMove;

        public MoveBookStockItems(IDbConversation dbConversation)
            : base(dbConversation)
        {
        }

        Stock _fromStock;
        public Stock FromStock
        {
            get { return _fromStock; }
            set
            {
                _fromStock = value;
                if (_itemsToMove != null) _itemsToMove.Clear();
                _itemsToMove = null;
            }
        }

        public Stock ToStock { get; set; }

        public ObservableCollection<EditStockMovementItem> ItemsToMove
        {
            get
            {
                return _itemsToMove ?? (_itemsToMove = GetItemsToMove(FromStock));
            }
        }

        public void AddStockItem(EditStockMovementItem movementItem)
        {
            _itemsToMove.Add(movementItem);
        }

        #region IDataErrorInfo Members

        string IDataErrorInfo.this[string propertyName]
        {
            get { return GetValidationError(propertyName); }
        }

        string IDataErrorInfo.Error { get { return null; } }

        #endregion

        #region Validation

        public bool IsValid
        {
            get { return ValidatedProperties.All(property => GetValidationError(property) == null); }
        }

        static readonly string[] ValidatedProperties = 
        { 
            "FromStock",
            "ToStock",
            //"ItemsToMove",
        };

        string GetValidationError(string propertyName)
        {
            if (Array.IndexOf(ValidatedProperties, propertyName) < 0)
                return null;

            string error = null;

            switch (propertyName)
            {
                case "FromStock":
                    error = ValidateFromStock();
                    break;
                case "ToStock":
                    error = ValidateToStock();
                    break;
                case "ItemsToMove":
                    error = ValidateItemsToMove();
                    break;
            }
            return error;
        }

        string ValidateFromStock()
        {
            if (FromStock == null)
                return Strings.Model_MoveBookStockItems_Source_is_missing;
            if (ToStock != null && FromStock.Id == ToStock.Id)
                return Strings.Model_MoveBookStockItems_Stocks_are_identical;
            return null;
        }

        string ValidateToStock()
        {
            if (ToStock == null)
                return Strings.Model_MoveBookStockItems_Destination_is_missing;
            if (FromStock != null && FromStock.Id == ToStock.Id)
                return Strings.Model_MoveBookStockItems_Stocks_are_identical;
            return null;
        }

        string ValidateItemsToMove()
        {
            var query = from item in ItemsToMove where item.QuantityToBook != 0.0m select item;
            return query.FirstOrDefault() == null ? Strings.Model_MoveBookStockItems_Nothing_to_Book : null;
        }

        #endregion
    }
}