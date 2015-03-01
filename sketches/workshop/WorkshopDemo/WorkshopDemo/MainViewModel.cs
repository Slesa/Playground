using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WorkshopDemo
{
    public class WorkItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            Items = new ObservableCollection<WorkItem>();
            AddCommand = new DelegateCommand(Add, CanAdd);
            EditCommand = new DelegateCommand(Edit, CanEdit);
            RemoveCommand = new DelegateCommand(Remove, CanRemove);
        }

        public DelegateCommand AddCommand { get; set; }

        void Add(object o) { }

        bool CanAdd(object o)
        {
            return true;
        }

        public DelegateCommand EditCommand { get; set; }
        void Edit(object o) { }

        bool CanEdit(object o)
        {
            return SelectedItem!=null;
        }

        public DelegateCommand RemoveCommand { get; set; }
        void Remove(object o) { }

        bool CanRemove(object o)
        {
            return SelectedItem!=null;
        }


        public WorkItem SelectedItem { get; set; }
        public ObservableCollection<WorkItem> Items { get; internal set; }


        string _statusText;

        public string StatusText
        {
            get { return _statusText; }
            set
            {
                _statusText = value;
                OnPropertyChanged();
                OnPropertyChanged("StatusVisible");
            }
        }

        public bool StatusVisible
        {
            get { return !string.IsNullOrEmpty(StatusText); }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}