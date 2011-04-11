using System;
using System.ComponentModel;
using Lucifer.Ics.Model.Entities;

namespace Lucifer.Ics.Editor.Model
{
    public class UnitTypeModel : IDataErrorInfo
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

        public int Id { get { return _unitType.Id; } }
        public string Name
        {
            get { return _unitType.Name; }
            set { _unitType.Name = value; }
        }
        public string this[string columnName]
        {
            get { return GetValidationError(columnName); }
        }

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
            return null;
        }

        public string Error
        {
            get { return null; }
        }
    }
}