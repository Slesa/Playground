using System;
using System.ComponentModel;
using System.Linq;
using Caliburn.Micro;
using Lucifer.Editor.Validators;
using Lucifer.Ics.Editor.Resources;
using Lucifer.Ics.Model.Entities;

namespace Lucifer.Ics.Editor.Model
{
    public class PurchaseFamilyChangedEvent
    {
        public PurchaseFamilyChangedEvent(PurchaseFamily family)
        {
            PurchaseFamily = family;
        }
        public PurchaseFamily PurchaseFamily { get; private set; }
    }

    public class PurchaseFamilyRemovedEvent
    {
        public PurchaseFamilyRemovedEvent(int id)
        {
            Id = id;
        }
        public int Id { get; private set; }
    }

    public class PurchaseFamilyModel : PropertyChangedBase, IDataErrorInfo
    {
        readonly PurchaseFamily _purchaseFamily;

        public PurchaseFamilyModel()
        {
            _purchaseFamily = new PurchaseFamily();
        }
        public PurchaseFamilyModel(PurchaseFamily purchaseFamily)
        {
            _purchaseFamily = purchaseFamily;
        }

        public PurchaseFamily PurchaseFamily { get { return _purchaseFamily; } }
        public int Id { get { return _purchaseFamily.Id; } }
        public string Name
        {
            get { return _purchaseFamily.Name; }
            set
            {
                _purchaseFamily.Name = value;
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
            return EditValidators.IsStringMissing(Name) ? Strings.PurchaseFamilyModel_Name_missing : null;
        }

        #endregion
    }
}