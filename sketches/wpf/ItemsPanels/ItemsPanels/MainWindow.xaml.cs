using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace ItemsPanels
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ColorList.ItemsSource = typeof (Colors).GetProperties();
        }

        ObservableCollection<string> _items;
        public ObservableCollection<string> Items
        {
            get
            {
                if (_items == null)
                {
                    _items = new ObservableCollection<string>();
                    AddItem();
                    AddItem();
                    AddItem();
                }
                return _items;
            }
        }

        void AddItem()
        {
            Items.Add(CreateItem());
        }

        string CreateItem()
        {
            return string.Format("Item {0}", Items.Count + 1);
        }

        void OnAddItem(object sender, RoutedEventArgs e)
        {
            AddItem();
        }

        void OnDelItem(object sender, RoutedEventArgs e)
        {
            Items.RemoveAt(Items.Count - 1);
        }
    }
}
