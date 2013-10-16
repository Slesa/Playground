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

        ObservableCollection<Point> _points;
        public ObservableCollection<Point> Points
        {
            get
            {
                if (_points == null)
                {
                    _points = new ObservableCollection<Point>();
                    _points.Add(new Point(200,50));
                    _points.Add(new Point(300,150));
                    _points.Add(new Point(400,100));
                    _points.Add(new Point(500,50));
                }
                return _points;
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
