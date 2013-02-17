using System.Windows;
using ProjectCleaner.ViewModels;

namespace ProjectCleaner.Views
{
    public partial class Shell : Window
    {
        public Shell(ShellViewModel shellViewModel)
        {
            InitializeComponent();
            DataContext = shellViewModel;
        }

        void OnDrop(object sender, DragEventArgs e)
        {
            var viewModel = (ShellViewModel)DataContext;
            viewModel.HandleDrop(e);
        }

        void OnDragOver(object sender, DragEventArgs e)
        {
            var viewModel = (ShellViewModel)DataContext;
            if(!viewModel.HandleDragOver(e)) e.Effects = DragDropEffects.None;
        }

        void OnDragLeave(object sender, DragEventArgs e)
        {
            var viewModel = (ShellViewModel)DataContext;
            viewModel.HandleDragLeave();
        }
    }
}
