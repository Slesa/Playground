using System.Windows.Controls;
using HelloWorldModule.ViewModels;
using Microsoft.Practices.ServiceLocation;

namespace HelloWorldModule.Views
{
    /// <summary>
    /// Interaction logic for SelectClientView.xaml
    /// </summary>
    public partial class SelectClientView : UserControl
    {
        public SelectClientView()
        {
            this.DataContext = ServiceLocator.Current.GetInstance<SelectClientViewModel>();
            InitializeComponent();
        }
    }
}
