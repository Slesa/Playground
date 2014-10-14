using System.Windows;
using System.Windows.Controls;

namespace SelectOnFocus
{
    public partial class App : Application
    {
        /*
        protected override void OnStartup(StartupEventArgs e)
        {
            EventManager.RegisterClassHandler(typeof(TextBox), TextBox.GotFocusEvent, new RoutedEventHandler(TextBox_GotFocus));
            base.OnStartup(e);
        }

        void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var tb = sender as TextBox;
            if (tb == null) return;
            tb.Focus();
            tb.SelectAll();
        } 
         * */
    }
}
