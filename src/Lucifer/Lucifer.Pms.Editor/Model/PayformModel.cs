using System;
using System.ComponentModel;
using System.Linq;
using Caliburn.Micro;
using Lucifer.Editor.Validators;
using Lucifer.Pms.Editor.Resources;
using Lucifer.Pms.Model.Entities;

namespace Lucifer.Pms.Editor.Model
{
    public class PayformChangedEvent
    {
        public PayformChangedEvent(Payform payform)
        {
            Payform = payform;
        }
        public Payform Payform { get; private set; }
    }

    public class PayformRemovedEvent
    {
        public PayformRemovedEvent(int id)
        {
            Id = id;
        }
        public int Id { get; private set; }
    }

    public class PayformModel : PropertyChangedBase, IDataErrorInfo
    {
        readonly Payform _payform;

        public PayformModel()
        {
            _payform = new Payform();
        }
        public PayformModel(Payform payform)
        {
            _payform = payform;
        }

        public Payform Payform { get { return _payform; } }
        public int Id { get { return _payform.Id; } }
        public string Name
        {
            get { return _payform.Name; }
            set
            {
                _payform.Name = value;
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
            return EditValidators.IsStringMissing(Name) ? Strings.PayformModel_Name_missing : null;
        }

        #endregion
    }
}