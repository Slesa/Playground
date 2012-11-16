using System.Collections.ObjectModel;

namespace Selections.ViewModels
{
    public class SingleViewModel 
    {
        ObservableCollection<ItemViewModel> _items;
        public ObservableCollection<ItemViewModel> Items
        {
            get { return _items ?? (_items = CollectionCreator.CreateItems(200)); }
        }
        public ItemViewModel SelectedItem { get; set; }
    }


}