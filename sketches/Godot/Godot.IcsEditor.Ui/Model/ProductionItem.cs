using System;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace Godot.IcsEditor.Model
{
    public class ProductionItem : IcsModel.Entities.ProductionItem, IDataErrorInfo
    {
        #region Creation 

        public static ProductionItem CreateProductionItem()
        {
            return new ProductionItem();
        }

        public static ProductionItem CreateProductionItem(string name)
        {
            return new ProductionItem {Name = name};
        }

        #endregion

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
            }
            return error;
        }

        string ValidateName()
        {
            if (IsStringMissing(Name))
            {
                return "Name is missing";
                // Strings.Customer_Error_MissingFirstName;
            }
            return null;
        }

        static bool IsStringMissing(string value)
        {
            return
                String.IsNullOrEmpty(value) ||
                value.Trim() == String.Empty;
        }

        static bool IsValidEmailAddress(string email)
        {
            if (IsStringMissing(email))
                return false;

            // This regex pattern came from: http://haacked.com/archive/2007/08/21/i-knew-how-to-validate-an-email-address-until-i.aspx
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }

        #endregion
    }
}