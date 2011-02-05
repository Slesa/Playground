using System;
using System.ComponentModel;
using System.Linq;
using Godot.IcsEditor.Ui.Localization;
using Godot.IcsModel.Entities;

namespace Godot.IcsEditor.Ui.Model
{
    public class EditPurchaseItem : IDataErrorInfo
    {
        #region Creation

        public static EditPurchaseItem CreatePurchaseItem()
        {
            return new EditPurchaseItem(new PurchaseItem());
        }

        public EditPurchaseItem(PurchaseItem item)
        {
            PurchaseItem = item;
        }

        #endregion

        public PurchaseItem PurchaseItem { get; private set; }
        public int Id { get { return PurchaseItem.Id; } }
        public string Name { get { return PurchaseItem.Name; } set { PurchaseItem.Name = value; } }
        public PurchaseFamily PurchaseFamily { get { return PurchaseItem.PurchaseFamily; } set { PurchaseItem.PurchaseFamily = value; } }
        public Unit PurchaseUnit { get { return PurchaseItem.PurchaseUnit; } set { PurchaseItem.PurchaseUnit = value; } }
        public Unit RecipeUnit { get { return PurchaseItem.RecipeUnit; } set { PurchaseItem.RecipeUnit = value; } }

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
            "PurchaseFamily",
            "PurchaseUnit",
            "RecipeUnit",
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

        string ValidateFamily()
        {
            return PurchaseFamily == null ? Strings.Model_EditPurchaseItem_PurchaseFamily_is_missing : null;
        }

        string ValidatePurchaseUnit()
        {
            return PurchaseUnit == null ? Strings.Model_EditPurchaseItem_PurchaseUnit_is_missing : null;
        }

        string ValidateRecipeUnit()
        {
            return RecipeUnit == null ? Strings.Model_EditPurchaseItem_RecipeUnit_is_missing : null;
        }

        string ValidateName()
        {
            return EditObject.IsStringMissing(Name) ? Strings.Model_EditPurchaseItem_Name_is_missing : null;
        }

        #endregion
    }
}