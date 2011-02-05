using Godot.IcsEditor.Ui.ViewModel;

namespace Godot.IcsEditor.Ui.View
{
    public partial class EditRecipeView
    {
        public EditRecipeView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            comboItems.ItemsSource = (DataContext as EditRecipeViewModel).AllRecipeableItems;
            comboUnits.ItemsSource = (DataContext as EditRecipeViewModel).AllRecipeUnits;
        }

    }
}
