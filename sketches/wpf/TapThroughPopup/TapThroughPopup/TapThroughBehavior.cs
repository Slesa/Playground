using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace TapThroughPopup
{
    class TapThroughBehavior : Behavior<ComboBox>
    {
        protected override void OnAttached()
        {
            Mouse.AddPreviewMouseDownOutsideCapturedElementHandler(AssociatedObject, MouseDownOutsideCapturedElement);
        }

        void MouseDownOutsideCapturedElement(object sender, MouseButtonEventArgs e)
        {
            AssociatedObject.ReleaseMouseCapture();
        }
    }
}
