using System;
using System.ComponentModel;
using System.Linq;
using Caliburn.Micro;
using Lucifer.Editor.Validators;
using Lucifer.Ums.Editor.Resources;
using Lucifer.Ums.Model.Entities;

namespace Lucifer.Ums.Editor.Model
{
    public class UserChangedEvent
    {
        public User User;
    }
    public class UserRemovedEvent
    {
        public int Id;
    }

    public class UserModel : PropertyChangedBase, IDataErrorInfo
    {
        readonly User _user;

        public UserModel()
        {
            _user = new User();
        }
        public UserModel(User user)
        {
            _user = user;
        }

        public User User { get { return _user; } }
        public int Id { get { return _user.Id; } }
        public string Name
        {
            get { return _user.Name; }
            set
            {
                _user.Name = value;
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
            return EditValidators.IsStringMissing(Name) ? Strings.UserModel_Name_missing : null;
        }

        #endregion
    }
}