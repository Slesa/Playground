using System.Collections.ObjectModel;

namespace Selections.ViewModels
{
    public static class CollectionCreator
    {
        public static ObservableCollection<ItemViewModel> CreateItems(int max)
        {
            var items = new ObservableCollection<ItemViewModel>();
            for (var i = 0; i < max; i++)
            {
                items.Add(new ItemViewModel("Item " + (i + 1)));
            }
            return items;
        }

        public static ObservableCollection<NodeViewModel> CreateNodes(int maxNode, int maxItem)
        {
            var nodes = new ObservableCollection<NodeViewModel>();
            for (var i = 0; i < maxNode; i++)
            {
                var node = new NodeViewModel("Node " + (i + 1));
                node.Leafs = CreateItems(maxItem);
                nodes.Add(node);
            }
            return nodes;
        }

        public static ObservableCollection<GroupedViewModel> CreateGroupedItems(int max)
        {
            var items = new ObservableCollection<GroupedViewModel>();
            for (var i = 0; i < max; i++)
            {
                items.Add(new GroupedViewModel(i, "Item " + (i + 1)));
            }
            return items;
        }
    }
}