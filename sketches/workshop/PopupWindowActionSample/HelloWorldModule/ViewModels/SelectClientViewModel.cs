using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using HelloWorldModule.Models;
using HelloWorldModule.Views;
using Infrastructure;
using Infrastructure.InteractionRequests;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;

namespace HelloWorldModule.ViewModels
{
    using Microsoft.Practices.Prism.PubSubEvents;

    public class SelectClientViewModel : Notification, INotifyPropertyChanged, IPopupWindowActionAware, IRegionManagerAware
    {
        private EventAggregator eventAggregator;
        
        public SelectClientViewModel()
        {
            this.Clients = new ObservableCollection<ClientData>();
            this.Clients.Add(new ClientData() { Name = "John Doe", Address = "4th Street 234", Email = "john.doe@mail.com" });
            this.Clients.Add(new ClientData() { Name = "Steve Martin", Address = "20th Street 1024", Email = "steve.martin@mail.com" });
            this.Clients.Add(new ClientData() { Name = "Jennifer Null", Address = "99th Street 10", Email = "jennifer.null@mail.com" });

            this.SelectedClient = null;

            this.OkCommand = new DelegateCommand(this.OkAction);
            this.CancelCommand = new DelegateCommand(this.CancelAction);

            this.ChangeDetailCommand = new DelegateCommand(this.ChangeDetail);

            this.RaisePropertyChanged(String.Empty);

            this.eventAggregator = ServiceLocator.Current.GetInstance<EventAggregator>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<ClientData> Clients { get; private set; }

        public ClientData SelectedClient { get; set; }

        public ICommand OkCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        public ICommand ChangeDetailCommand { get; private set; }

        // IPopupWindowActionAware implementation
        public Window HostWindow { get; set; }

        public Notification HostNotification { get; set; }

        // IRegionManagerAware implementation
        public IRegionManager RegionManager { get; set; }

        protected void OkAction()
        {
            this.eventAggregator.GetEvent<ClientSelectedEvent>().Publish(this.SelectedClient);

            if (this.HostWindow != null)
            {
                this.HostWindow.Close();
            }
        }

        protected void CancelAction()
        {
            this.eventAggregator.GetEvent<ClientSelectedEvent>().Publish(null);

            if (this.HostWindow != null)
            {
                this.HostWindow.Close();
            }
        }

        protected void ChangeDetail()
        {
            if (this.RegionManager != null)
            {
                IRegion detailsRegion = this.RegionManager.Regions["DetailsRegion"];

                while (detailsRegion.Views.Count() > 0)
                {
                    detailsRegion.Remove(detailsRegion.Views.FirstOrDefault());
                }

                var detailView = new ClientView();
                detailView.DataContext = this.SelectedClient;

                detailsRegion.Add(detailView);
            }
        }

        // INotifyPropertyChange implementation
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
