using System;
using System.ComponentModel;
using System.Linq;
using Caliburn.Micro;
using Lucifer.Editor.Validators;
using Lucifer.Ums.Editor.Resources;
using Lucifer.Ums.Model.Entities;

namespace Lucifer.Ums.Editor.Model
{
    public class UserRoleChangedEvent
    {
        public UserRoleChangedEvent(UserRole role)
        {
            UserRole = role;
        }
        public UserRole UserRole { get; private set; }
    }

    public class UserRoleRemovedEvent
    {
        public UserRoleRemovedEvent(int id)
        {
            Id = id;
        }
        public int Id { get; private set; }
    }

    public class UserRoleModel : PropertyChangedBase, IDataErrorInfo
    {
        readonly UserRole _userRole;

        public UserRoleModel()
        {
            _userRole = new UserRole();
        }
        public UserRoleModel(UserRole userRole)
        {
            _userRole = userRole;
        }

        public UserRole UserRole { get { return _userRole; } }
        public int Id { get { return _userRole.Id; } }
        public string Name { 
            get { return _userRole.Name; }
            set
            {
                _userRole.Name = value;
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
            return EditValidators.IsStringMissing(Name) ? Strings.UserRoleModel_Name_missing : null;
        }

        #endregion
    }
}