using System;
using System.ComponentModel;
using System.Linq;
using Caliburn.Micro;
using Lucifer.Ics.Editor.Resources;
using Lucifer.Ics.Model.Entities;

namespace Lucifer.Ics.Editor.Model
{
    public class RecipeChangedEvent
    {
        public RecipeChangedEvent(Recipe recipe)
        {
            Recipe = recipe;
        }
        public Recipe Recipe { get; private set; }
    }

    public class RecipeRemovedEvent
    {
        public RecipeRemovedEvent(int id)
        {
            Id = id;
        }
        public int Id { get; private set; }
    }

    public class RecipeModel : PropertyChangedBase, IDataErrorInfo
    {
        readonly Recipe _recipe;

        public RecipeModel()
        {
            _recipe = new Recipe();
        }
        public RecipeModel(Recipe recipe)
        {
            _recipe = recipe;
        }

        public Recipe Recipe { get { return _recipe; } }
        public int Id { get { return _recipe.Id; } }
        public int Plu
        {
            get { return _recipe.Plu; }
            set
            {
                _recipe.Plu = value;
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
                "Plu",
            };

        string GetValidationError(string columnName)
        {
            if (Array.IndexOf(ValidatedProperties, columnName) < 0)
                return null;
            string error = null;
            switch (columnName)
            {
                case "Plu":
                    error = ValidatePlu();
                    break;
            }
            return error;
        }

        string ValidatePlu()
        {
            return Plu==0 ? Strings.RecipeModel_Plu_missing : null;
        }


        #endregion
        
    }
}