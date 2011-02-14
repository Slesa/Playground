using System.Windows.Controls;

namespace Godot.IcsEditor.View
{

    /// <summary>
    /// Interaction logic for DlgProductionItems.xaml
    /// </summary>
    public partial class ProductionItemView : UserControl
    {
        public ProductionItemView()
        {
            InitializeComponent();
            TbxName.Focus();
        }
    }
}
