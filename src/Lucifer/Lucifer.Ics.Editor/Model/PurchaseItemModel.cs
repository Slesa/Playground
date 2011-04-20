using System;
using System.ComponentModel;
using System.Linq;
using Caliburn.Micro;
using Lucifer.Editor.Validators;
using Lucifer.Ics.Editor.Resources;
using Lucifer.Ics.Model.Entities;

namespace Lucifer.Ics.Editor.Model
{
    public class PurchaseItemChangedEvent
    {
        public PurchaseItem PurchaseItem;
    }
    public class PurchaseItemRemovedEvent
    {
        public int Id;
    }

    public class PurchaseItemModel : PropertyChangedBase, IDataErrorInfo
    {
        readonly PurchaseItem _purchaseItem;

        public PurchaseItemModel()
        {
            _purchaseItem = new PurchaseItem();
        }
        public PurchaseItemModel(PurchaseItem purchaseItem)
        {
            _purchaseItem = purchaseItem;
        }

        public PurchaseItem PurchaseItem { get { return _purchaseItem; } }
        public int Id { get { return _purchaseItem.Id; } }
        public string Name
        {
            get { return _purchaseItem.Name; }
            set
            {
                _purchaseItem.Name = value;
                NotifyOfPropertyChange(() => Error);
            }
        }
        public PurchaseFamily PurchaseFamily
        {
            get { return PurchaseItem.PurchaseFamily; }
            set
            {
                PurchaseItem.PurchaseFamily = value;
                NotifyOfPropertyChange(() => Error);
            }
        }
        public Unit PurchaseUnit
        {
            get { return PurchaseItem.PurchaseUnit; }
            set
            {
                PurchaseItem.PurchaseUnit = value;
                NotifyOfPropertyChange(() => Error);
            }
        }
        public Unit RecipeUnit
        {
            get { return PurchaseItem.RecipeUnit; }
            set
            {
                PurchaseItem.RecipeUnit = value;
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
                "PurchaseFamily",
                "PurchaseUnit",
                "RecipeUnit",
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
                case "PurchaseFamily":
                    error = ValidateFamily();
                    break;
                case "PurchaseUnit":
                    error = ValidatePurchaseUnit();
                    break;
                case "RecipeUnit":
                    error = ValidateRecipeUnit();
                    break;
            }
            return error;
        }

        string ValidateName()
        {
            return EditValidators.IsStringMissing(Name) ? Strings.PurchaseItemModel_Name_missing : null;
        }
        string ValidateFamily()
        {
            return PurchaseFamily == null ? Strings.PurchaseItemModel_PurchaseFamily_is_missing : null;
        }
        string ValidatePurchaseUnit()
        {
            return PurchaseUnit == null ? Strings.PurchaseItemModel_PurchaseUnit_is_missing : null;
        }
        string ValidateRecipeUnit()
        {
            return RecipeUnit == null ? Strings.PurchaseItemModel_RecipeUnit_is_missing : null;
        }

        #endregion

    }
}