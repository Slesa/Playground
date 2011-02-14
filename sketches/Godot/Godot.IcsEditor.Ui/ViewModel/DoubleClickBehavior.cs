using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Godot.IcsEditor.Ui.ViewModel
{
    class DoubleClickBehavior
    {
        public static DependencyProperty MouseDoubleClickProperty = DependencyProperty.RegisterAttached(
                                            "MouseDoubleClick",
                                            typeof(ICommand),
                                            typeof(DoubleClickBehavior),
                                            new UIPropertyMetadata(MouseDoubleClick));
        public static void SetMouseDoubleClick(DependencyObject target, ICommand value)
        {
            target.SetValue(MouseDoubleClickProperty, value);
        }

        private static void MouseDoubleClick(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            var element = target as ListViewItem;

            if (element == null)
                throw new InvalidOperationException("MouseDoubleClickBehavior only on ListViewItem usable.");
            if ((e.NewValue != null) && (e.OldValue == null))
                element.MouseDoubleClick += MouseDoubleClick;
            else if ((e.NewValue == null) && (e.OldValue != null))
                element.MouseDoubleClick -= MouseDoubleClick;
        }

        private static void MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var element = (UIElement)sender;
            var command = (ICommand)element.GetValue(MouseDoubleClickProperty);
            command.Execute(e);
        }
    }
}
