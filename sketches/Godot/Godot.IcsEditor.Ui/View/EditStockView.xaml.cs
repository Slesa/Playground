using Godot.IcsEditor.Ui.ViewModel;

namespace Godot.IcsEditor.Ui.View
{
    public partial class EditStockView
    {
        public EditStockView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _comboItems.ItemsSource = (DataContext as EditStockViewModel).AllRecipeableItems;
            _comboUnits.ItemsSource = (DataContext as EditStockViewModel).AllUnits;
        }
    }
}
