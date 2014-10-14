using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace SelectOnFocus
{
    public class SelectTextOnFocus :  Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.GotFocus += OnFocus;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.GotFocus -= OnFocus;
            base.OnDetaching();
        }

        void OnFocus(object sender, RoutedEventArgs e)
        {
            AssociatedObject.SelectAll();
        }
    }
}