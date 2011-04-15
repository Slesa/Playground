using System.ComponentModel;
using Caliburn.Micro;
using Lucifer.DataAccess;

namespace Lucifer.Editor
{
    public abstract class EditItemViewModel<T> : Screen where T: PropertyChangedBase, IDataErrorInfo
    {
        protected readonly IDbConversation _dbConversation;
        protected T _element;

        protected EditItemViewModel(string caption, IDbConversation dbConversation)
        {
            _dbConversation = dbConversation;
            DisplayName = caption;
            _element = CreateNewElementModel();
            PrepareElement(_element);
            
        }

        protected EditItemViewModel(int elementId, string caption, IDbConversation dbConversation)
        {
            _dbConversation = dbConversation;
            DisplayName = caption;
            _element = CreateElementModel(elementId);
            PrepareElement(_element);
        }

        protected abstract T CreateNewElementModel();
        protected abstract T CreateElementModel(int elementId);

        public bool CanSave
        {
            get { return Error == null; }
        }

        public void Close()
        {
            TryClose();
        }
        
        public string this[string propertyName]
        {
            get
            {
                var error = _element[propertyName];
                return error;
            }
        }

        public string Error
        {
            get { return _element.Error; }
        }

        void PrepareElement(T element)
        {
            element.PropertyChanged += OnPropertyChanged;
        }

        void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "Error")
                return;
            NotifyOfPropertyChange(()=>CanSave);
        }
    }
}