using System;
using System.ComponentModel;
using Lucifer.Editor.Validators;
using Lucifer.Ics.Editor.Resources;
using Lucifer.Ics.Model.Entities;

namespace Lucifer.Ics.Editor.Model
{
    public class UnitModel : IDataErrorInfo
    {
        readonly Unit _unit;

        public UnitModel()
        {
            _unit = new Unit();
        }

        public UnitModel(Unit unit)
        {
            _unit = unit;
        }

        public int Id { get { return _unit.Id; } }
        public string Name
        {
            get { return _unit.Name; }
            set { _unit.Name = value; }
        }
        public string Contraction
        {
            get { return _unit.Contraction; }
            set { _unit.Contraction = value; }
        }
        public UnitType UnitType
        {
            get { return _unit.UnitType; }
            set { _unit.UnitType = value; }
        }
        public Unit Parent
        {
            get { return _unit.Parent; }
            set { _unit.Parent = value; }
        }
        private string FactorToParentString { get; set; }
        public string FactorToParent
        {
            get { return _unit.FactorToParent.ToString(); }
            set
            {
                FactorToParentString = value;
                decimal x;
                if (decimal.TryParse(FactorToParentString, out x))
                    _unit.FactorToParent = x;
            }
        }
        public bool Purchasing
        {
            get { return _unit.Purchasing; }
            set { _unit.Purchasing = value; }
        }
        public bool Reciping
        {
            get { return _unit.Reciping; }
            set { _unit.Reciping = value; }
        }

        #region IDataErrorInfo Members

        public string this[string columnName]
        {
            get { return GetValidationError(columnName); }
        }

        public string Error { get { return null; }  }

        #endregion

        static readonly string[] ValidatedProperties =
            {
                "Name",
                "Contraction",
                "UnitType",
                "Parent",
                "FactorToParent",
            };

        string GetValidationError(string columnName)
        {
            if (Array.IndexOf(ValidatedProperties, columnName) < 0)
                return null;
            string error = null;
            switch(columnName)
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
            return EditValidators.IsStringMissing(Name) ? Strings.UnitModel_Name_missing : null;
        }
        string ValidateContraction()
        {
            return EditValidators.IsStringMissing(Contraction) ? Strings.UnitModel_Contraction_missing : null;
        }
        string ValidateUnitType()
        {
            if (UnitType == null)
                return Strings.UnitModel_UnitType_missing;
            if (Parent != null && Parent.UnitType != UnitType)
                return Strings.UnitModel_InvalidParentType;
            return null;
        }
        string ValidateParent()
        {
            if (Parent != null)
            {
                var parent = Parent;
                if (parent.UnitType != UnitType)
                    return Strings.UnitModel_InvalidParentType;
                do
                {
                    if (parent == _unit)
                        return Strings.UnitModel_Parent_recursion_detected;
                    parent = parent.Parent;
                } while (parent != null);
            }
            return null;
        }
        string ValidateFactorToParent()
        {
            decimal factor = 0.0m;
            if (!EditValidators.IsStringMissing(FactorToParentString))
            {
                if (!decimal.TryParse(FactorToParentString, out factor))
                    return Strings.UnitModel_FactorToParent_invalid;
            }
            if (Parent != null)
            {
                if (EditValidators.IsStringMissing(FactorToParentString))
                    return Strings.UnitModel_FactorToParent_missing;
                if (factor == 0.0m)
                    return Strings.UnitModel_FactorToParent_invalid;
            }
            return null;
        }
 

    }
}