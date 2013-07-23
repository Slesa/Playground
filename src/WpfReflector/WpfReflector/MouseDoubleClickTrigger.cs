using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace WpfReflector
{
    public class MouseDoubleClickTrigger : TriggerBase<UIElement>
    {
        protected override void OnAttached() { AssociatedObject.MouseDown += AssociatedObjectOnMouseDown; }

        protected override void OnDetaching() { AssociatedObject.MouseDown -= AssociatedObjectOnMouseDown; }

        void AssociatedObjectOnMouseDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            if (mouseButtonEventArgs.ClickCount != 2) return;
            InvokeActions(null);
        }        
    }
}