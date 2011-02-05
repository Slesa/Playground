using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Godot.IcsEditor.Ui.Localization;
using Godot.IcsModel.Entities;

namespace Godot.IcsEditor.Ui.Model
{
    public class EditRecipe : IDataErrorInfo
    {
        #region Creation 

        public static EditRecipe CreateRecipe()
        {
            return new EditRecipe(new Recipe());
        }

        public EditRecipe(Recipe recipe)
        {
            Recipe = recipe;
        }

        #endregion

        public Recipe Recipe { get; private set; }
        public int Id { get { return Recipe.Id; } }
        public int Plu { get { return Recipe.Plu; } set { Recipe.Plu=value; } }

        public IEnumerable<RecipeItem> RecipeItems
        {
            get { return Recipe.RecipeItems; }
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
            "Plu",
        };

        string GetValidationError(string propertyName)
        {
            if (Array.IndexOf(ValidatedProperties, propertyName) < 0)
                return null;

            string error = null;

            switch (propertyName)
            {
                case "Plu":
                    error = ValidatePlu();
                    break;
            }
            return error;
        }

        string ValidatePlu()
        {
            return Plu==0 ? Strings.Model_Recipe_SalesItem_is_missing : null;
        }

        #endregion
    }
}