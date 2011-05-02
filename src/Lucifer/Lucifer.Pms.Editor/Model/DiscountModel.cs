using System;
using System.ComponentModel;
using System.Linq;
using Caliburn.Micro;
using Lucifer.Editor.Validators;
using Lucifer.Pms.Editor.Resources;
using Lucifer.Pms.Model.Entities;

namespace Lucifer.Pms.Editor.Model
{
    public class DiscountChangedEvent
    {
        public DiscountChangedEvent(Discount discount)
        {
            Discount = discount;
        }
        public Discount Discount { get; private set; }
    }

    public class DiscountRemovedEvent
    {
        public DiscountRemovedEvent(int id)
        {
            Id = id;
        }
        public int Id { get; private set; }
    }

    public class DiscountModel : PropertyChangedBase, IDataErrorInfo
    {
        readonly Discount _discount;

        public DiscountModel()
        {
            _discount = new Discount();
        }
        public DiscountModel(Discount discount)
        {
            _discount = discount;
        }

        public Discount Discount { get { return _discount; } }
        public int Id { get { return _discount.Id; } }
        public string Name
        {
            get { return _discount.Name; }
            set 
            { 
                _discount.Name = value;
                NotifyOfPropertyChange(() => Error);
            }
        }
        public decimal Rate
        {
            get { return _discount.Rate; }
            set
            {
                _discount.Rate = value;
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
                "Rate",
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
                case "Rate":
                    error = ValidateRate();
                    break;
            }
            return error;
        }

        string ValidateName()
        {
            return EditValidators.IsStringMissing(Name) ? Strings.DiscountModel_Name_missing : null;
        }

        string ValidateRate()
        {
            return Rate==0.0m ? Strings.DiscountModel_Rate_missing : null;
        }

        #endregion
    }
}