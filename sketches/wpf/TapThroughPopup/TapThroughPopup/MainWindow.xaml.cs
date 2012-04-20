using System.Windows;
using System.Windows.Media;

namespace TapThroughPopup
{
    public partial class MainWindow : Window 
    {
        public MainWindow()
        {
            InitializeComponent();
            
            Background = Brushes.BlanchedAlmond;
        }

        void ChangeBackground(object sender, RoutedEventArgs routedEventArgs)
        {
            Background = Background == Brushes.BlanchedAlmond ? Brushes.Aquamarine : Brushes.BlanchedAlmond;
        }
        
    }
}