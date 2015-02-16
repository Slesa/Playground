using System.Windows;

namespace Infrastructure.InteractionRequests.DefaultWindows
{
    /// <summary>
    /// Interaction logic for ConfirmationChildWindow.xaml
    /// </summary>
    public partial class DefaultConfirmationWindow : Window
    {
        /// <summary>
        /// The content template to use when showing <see cref="Confirmation"/> data.
        /// </summary>
        public static readonly DependencyProperty ConfirmationTemplateProperty =
            DependencyProperty.Register(
                "ConfirmationTemplate",
                typeof(DataTemplate),
                typeof(DefaultConfirmationWindow),
                new PropertyMetadata(null));

        /// <summary>
        /// Creates a new instance of ConfirmationChildWindow.
        /// </summary>
        public DefaultConfirmationWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The content template to use when showing <see cref="Confirmation"/> data.
        /// </summary>
        public DataTemplate ConfirmationTemplate
        {
            get { return (DataTemplate)GetValue(ConfirmationTemplateProperty); }
            set { SetValue(ConfirmationTemplateProperty, value); }
        }
    }
}
