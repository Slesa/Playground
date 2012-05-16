using System;
using System.ComponentModel;
using System.Windows;
using Microsoft.Practices.Prism.Modularity;
using Modularity.Wpf.Values;

namespace Modularity.Wpf
{
    public partial class ModuleControl
    {
        ModuleTrackingState _moduleTrackingState;

        public ModuleControl()
        {
            InitializeComponent();

            DataContextChanged += ControlDataContextChanged;
            OnDataContextChanged();
        }

        public event EventHandler RequestModuleLoad;
        void RaiseRequestModuleLoad()
        {
            if (RequestModuleLoad != null)
                RequestModuleLoad(this, EventArgs.Empty);
        }

        protected override void OnMouseLeftButtonUp(System.Windows.Input.MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            if (e.Handled) return;

            if (_moduleTrackingState!=null 
                && _moduleTrackingState.ExpectedInitializationMode==InitializationMode.OnDemand 
                && _moduleTrackingState.ModuleInitializationStatus==ModuleInitializationStatus.NotStarted)
                RaiseRequestModuleLoad();
            e.Handled = true;
        }

        void UpdateClickToLoadTextBlockVisibility()
        {
            if (_moduleTrackingState != null
                && _moduleTrackingState.ExpectedInitializationMode == InitializationMode.OnDemand
                && _moduleTrackingState.ModuleInitializationStatus == ModuleInitializationStatus.NotStarted)
                ClickToLoadTextBlock.Visibility = Visibility.Visible;
            else
                ClickToLoadTextBlock.Visibility = Visibility.Collapsed;
        }

        void UpdateLoadProgressTextBlockVisibility()
        {
            if (_moduleTrackingState != null
                && _moduleTrackingState.ExpectedDownloadTiming == DownloadTiming.InBackground
                && _moduleTrackingState.ModuleInitializationStatus == ModuleInitializationStatus.Downloading)
                LoadProgressPanel.Visibility = Visibility.Visible;
            else
                LoadProgressPanel.Visibility = Visibility.Collapsed;
        }

        void ControlLoaded(object sender, RoutedEventArgs e)
        {
            UpdateControls();
        }

        void ControlDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            OnDataContextChanged();
        }

        void OnDataContextChanged()
        {
            if (_moduleTrackingState != null)
                _moduleTrackingState.PropertyChanged -= TrackingStatePropertyChanged;

            _moduleTrackingState = DataContext as ModuleTrackingState;

            if (_moduleTrackingState != null)
                _moduleTrackingState.PropertyChanged += TrackingStatePropertyChanged;
            
            UpdateControls();
        }

        void TrackingStatePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateControls();
        }

        void UpdateControls()
        {
            UpdateClickToLoadTextBlockVisibility();
            UpdateLoadProgressTextBlockVisibility();
        }

    }
}
