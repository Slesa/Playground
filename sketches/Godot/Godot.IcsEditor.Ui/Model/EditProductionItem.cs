using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Godot.IcsEditor.Ui.Localization;
using Godot.IcsModel.Entities;

namespace Godot.IcsEditor.Ui.Model
{
    public class EditProductionItem : IDataErrorInfo
    {
        #region Creation 

        public static EditProductionItem CreateProductionItem()
        {
            return new EditProductionItem(new ProductionItem());
        }

        public EditProductionItem(ProductionItem productionItem)
        {
            ProductionItem = productionItem;
        }

        #endregion

        public ProductionItem ProductionItem { get; private set; }
        public int Id { get { return ProductionItem.Id; } }
        public string Name { get { return ProductionItem.Name; } set { ProductionItem.Name = value; } }
        public Unit RecipeUnit { get { return ProductionItem.RecipeUnit; } set { ProductionItem.RecipeUnit = value; } }

        public IEnumerable<RecipeItem> RecipeItems
        {
            get { return ProductionItem.RecipeItems; }
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
                case "RecipeUnit":
                    error = ValidateRecipeUnit();
                    break;
            }
            return error;
        }

        string ValidateRecipeUnit()
        {
            return RecipeUnit == null ? Strings.Model_EditProductionItem_RecipeUnit_is_missing : null;
        }

        string ValidateName()
        {
            return EditObject.IsStringMissing(Name) ? Strings.Model_EditProductionItem_Name_is_missing : null;
        }

        #endregion
    }
}