using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.ViewModel;

namespace Selections.ViewModels
{
    public class TreeViewModel : NotificationObject
    {
        ObservableCollection<NodeViewModel> _nodes;

        public ObservableCollection<NodeViewModel> Nodes
        {
            get { return _nodes ?? (_nodes = CollectionCreator.CreateNodes(100, 5)); }
        }

        NodeViewModel _selectedNode;
        public NodeViewModel SelectedNode
        {
            get { return _selectedNode; }
            set
            {
                _selectedNode = value;
                RaisePropertyChanged(() => SelectedNode);
            }
        }

        ItemViewModel _selectedLeaf;
        public ItemViewModel SelectedLeaf
        {
            get { return _selectedLeaf; }
            set
            {
                _selectedLeaf = value;
                RaisePropertyChanged(() => SelectedLeaf);
            }
        }

        object _selectedItem;
        public object SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SelectedLeaf = value as ItemViewModel;
                SelectedNode = value as NodeViewModel;
            }
        }
    }
}