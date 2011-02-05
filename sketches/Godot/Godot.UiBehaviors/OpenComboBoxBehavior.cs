using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Godot.UiBehaviors
{
    /// Behavior for ComboBox control.
    /// It invokes drop down list when mouse is over it.
    /// <author>
    /// Jacek Ciereszko
    /// http://geekswithblogs.net/SilverBlog/
    /// </author>
    /// 

    public class OpenComboBoxBehavior : Behavior<ComboBox>
    {
        /// <summary>
        /// Called after the Behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>Override this to hook up functionality to the AssociatedObject.</remarks>
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.MouseEnter += AssociatedObjectMouseEnter;
        }

        /// <summary>
        /// Called after the Behavior is detached from an AssociatedObject.
        /// </summary>
        /// <remarks>Override this to hook up functionality to the AssociatedObject.</remarks>
        protected override void OnDetaching()
        {
            AssociatedObject.MouseEnter -= AssociatedObjectMouseEnter;
            base.OnDetaching();
        }

        /// <summary>
        /// When mouse is over ComboBox, control drop down will open
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AssociatedObjectMouseEnter(object sender, MouseEventArgs e)
        {
            AssociatedObject.IsDropDownOpen = true;
        }

    }
}
