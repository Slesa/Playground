using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using Lucifer.DataAccess;
using Lucifer.Editor.Resources;

namespace Lucifer.Editor
{
    public abstract class SelectionListViewModel<T> : Screen
        where T : PropertyChangedBase, ISelectableRowViewModelBase
    {
        protected readonly IDbConversation DbConversation;
        protected readonly IEventAggregator EventAggregator;

        public IObservableCollection<T> ElementList { get; private set; }

        protected SelectionListViewModel(string caption, IDbConversation dbConversation, IEventAggregator eventAggregator)
        {
            DbConversation = dbConversation;
            EventAggregator = eventAggregator;
            DisplayName = caption;
            PrepareElementList();
        }

        public bool ItemSelected
        {
            get { return ElementList.Where(element => element.IsSelected).Count() == 1 ? true : false; }
        }

        public bool ItemsSelected
        {
            get { return ElementList.FirstOrDefault(element => element.IsSelected) != null ? true : false; }
        }

        public bool CanEdit
        {
            get { return ItemSelected; }
        }

        public bool CanRemove
        {
            get { return ItemSelected; }
        }

        protected List<T> RemoveSelectionWith(Action<T> action)
        {
            try
            {
                var removedItems = new List<T>();
                var selection = ElementList.Where(x => x.IsSelected);
                DbConversation.UsingTransaction(() =>
                {
                    foreach (var element in selection)
                    { 
                        action(element);
                        removedItems.Add(element);
                    }
                });
                return removedItems;
            }
            catch (Exception exception)
            {
                var message = Strings.Error_UnableToRemove;
                message += "\n\n" + exception.Message;
                if (exception.InnerException != null)
                    message += "\n\n" + exception.InnerException.Message;
                MessageBox.Show(message);
                return null;
            }
        }

        protected abstract BindableCollection<T> CreateElementList();

        void PrepareElementList()
        {
            ElementList = CreateElementList();
            foreach (var element in ElementList)
                ConnectElement(element);
        }

        protected void ConnectElement(T element)
        {
            element.PropertyChanged += OnPropertyChanged;
        }

        void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "IsSelected")
                return;
            NotifyOfPropertyChange(() => CanEdit);
            NotifyOfPropertyChange(() => CanRemove);
        }
    }
}