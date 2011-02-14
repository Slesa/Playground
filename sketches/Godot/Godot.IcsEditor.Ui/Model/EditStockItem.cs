using System;
using System.ComponentModel;
using System.Linq;
using Godot.IcsEditor.Ui.Localization;
using Godot.IcsModel.Entities;

namespace Godot.IcsEditor.Ui.Model
{
    public class EditStockItem : IDataErrorInfo
    {
        #region Creation 

        public static EditStockItem CreateStockItem()
        {
            return new EditStockItem(new StockItem());
        }

        public EditStockItem(StockItem stockItem)
        {
            StockItem = stockItem;
        }

        #endregion

        public StockItem StockItem { get; private set; }
        public decimal Quantity { get { return StockItem.Quantity; } set { StockItem.Quantity = value; } }
        public Unit Unit { get { return StockItem.Unit; } set { StockItem.Unit = value; } }
        public RecipeableItem RecipeableItem { get { return StockItem.RecipeableItem; } set { StockItem.RecipeableItem = value; } }

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
            "Unit",
            "RecipeableItem",
        };

        string GetValidationError(string propertyName)
        {
            if (Array.IndexOf(ValidatedProperties, propertyName) < 0)
                return null;

            string error = null;

            switch (propertyName)
            {
                case "Unit":
                    error = ValidateUnit();
                    break;
                case "RecipeableItem":
                    error = ValidateRecipeableItem();
                    break;
            }
            return error;
        }

        string ValidateRecipeableItem()
        {
            return RecipeableItem== null ? Strings.Model_EditStock_StockItem_is_missing : null;
        }

        string ValidateUnit()
        {
            return Unit==null ? Strings.Model_EditStock_Unit_is_missing : null;
        }

        #endregion
        
    }
}