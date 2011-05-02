using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Caliburn.Micro;
using Lucifer.Editor.Validators;
using Lucifer.Ics.Editor.Resources;
using Lucifer.Ics.Model.Entities;

namespace Lucifer.Ics.Editor.Model
{
    public class StockChangedEvent
    {
        public StockChangedEvent(Stock stock)
        {
            Stock = stock;
        }
        public Stock Stock { get; private set; }
    }

    public class StockRemovedEvent
    {
        public StockRemovedEvent(int id)
        {
            Id = id;
        }
        public int Id { get; private set; }
    }

    public class StockModel : PropertyChangedBase, IDataErrorInfo
    {
        readonly Stock _stock;

        public StockModel()
        {
            _stock = new Stock();
        }
        public StockModel(Stock stock)
        {
            _stock = stock;
        }

        public Stock Stock { get { return _stock; } }
        public int Id { get { return _stock.Id; } }
        public string Name
        {
            get { return _stock.Name; }
            set
            {
                _stock.Name = value;
                NotifyOfPropertyChange(() => Error);
            }
        }
        public bool IsMainStock
        {
            get { return Stock.IsMainStock; }
            set { _stock.IsMainStock = value; }
        }
        public IEnumerable<StockItem> StockItems
        {
            get { return Stock.StockItems; }
        }

        #region IDataErrorInfo Members

        public string this[string columnName]
        {
            get { return GetValidationError(columnName); }
        }

        public string Error
        {
            get
            {
                return ValidatedProperties.Select(GetValidationError).FirstOrDefault(error => error != null);
            }
        }

        #endregion

        #region Validation

        static readonly string[] ValidatedProperties =
            {
                "Name",
            };

        string GetValidationError(string columnName)
        {
            if (Array.IndexOf(ValidatedProperties, columnName) < 0)
                return null;
            string error = null;
            switch (columnName)
            {
                case "Name":
                    error = ValidateName();
                    break;
        }
            return error;
        }

        string ValidateName()
        {
            return EditValidators.IsStringMissing(Name) ? Strings.StockModel_Name_missing : null;
        }


        #endregion
    }
}