using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Godot.IcsEditor.Ui.Localization;
using Godot.IcsModel.Entities;
using Godot.Model;

namespace Godot.IcsEditor.Ui.Model
{
    public class MoveDepositStockItems : EditStockMovements, IDataErrorInfo
    {
        ObservableCollection<EditStockMovementItem> _itemsToMove;

        public MoveDepositStockItems(IDbConversation dbConversation) 
            : base(dbConversation)
        {
        }

        public string Reason { get; set; }

        Stock _stock;
        public Stock Stock
        {
            get { return _stock; }
            set
            {
                _stock = value;
                if (_itemsToMove != null) _itemsToMove.Clear();
                _itemsToMove = null;
            }
        }

        public ObservableCollection<EditStockMovementItem> ItemsToMove
        {
            get
            {
                return _itemsToMove ?? (_itemsToMove = GetItemsToMove(Stock));
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
            "Stock",
            "Reason",
            //"ItemsToMove",
        };

        string GetValidationError(string propertyName)
        {
            if (Array.IndexOf(ValidatedProperties, propertyName) < 0)
                return null;

            string error = null;

            switch (propertyName)
            {
                case "Stock":
                    error = ValidateStock();
                    break;
                case "Reason":
                    error = ValidateReason();
                    break;
                case "ItemsToMove":
                    error = ValidateItemsToMove();
                    break;
            }
            return error;
        }

        string ValidateStock()
        {
            return Stock == null ? Strings.Model_MoveDepositStockItems_Stock_is_missing : null;
        }

        string ValidateReason()
        {
            return EditObject.IsStringMissing(Reason) ? Strings.Model_MoveDepositStockItems_Reason_is_missing : null;
        }

        string ValidateItemsToMove()
        {
            var query = from item in ItemsToMove where item.QuantityToBook != 0.0m select item;
            return query.FirstOrDefault() == null ? Strings.Model_MoveDepositStockItems_Nothing_to_book : null;
        }

        #endregion
    }
}