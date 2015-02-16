using System.ComponentModel;
using System.Windows.Input;
using HelloWorldModule.Models;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.ServiceLocation;

namespace HelloWorldModule.ViewModels
{
    using Microsoft.Practices.Prism.PubSubEvents;

    public class HelloWorldViewModel : INotifyPropertyChanged
    {
        private string result;

        private EventAggregator eventAggregator;

        public HelloWorldViewModel()
        {
            this.RaiseConfirmation = new DelegateCommand(this.OnRaiseConfirmation);

            this.RaiseNotification = new DelegateCommand(this.OnRaiseNotification);

            this.RaiseSelectItem = new DelegateCommand(this.OnRaiseSelectItem);

            this.RaiseSelectClient = new DelegateCommand(this.OnRaiseSelectClient);

            this.ConfirmationRequest = new InteractionRequest<Confirmation>();

            this.NotificationRequest = new InteractionRequest<Notification>();

            this.SelectItemRequest = new InteractionRequest<SelectItemViewModel>();

            this.SelectClientRequest = new InteractionRequest<Notification>();

            this.eventAggregator = ServiceLocator.Current.GetInstance<EventAggregator>();

            this.eventAggregator.GetEvent<ClientSelectedEvent>().Subscribe(this.ClientSelected);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public InteractionRequest<Confirmation> ConfirmationRequest { get; private set; }

        public InteractionRequest<Notification> NotificationRequest { get; private set; }

        public InteractionRequest<SelectItemViewModel> SelectItemRequest { get; private set; }

        public InteractionRequest<Notification> SelectClientRequest { get; private set; }

        public ICommand RaiseConfirmation { get; private set; }

        public ICommand RaiseNotification { get; private set; }

        public ICommand RaiseSelectItem { get; private set; }

        public ICommand RaiseSelectClient { get; private set; }

        public string Result
        {
            get
            {
                return this.result;
            }

            set
            {
                this.result = value;
                this.PropertyChanged(this, new PropertyChangedEventArgs("Result"));
            }
        }

        public void ClientSelected(ClientData client)
        {
            if (client != null)
            {
                this.Result = "The user selected the client: " + client.Name;
            }
            else
            {
                this.Result = "The user didn't select a client.";
            }
        }

        private void OnRaiseConfirmation()
        {
            this.ConfirmationRequest.Raise(
                new Confirmation { Content = "Confirmation Message", Title = "WPF Confirmation" },
                (cb) => { Result = cb.Confirmed ? "The user confirmed" : "The user cancelled"; });
        }

        private void OnRaiseNotification()
        {
            this.NotificationRequest.Raise(
               new Notification { Content = "Notification Message", Title = "WPF Notification" },
               dummy => { Result = "The user was notified"; });
        }

        private void OnRaiseSelectItem()
        {
            this.SelectItemRequest.Raise(
                new SelectItemViewModel { Title = "Items" },
                (vm) =>
                {
                    if (vm.SelectedItem != null)
                    {
                        Result = "The user selected: " + vm.SelectedItem;
                    }
                    else
                    {
                        Result = "The user didn't select an item.";
                    }
                });
        }

        private void OnRaiseSelectClient()
        {
            this.SelectClientRequest.Raise(
               new Notification { Title = "Clients" });
        }
    }
}

