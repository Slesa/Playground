using System;
using System.ComponentModel;

namespace BugTracker.Model
{
    [Serializable]
    public class Bug : INotifyPropertyChanged, IDataErrorInfo
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Severity Severity { get; set; }
        public DateTime CreatedOn { get; set; }

        #region IDataErrorInfo Members

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Description")
                {
                    return String.IsNullOrEmpty(Description) ? "Description may not be empty" : null;
                }

                return null;
            }
        }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region INotifyPropertyChanged Members

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}