using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Infrastructure.InteractionRequests;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace HelloWorldModule.ViewModels
{
    public class SelectItemViewModel : Notification, INotifyPropertyChanged, IPopupWindowActionAware
    {
        public SelectItemViewModel()
        {
            this.Items = new ObservableCollection<string>();
            this.Items.Add("Pizza");
            this.Items.Add("Hamburger");
            this.Items.Add("Salad");
            this.Items.Add("Noodles");
            this.Items.Add("Steak");

            this.SelectedItem = null;

            this.OkCommand = new DelegateCommand(this.OkAction);
            this.CancelCommand = new DelegateCommand(this.CancelAction);

            this.RaisePropertyChanged(String.Empty);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<string> Items { get; set; }

        public string SelectedItem { get; set; }

        public ICommand OkCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        // IPopupWindowActionAware implementation
        public Window HostWindow { get; set; }

        public Notification HostNotification { get; set; }

        protected void OkAction()
        {
            if (this.HostWindow != null)
            {
                this.HostWindow.Close();
            }
        }

        protected void CancelAction()
        {
            this.SelectedItem = null;

            if (this.HostWindow != null)
            {
                this.HostWindow.Close();
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
