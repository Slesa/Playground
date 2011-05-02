using System;
using System.ComponentModel;
using System.Linq;
using Caliburn.Micro;
using Lucifer.Editor.Validators;
using Lucifer.Pms.Editor.Resources;
using Lucifer.Pms.Model.Entities;

namespace Lucifer.Pms.Editor.Model
{
    public class SalesFamilyChangedEvent
    {
        public SalesFamilyChangedEvent(SalesFamily family)
        {
            SalesFamily = family;
        }
        public SalesFamily SalesFamily { get; private set; }
    }

    public class SalesFamilyRemovedEvent
    {
        public SalesFamilyRemovedEvent(int id)
        {
            Id = id;
        }
        public int Id { get; private set; }
    }

    public class SalesFamilyModel : PropertyChangedBase, IDataErrorInfo
    {
        readonly SalesFamily _salesFamily;

        public SalesFamilyModel()
        {
            _salesFamily = new SalesFamily();
        }
        public SalesFamilyModel(SalesFamily salesFamily)
        {
            _salesFamily = salesFamily;
        }

        public SalesFamily SalesFamily { get { return _salesFamily; } }
        public int Id { get { return _salesFamily.Id; } }
        public string Name
        {
            get { return _salesFamily.Name; }
            set 
            { 
                _salesFamily.Name = value;
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
            switch (columnName)
            {
                case "Name":
                    error = ValidateName();
                    break;
            }
            return error;
        }

        string ValidateName()
        {
            return EditValidators.IsStringMissing(Name) ? Strings.SalesFamilyModel_Name_missing : null;
        }

        #endregion
    }
}