using System;
using System.ComponentModel;
using System.Linq;
using Caliburn.Micro;
using Lucifer.Editor.Validators;
using Lucifer.Ics.Editor.Resources;
using Lucifer.Ics.Model.Entities;

namespace Lucifer.Ics.Editor.Model
{
    public class UnitTypeChangedEvent
    {
        public UnitTypeChangedEvent(UnitType type)
        {
            UnitType = type;
        }
        public UnitType UnitType { get; private set; }
    }

    public class UnitTypeRemovedEvent
    {
        public UnitTypeRemovedEvent(int id)
        {
            Id = id;
        }
        public int Id { get; private set; }
    }

    public class UnitTypeModel : PropertyChangedBase, IDataErrorInfo
    {
        readonly UnitType _unitType;

        public UnitTypeModel()
        {
            _unitType = new UnitType();
        }

        public UnitTypeModel(UnitType unitType)
        {
            _unitType = unitType;
        }

        public UnitType UnitType { get { return _unitType; } }
        public int Id { get { return _unitType.Id; } }
        public string Name
        {
            get { return _unitType.Name; }
            set 
            { 
                _unitType.Name = value;
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
            }
            return error;
        }

        string ValidateName()
        {
            return EditValidators.IsStringMissing(Name) ? Strings.UnitTypeModel_Name_missing : null;
        }

        #endregion

    }
}