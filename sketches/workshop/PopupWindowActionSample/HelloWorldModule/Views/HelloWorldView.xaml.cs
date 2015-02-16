using System.Windows.Controls;
using HelloWorldModule.ViewModels;

namespace HelloWorldModule.Views
{
    /// <summary>
    /// Interaction logic for HelloWorldView.xaml
    /// </summary>
    public partial class HelloWorldView : UserControl
    {
        public HelloWorldView(HelloWorldViewModel viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();          
        }
    }
}
