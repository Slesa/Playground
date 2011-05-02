using System;
using System.ComponentModel;
using System.Linq;
using Caliburn.Micro;
using Lucifer.Editor.Validators;
using Lucifer.Ics.Editor.Resources;
using Lucifer.Ics.Model.Entities;

namespace Lucifer.Ics.Editor.Model
{
    public class ProductionItemChangedEvent
    {
        public ProductionItemChangedEvent(ProductionItem item)
        {
            ProductionItem = item;
        }
        public ProductionItem ProductionItem { get; private set; }
    }

    public class ProductionItemRemovedEvent
    {
        public ProductionItemRemovedEvent(int id)
        {
            Id = id;
        }
        public int Id { get; private set; }
    }

    public class ProductionItemModel : PropertyChangedBase, IDataErrorInfo
    {
        readonly ProductionItem _productionItem;

        public ProductionItemModel()
        {
            _productionItem = new ProductionItem();
        }
        public ProductionItemModel(ProductionItem productionItem)
        {
            _productionItem = productionItem;
        }

        public ProductionItem ProductionItem { get { return _productionItem; } }
        public int Id { get { return _productionItem.Id; } }
        public string Name
        {
            get { return _productionItem.Name; }
            set
            {
                _productionItem.Name = value;
                NotifyOfPropertyChange(() => Error);
            }
        }
        public Unit RecipeUnit
        {
            get { return ProductionItem.RecipeUnit; } 
            set
            {
                ProductionItem.RecipeUnit = value;
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
                case "RecipeUnit":
                    error = ValidateRecipeUnit();
                    break;
        }
            return error;
        }

        string ValidateName()
        {
            return EditValidators.IsStringMissing(Name) ? Strings.ProductionItemModel_Name_missing : null;
        }

        string ValidateRecipeUnit()
        {
            return RecipeUnit == null ? Strings.ProductionItemModel_RecipeUnit_is_missing : null;
        }

        #endregion
        
    }
}