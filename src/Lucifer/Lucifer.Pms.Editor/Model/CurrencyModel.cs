using System;
using System.ComponentModel;
using System.Linq;
using Caliburn.Micro;
using Lucifer.Editor.Validators;
using Lucifer.Pms.Editor.Resources;
using Lucifer.Pms.Model.Entities;

namespace Lucifer.Pms.Editor.Model
{
    public class CurrencyChangedEvent
    {
        public CurrencyChangedEvent(Currency currency)
        {
            Currency = currency;
        }
        public Currency Currency { get; private set; }
    }

    public class CurrencyRemovedEvent
    {
        public CurrencyRemovedEvent(int id)
        {
            Id = id;
        }
        public int Id { get; private set; }
    }

    public class CurrencyModel : PropertyChangedBase, IDataErrorInfo
    {
        readonly Currency _currency;

        public CurrencyModel()
        {
            _currency = new Currency();
        }
        public CurrencyModel(Currency currency)
        {
            _currency = currency;
        }

        public Currency Currency { get { return _currency; } }
        public int Id { get { return _currency.Id; } }
        public string Name
        {
            get { return _currency.Name; }
            set 
            { 
                _currency.Name = value;
                NotifyOfPropertyChange(() => Error);
            }
        }
        public string Contraction
        {
            get { return _currency.Contraction; }
            set 
            { 
                _currency.Contraction = value;
                NotifyOfPropertyChange(() => Error);
            }
        }
        public string Symbol
        {
            get { return _currency.Symbol; }
            set 
            { 
                _currency.Symbol = value;
                NotifyOfPropertyChange(() => Error);
            }
        }
        public decimal Rate
        {
            get { return _currency.Rate; }
            set
            {
                _currency.Rate = value;
                NotifyOfPropertyChange(() => Error);
            }
        }
        public int DecimalPosition
        {
            get { return _currency.DecimalPosition; }
            set
            {
                _currency.DecimalPosition = value;
                NotifyOfPropertyChange(() => Error);
            }
        }
        public char DecimalChar
        {
            get { return _currency.DecimalChar; }
            set
            {
                _currency.DecimalChar = value;
                NotifyOfPropertyChange(() => Error);
            }
        }
        public char ThousandChar
        {
            get { return _currency.ThousandChar; }
            set
            {
                _currency.ThousandChar = value;
                NotifyOfPropertyChange(() => Error);
            }
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
                "Symbol",
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
                case "Symbol":
                    error = ValidateSymbol();
                    break;
            }
            return error;
        }

        string ValidateName()
        {
            return EditValidators.IsStringMissing(Name) ? Strings.CurrencyModel_Name_missing : null;
        }

        string ValidateSymbol()
        {
            return EditValidators.IsStringMissing(Symbol) ? Strings.CurrencyModel_Symbol_missing : null;
        }

        #endregion
    }
}