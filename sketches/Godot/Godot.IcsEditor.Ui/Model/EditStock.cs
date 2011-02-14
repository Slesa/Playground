using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Godot.IcsEditor.Ui.Localization;
using Godot.IcsModel.Entities;

namespace Godot.IcsEditor.Ui.Model
{
    public class EditStock : IDataErrorInfo
    {
        #region Creation 

        public static EditStock CreateStock()
        {
            return new EditStock(new Stock());
        }

        public EditStock(Stock stock)
        {
            Stock = stock;
        }

        #endregion

        public Stock Stock { get; private set; }
        public int Id { get { return Stock.Id; } }
        public string Name { get { return Stock.Name; } set { Stock.Name = value; } }

        public IEnumerable<StockItem> StockItems
        {
            get { return Stock.StockItems; }
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
            "Name",
        };

        string GetValidationError(string propertyName)
        {
            if (Array.IndexOf(ValidatedProperties, propertyName) < 0)
                return null;

            string error = null;

            switch (propertyName)
            {
                case "Name":
                    error = ValidateName();
                    break;
            }
            return error;
        }

        string ValidateName()
        {
            return EditObject.IsStringMissing(Name) ? Strings.Model_EditStock_Name_is_missing : null;
        }

        #endregion
    }
}