using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using Infrastructure.InteractionRequests.DefaultWindows;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Regions;

namespace Infrastructure.InteractionRequests
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class PopupWindowAction : TriggerAction<FrameworkElement>
    {
        #region DependecyProperties

        /// <summary>
        /// The content of the child window to display as part of the popup.
        /// </summary>
        public static readonly DependencyProperty WindowContentProperty =
            DependencyProperty.Register(
                "WindowContent",
                typeof(FrameworkElement),
                typeof(PopupWindowAction),
                new PropertyMetadata(null));

        /// <summary>
        /// The <see cref="DataTemplate"/> to apply to the popup content.
        /// </summary>
        public static readonly DependencyProperty ContentTemplateProperty =
            DependencyProperty.Register(
                "ContentTemplate",
                typeof(DataTemplate),
                typeof(PopupWindowAction),
                new PropertyMetadata(null));

        /// <summary>
        /// Determines if the content should be shown in a modal window or not.
        /// </summary>
        public static readonly DependencyProperty IsModalProperty =
            DependencyProperty.Register(
                "IsModal",
                typeof(bool),
                typeof(PopupWindowAction),
                new PropertyMetadata(null));

        /// <summary>
        /// Determines if the content should be initially shown centered over the view that raised the interaction request or not.
        /// </summary>
        public static readonly DependencyProperty CenterOverAssociatedObjectProperty =
            DependencyProperty.Register(
                "CenterOverAssociatedObject",
                typeof(bool),
                typeof(PopupWindowAction),
                new PropertyMetadata(null));

        #endregion

        #region Getters and Setters

        /// <summary>
        /// Gets or sets the content of the window.
        /// </summary>
        public FrameworkElement WindowContent
        {
            get { return (FrameworkElement)GetValue(WindowContentProperty); }
            set { SetValue(WindowContentProperty, value); }
        }

        /// <summary>
        /// Gets or sets the content template for the window.
        /// </summary>
        public DataTemplate ContentTemplate
        {
            get { return (DataTemplate)GetValue(ContentTemplateProperty); }
            set { SetValue(ContentTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets if the window will be modal or not.
        /// </summary>
        public bool IsModal
        {
            get { return (bool)GetValue(IsModalProperty); }
            set { SetValue(IsModalProperty, value); }
        }

        /// <summary>
        /// Gets or sets if the window will be initially shown centered over the view that raised the interaction request or not.
        /// </summary>
        public bool CenterOverAssociatedObject
        {
            get { return (bool)GetValue(CenterOverAssociatedObjectProperty); }
            set { SetValue(CenterOverAssociatedObjectProperty, value); }
        }

        #endregion

        #region PopupWindowAction logic

        /// <summary>
        /// Displays the child window and collects results for <see cref="IInteractionRequest"/>.
        /// </summary>
        /// <param name="parameter">The parameter to the action. If the action does not require a parameter, the parameter may be set to a null reference.</param>
        protected override void Invoke(object parameter)
        {
            var args = parameter as InteractionRequestedEventArgs;
            if (args == null)
            {
                return;
            }

            // If the WindowContent shouldn't be part of another visual tree.
            if (this.WindowContent != null && this.WindowContent.Parent != null)
            {
                return;
            }
            Notification context = (Notification) args.Context;
            var wrapperWindow = this.GetWindow(context);

            var callback = args.Callback;
            EventHandler handler = null;
            handler =
                (o, e) =>
                {
                    wrapperWindow.Closed -= handler;
                    wrapperWindow.Content = null;
                    callback();
                };
            wrapperWindow.Closed += handler;

            if (this.CenterOverAssociatedObject)
            {
                SizeChangedEventHandler sizeHandler = null;
                sizeHandler =
                    (o, e) =>
                    {
                        wrapperWindow.SizeChanged -= sizeHandler;

                        FrameworkElement invoker = this.AssociatedObject;
                        Point position = invoker.PointToScreen(new Point(0, 0));

                        wrapperWindow.Top = position.Y + ((invoker.ActualHeight - wrapperWindow.ActualHeight) / 2);
                        wrapperWindow.Left = position.X + ((invoker.ActualWidth - wrapperWindow.ActualWidth) / 2);
                    };
                wrapperWindow.SizeChanged += sizeHandler;
            }

            if (this.IsModal)
            {
                wrapperWindow.ShowDialog();
            }
            else
            {
                wrapperWindow.Show();
            }
        }

        /// <summary>
        /// Checks if the WindowContent or its DataContext implements IPopupWindowActionAware and IRegionManagerAware.
        /// If so, it sets the corresponding values.
        /// Also, if WindowContent does not have a RegionManager attached, it creates a new scoped RegionManager for it.
        /// </summary>
        /// <param name="notification">The notification to be set as a DataContext in the HostWindow.</param>
        /// <param name="wrapperWindow">The HostWindow</param>
        protected void PrepareContentForWindow(Notification notification, Window wrapperWindow)
        {
            if (this.WindowContent == null)
            {
                return;
            }

            // We set the WindowContent as the content of the window. 
            wrapperWindow.Content = this.WindowContent;

            IRegionManager regionManager = this.WindowContent.GetValue(RegionManager.RegionManagerProperty) as IRegionManager;

            // If the WindowContent does not have a RegionManager attached we create a new scoped RegionManager for it in order to support regions.
            if (regionManager == null)
            {
                regionManager = new RegionManager();
                this.WindowContent.SetValue(RegionManager.RegionManagerProperty, regionManager);
            }

            // If the WindowContent implements IRegionManagerAware we set the new scoped manager as the RegionManager.
            IRegionManagerAware regionManagerAwareContent = this.WindowContent as IRegionManagerAware;
            if (regionManagerAwareContent != null)
            {
                regionManagerAwareContent.RegionManager = regionManager;
            }

            // If the WindowContent's DataContext implements IRegionManagerAware we set the new scoped manager as the RegionManager.
            IRegionManagerAware regionManagerAwareDataContext = this.WindowContent.DataContext as IRegionManagerAware;
            if (regionManagerAwareDataContext != null)
            {
                regionManagerAwareDataContext.RegionManager = regionManager;
            }

            // If the WindowContent implements IPopupWindowActionAware, we set the corresponding values.
            IPopupWindowActionAware popupAwareContent = this.WindowContent as IPopupWindowActionAware;
            if (popupAwareContent != null)
            {
                popupAwareContent.HostWindow = wrapperWindow;
                popupAwareContent.HostNotification = notification;
            }

            // If the WindowContent's DataContext implements IPopupWindowActionAware, we set the corresponding values.
            IPopupWindowActionAware popupAwareDataContext = this.WindowContent.DataContext as IPopupWindowActionAware;
            if (popupAwareDataContext != null)
            {
                popupAwareDataContext.HostWindow = wrapperWindow;
                popupAwareDataContext.HostNotification = notification;
            }
        }

        #endregion

        #region Window creation methods

        /// <summary>
        /// Returns the window to display as part of the trigger action.
        /// </summary>
        /// <param name="notification">The notification to be set as a DataContext in the window.</param>
        /// <returns></returns>
        protected Window GetWindow(Notification notification)
        {
            Window wrapperWindow;

            /*if (this.WindowContent != null)
            {
                wrapperWindow = new Window();

                // If the WindowContent does not have its own DataContext, it will inherit this one.
                wrapperWindow.DataContext = notification;
                wrapperWindow.Title = notification.Title;

                wrapperWindow.SizeToContent = SizeToContent.WidthAndHeight;

                this.PrepareContentForWindow(notification, wrapperWindow);
            }
            else */

            {
                wrapperWindow = this.CreateDefaultWindow(notification);
                if (this.WindowContent != null)
                {
                    notification.Content = this.WindowContent;    
                }                
                wrapperWindow.DataContext = notification;
            }

            return wrapperWindow;
        }

        private Window CreateDefaultWindow(Notification notification)
        {
            return notification is Confirmation
                ? (Window)new DefaultConfirmationWindow { ConfirmationTemplate = this.ContentTemplate }
                : new DefaultNotificationWindow { NotificationTemplate = this.ContentTemplate };
        }

        #endregion
    }
}
