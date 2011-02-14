using System;
using System.ComponentModel;
using System.Linq;
using Godot.IcsEditor.Ui.Localization;
using Godot.IcsEditor.Ui.ViewModel;
using Godot.IcsModel.Entities;

namespace Godot.IcsEditor.Ui.Model
{
    public class EditRecipeItem : ViewModelBase, IDataErrorInfo
    {
        #region Creation 

        public static EditRecipeItem CreateRecipeItem()
        {
            return new EditRecipeItem(new RecipeItem());
        }

        public EditRecipeItem(RecipeItem recipeItem)
        {
            RecipeItem = recipeItem;
        }

        #endregion

        public RecipeItem RecipeItem { get; private set; }
        public decimal Quantity { get { return RecipeItem.Quantity; } set { RecipeItem.Quantity = value; } }
        public Unit Unit { get { return RecipeItem.Unit; } set { RecipeItem.Unit = value; } }
        public RecipeableItem RecipeableItem { get { return RecipeItem.RecipeableItem; } set { RecipeItem.RecipeableItem = value; } }

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
            return RecipeableItem== null ? Strings.Model_EditStock_Name_is_missing : null;
        }

        string ValidateUnit()
        {
            return Unit==null ? Strings.Model_EditStock_Name_is_missing : null;
        }

        #endregion
        
    }
        
}