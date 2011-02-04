using System.Windows;

namespace Caliburn.Micro.Hello
{
    public class ShellViewModel : PropertyChangedBase
    {
        string _name;

        public string Name
        {
            get { return _name; }
            set 
            { 
                _name = value;
                NotifyOfPropertyChange(() => Name);
                NotifyOfPropertyChange(() => CanSayHello);
            }
        }

        public bool CanSayHello
        {
            get { return !string.IsNullOrEmpty(Name); }
        }

        public void SayHello()
        {
            MessageBox.Show(string.Format("Hello, {0}!", Name));
        }

    }
}