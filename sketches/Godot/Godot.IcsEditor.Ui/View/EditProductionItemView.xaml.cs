using Godot.IcsEditor.Ui.ViewModel;

namespace Godot.IcsEditor.Ui.View
{
    public partial class EditProductionItemView
    {
        public EditProductionItemView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _comboItems.ItemsSource = (DataContext as EditProductionItemViewModel).AllRecipeableItems;
            _comboUnits.ItemsSource = (DataContext as EditProductionItemViewModel).AllRecipeUnits;
        }
    }
}
