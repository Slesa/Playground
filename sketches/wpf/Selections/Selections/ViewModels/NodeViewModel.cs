using System.Collections.ObjectModel;

namespace Selections.ViewModels
{
    public class NodeViewModel
    {
        public NodeViewModel(string name)
        {
            Name = name;
        }

        public string Name { get; internal set; }
        public ObservableCollection<ItemViewModel> Leafs { get; set; }
    }
}