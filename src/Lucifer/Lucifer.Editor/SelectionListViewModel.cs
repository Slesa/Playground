using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Caliburn.Micro;
using Lucifer.DataAccess;

namespace Lucifer.Editor
{
    public abstract class SelectionListViewModel<T> : Screen 
        where T : PropertyChangedBase, ISelectableRowViewModelBase
    {
        protected readonly IDbConversation DbConversation;
        protected readonly IEventAggregator EventAggregator;

        public ObservableCollection<T> ElementList { get; private set; }

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


        protected abstract ObservableCollection<T> CreateElementList();

        void PrepareElementList()
        {
            ElementList = CreateElementList();
            foreach (var element in ElementList)
                element.PropertyChanged += OnPropertyChanged;
        }

        void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "IsSelected")
                return;
            NotifyOfPropertyChange(()=>CanEdit);
            NotifyOfPropertyChange(()=>CanRemove);
        }
    }
}