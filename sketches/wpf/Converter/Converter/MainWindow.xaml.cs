using System.Windows;

namespace Converter
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public CurrenciesViewModel ViewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
