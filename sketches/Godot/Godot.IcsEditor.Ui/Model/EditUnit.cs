using System;
using System.ComponentModel;
using System.Linq;
using Godot.IcsEditor.Ui.Localization;
using Godot.IcsModel.Entities;

namespace Godot.IcsEditor.Ui.Model
{
    public class EditUnit : IDataErrorInfo
    {
        #region Creation

        public static EditUnit CreateUnit()
        {
            return new EditUnit(new Unit());
        }

        public EditUnit(Unit unit)
        {
            Unit = unit;
            FactorToParentString = FactorToParent;
        }

        #endregion

        public Unit Unit { get; private set; }
        public int Id { get { return Unit.Id; } }
        public string Name { get { return Unit.Name; } set { Unit.Name = value; } }
        public string Contraction { get { return Unit.Contraction; } set { Unit.Contraction = value; } }
        public UnitType UnitType { get { return Unit.UnitType; } set { Unit.UnitType = value; } }
        public Unit Parent { get { return Unit.Parent; } set { Unit.Parent = value; } }
        private string FactorToParentString { get; set; }
        public string FactorToParent
        {
            get { return Unit.FactorToParent.ToString(); } 
            set
            {
                FactorToParentString = value;
                decimal x;
                if (decimal.TryParse(FactorToParentString, out x))
                    Unit.FactorToParent = x;
            }
        }
        public bool Purchasing { get { return Unit.Purchasing; } set { Unit.Purchasing = value; } }
        public bool Reciping { get { return Unit.Reciping; } set { Unit.Reciping = value; } }

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
            "Contraction",
            "UnitType",
            "Parent",
            "FactorToParent",
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
                case "Contraction":
                    error = ValidateContraction();
                    break;
                case "UnitType":
                    error = ValidateUnitType();
                    break;
                case "Parent":
                    error = ValidateParent();
                    break;
                case "FactorToParent":
                    error = ValidateFactorToParent();
                    break;
            }
            return error;
        }

        string ValidateName()
        {
            return EditObject.IsStringMissing(Name) ? Strings.Model_EditUnit_Name_is_missing : null;
        }

        string ValidateContraction()
        {
            return EditObject.IsStringMissing(Contraction) ? Strings.Model_EditUnit_Contraction_is_missing : null;
        }

        string ValidateUnitType()
        {
            if( UnitType == null )
                return Strings.Model_EditUnit_UnitType_is_missing;
            if (Parent != null && Parent.UnitType != UnitType)
                return Strings.Model_EditUnit_InvalidParentType;
            return null;
        }

        string ValidateParent()
        {
            if (Parent != null)
            {
                var parent = Parent;
                if( parent.UnitType!=UnitType)
                    return Strings.Model_EditUnit_InvalidParentType;
                do
                {
                    if( parent==Unit )
                        return Strings.Model_EditUnit_Parent_recursion_detected;
                    parent = parent.Parent;
                } while (parent != null);
            }
            return null;
        }

        string ValidateFactorToParent()
        {
            decimal factor = 0.0m;
            if (!EditObject.IsStringMissing(FactorToParentString))
            {
                if (!decimal.TryParse(FactorToParentString, out factor))
                    return Strings.Model_EditUnit_FactorToParent_is_invalid;
            }
            if (Parent != null)
            {
                if (EditObject.IsStringMissing(FactorToParentString)) 
                    return Strings.Model_EditUnit_FactorToParent_is_missing;
                if (factor == 0.0m)
                    return Strings.Model_EditUnit_FactorToParent_is_invalid;
            }
            return null;
        }
        #endregion
    }
}