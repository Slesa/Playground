using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using Caliburn.Micro;

namespace MediaOwl.Core
{
    /// <summary>
    /// The DefaultBusyService searches for a <see cref="System.Windows.Controls.BusyIndicator"/>
    /// with a certain name (<see cref="BusyIndicatorName"/>) in the Visual Tree of a View (<see cref="FrameworkElement"/>) and updates its appearance.
    /// This class implements <see cref="IBusyService"/>.
    /// This class was taken from the Caliburn Framework.
    /// </summary>
    [Export(typeof(IBusyService))]
    public class DefaultBusyService : IBusyService
    {
        private static readonly ILog Log = LogManager.GetLog(typeof(DefaultBusyService));

        public static string BusyIndicatorName = "busyIndicator";

        private class BusyInfo
        {
            public UIElement BusyIndicator { get; set; }
            public object BusyViewModel { get; set; }
        }

        private readonly Dictionary<object, BusyInfo> loaders = new Dictionary<object, BusyInfo>();
        private readonly object lockObject = new object();
        private readonly object defaultKey = new object();

        private readonly IWindowManager windowManager;

        [ImportingConstructor]
        public DefaultBusyService(IWindowManager windowManager)
        {
            this.windowManager = windowManager;
        }


        public void MarkAsBusy(object sourceViewModel, object busyViewModel)
        {
            sourceViewModel = sourceViewModel ?? defaultKey;

            if (loaders.ContainsKey(sourceViewModel))
            {
                var info = loaders[sourceViewModel];
                info.BusyViewModel = busyViewModel;
                UpdateLoader(info);
            }
            else
            {
                var busyIndicator = TryFindBusyIndicator(sourceViewModel);

                if (busyIndicator == null)
                {
#if WP7
                    var ex = new CaliburnException("No busy indicator with name '" + BusyIndicatorName + "' was found in the UI hierarchy.");
                    Log.Error(ex);
                    throw ex;
#else
                    var notifier = busyViewModel as IActivate;
                    if (notifier == null)
                        return;

                    notifier.Activated += (o, e) =>
                    {
                        if (e.WasInitialized)
                        {
                            var info = new BusyInfo { BusyViewModel = busyViewModel };
                            loaders[sourceViewModel] = info;
                            UpdateLoader(info);
                        }
                    };

                    Log.Warn("No busy indicator with name '" + BusyIndicatorName + "' was found in the UI hierarchy. Using modal.");
                    windowManager.ShowDialog(busyViewModel);
#endif
                }
                else
                {
                    var info = new BusyInfo { BusyIndicator = busyIndicator, BusyViewModel = busyViewModel };
                    loaders[sourceViewModel] = info;
                    ToggleBusyIndicator(info, true);
                    UpdateLoader(info);
                }
            }
        }

        public void MarkAsNotBusy(object sourceViewModel)
        {
            sourceViewModel = sourceViewModel ?? defaultKey;

            if (!loaders.ContainsKey(sourceViewModel)) return;

            var info = loaders[sourceViewModel];

            lock (lockObject)
            {
                loaders.Remove(sourceViewModel);
                ToggleBusyIndicator(info, false);
            }
        }

        private static void UpdateLoader(BusyInfo info)
        {
            if (info.BusyViewModel == null || info.BusyIndicator == null)
                return;

            var indicatorType = info.BusyIndicator.GetType();
            var content = indicatorType.GetProperty("BusyContent");

            if (content == null)
            {
                var contentProperty = indicatorType.GetAttributes<ContentPropertyAttribute>(true)
                    .FirstOrDefault();

                if (contentProperty == null)
                    return;

                content = indicatorType.GetProperty(contentProperty.Name);
            }
            content.SetValue(info.BusyIndicator, info.BusyViewModel, null);
        }

        private static void ToggleBusyIndicator(BusyInfo info, bool isBusy)
        {
            if (info.BusyIndicator != null)
            {
                var busyProperty = info.BusyIndicator.GetType().GetProperty("IsBusy");
                if (busyProperty != null)
                    busyProperty.SetValue(info.BusyIndicator, isBusy, null);
                else info.BusyIndicator.Visibility = isBusy ? Visibility.Visible : Visibility.Collapsed;
            }
            else if (!isBusy)
            {
                var close = info.BusyViewModel.GetType().GetMethod("Close", Type.EmptyTypes);
                if (close != null)
                    close.Invoke(info.BusyViewModel, null);
            }
        }

        private static UIElement TryFindBusyIndicator(object viewModel)
        {
            FrameworkElement view = GetView(viewModel);
            if (view == null)
            {
                Log.Warn("Could not find view for {0}.", viewModel);
                return null;
            }

            UIElement busyIndicator = null;

            while (view != null && busyIndicator == null)
            {
                busyIndicator = view.FindName(BusyIndicatorName) as UIElement;
                view = VisualTreeHelper.GetParent(view) as FrameworkElement;
            }

            return busyIndicator;
        }

        private static FrameworkElement GetView(object viewModel)
        {
            var viewAware = viewModel as IViewAware;
            if (viewAware == null)
                return null;

            return viewAware.GetView() as FrameworkElement;
        }
    }
}