using System;
using System.ComponentModel;
using System.Linq;
using Caliburn.Micro;
using Lucifer.Editor.Validators;
using Lucifer.Pms.Editor.Resources;
using Lucifer.Pms.Model.Entities;

namespace Lucifer.Pms.Editor.Model
{
    public class SalesItemChangedEvent
    {
        public SalesItemChangedEvent(SalesItem item)
        {
            SalesItem = item;
        }
        public SalesItem SalesItem { get; private set; }
    }

    public class SalesItemRemovedEvent
    {
        public SalesItemRemovedEvent(int id)
        {
            Id = id;
        }
        public int Id { get; private set; }
    }

    public class SalesItemModel : PropertyChangedBase, IDataErrorInfo
    {
        readonly SalesItem _salesItem;

        public SalesItemModel()
        {
            _salesItem = new SalesItem();
        }
        public SalesItemModel(SalesItem salesItem)
        {
            _salesItem = salesItem;
        }

        public SalesItem SalesItem { get { return _salesItem; } }
        public int Id { get { return _salesItem.Id; } }
        public string Name
        {
            get { return _salesItem.Name; }
            set 
            { 
                _salesItem.Name = value;
                NotifyOfPropertyChange(() => Error);
            }
        }
        public SalesFamily SalesFamily
        {
            get { return _salesItem.SalesFamily; }
            set
            {
                _salesItem.SalesFamily = value;
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
                "SalesFamily",
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
                case "SalesFamily":
                    error = ValidateSalesFamily();
                    break;
            }
            return error;
        }

        string ValidateName()
        {
            return EditValidators.IsStringMissing(Name) ? Strings.SalesFamilyItem_Name_missing : null;
        }
        string ValidateSalesFamily()
        {
            return SalesFamily==null ? Strings.SalesFamilyItem_SalesFamily_missing : null;
        }

        #endregion
    }
}