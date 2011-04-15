using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Caliburn.Micro;
using Lucifer.DataAccess;
using Lucifer.Editor.Resources;
using NHibernate;

namespace Lucifer.Editor
{
    public abstract class EditItemViewModel<T> : Screen where T: PropertyChangedBase, IDataErrorInfo
    {
        protected readonly IDbConversation DbConversation;
        protected readonly IEventAggregator EventAggregator;
        protected T Element;

        protected EditItemViewModel(IDbConversation dbConversation, IEventAggregator eventAggregator)
        {
            DbConversation = dbConversation;
            EventAggregator = eventAggregator;
            Element = CreateNewElementModel();
            PrepareElement(Element);
            
        }

        protected EditItemViewModel(int elementId, IDbConversation dbConversation, IEventAggregator eventAggregator)
        {
            DbConversation = dbConversation;
            EventAggregator = eventAggregator;
            Element = CreateElementModel(elementId);
            PrepareElement(Element);
        }

        protected abstract T CreateNewElementModel();
        protected abstract T CreateElementModel(int elementId);

        protected bool SuccessfullySaved(System.Action action)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                DbConversation.UsingTransaction(action);
                NotifyOfPropertyChange(() => DisplayName);
                Mouse.OverrideCursor = null;
                return true;
            }
            catch (StaleObjectStateException)
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show(Strings.Error_StaleObjectState);
                return true;
            }
            catch (Exception)
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show(Strings.Error_CouldNotSaveObject);
                return false;
            }
        }

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
                var error = Element[propertyName];
                return error;
            }
        }

        public string Error
        {
            get { return Element.Error; }
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