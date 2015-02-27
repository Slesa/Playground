using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Expression.Interactivity.Core;

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
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand RemoveCommand { get; set; }

        public ObservableCollection<WorkItem> Items { get; private set; }

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