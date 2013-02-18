using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using Microsoft.Practices.Prism.Commands;

namespace Selections.ViewModels
{
    public class SurfaceViewModel
    {
        public SurfaceViewModel()
        {
            SelectAllCommand = new DelegateCommand(SelectAll);
            ClearSelectionCommand = new DelegateCommand(ClearSelection, CanClearSelection);
            SelectedItems = new ObservableCollection<ItemViewModel>();
            SelectedItems.CollectionChanged += (sender, args) => ClearSelectionCommand.RaiseCanExecuteChanged();
        }

        public DelegateCommand SelectAllCommand { get; private set; }
        public DelegateCommand ClearSelectionCommand { get; private set; }

        void SelectAll()
        {
            if (SelectedItems.Any())
            {
                SelectedItems.Clear();
                return;
            }
            foreach (var entry in Items)
            {
                SelectedItems.Add(entry);
            }
        }

        bool CanClearSelection()
        {
            return SelectedItems.Any();
        }

        void ClearSelection()
        {
            SelectedItems.Clear();
        }

        ObservableCollection<ItemViewModel> _items;
        public ObservableCollection<ItemViewModel> Items
        {
            get { return _items ?? (_items = CollectionCreator.CreateItems(200)); }
        }

        ICollectionView _itemsViewSource;
        public ICollectionView ItemsViewSource
        {
            get
            {
                if (_itemsViewSource == null)
                {
                    _itemsViewSource = CollectionViewSource.GetDefaultView(Items);
                    _itemsViewSource.Filter = ItemFilter;
                }
                return _itemsViewSource;
            }
        }

        public ObservableCollection<ItemViewModel> SelectedItems { get; private set; }

        string _search;
        public string SearchItem
        {
            get { return _search; }
            set
            {
                _search = value;
                ItemsViewSource.Refresh();
            }
        }
        bool ItemFilter(object o)
        {
            if (_search == null) return true;
            var resource = (ItemViewModel)o;
            return resource.Name == null || resource.Name.ToLower().Contains(_search.ToLower());
        }
    }
}
