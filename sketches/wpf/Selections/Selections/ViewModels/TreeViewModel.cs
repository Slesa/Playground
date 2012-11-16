using System.Collections.ObjectModel;

namespace Selections.ViewModels
{
    public class TreeViewModel
    {
        ObservableCollection<NodeViewModel> _nodes;

        public ObservableCollection<NodeViewModel> Nodes
        {
            get { return _nodes ?? (_nodes = CollectionCreator.CreateNodes(200, 5)); }
        }

        public NodeViewModel SelectedNode { get; set; }
        public ItemViewModel SelectedItem { get; set; }
    }
}