using System;
using System.ComponentModel;
using System.Linq;
using Godot.IcsEditor.Ui.Localization;
using Godot.IcsModel.Entities;

namespace Godot.IcsEditor.Ui.Model
{
    public class EditUnitType : IDataErrorInfo
    {
        #region Creation

        public static EditUnitType CreateUnitType()
        {
            return new EditUnitType(new UnitType());
        }

        public EditUnitType(UnitType unitType)
        {
            UnitType = unitType;
        }

        #endregion

        public UnitType UnitType { get; private set; }
        public int Id { get { return UnitType.Id; } }
        public string Name { get { return UnitType.Name; } set { UnitType.Name = value; } }

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
            return EditObject.IsStringMissing(Name) ? Strings.Model_EditUnitType_Name_is_missing : null;
        }

        #endregion
    }
}