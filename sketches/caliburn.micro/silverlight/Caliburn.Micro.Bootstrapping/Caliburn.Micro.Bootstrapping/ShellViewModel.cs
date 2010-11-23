using System.ComponentModel.Composition;
using System.Windows;

namespace Caliburn.Micro.Bootstrapping
{
    [Export(typeof(IShell))]
    public class ShellViewModel : PropertyChangedBase, IShell
    {
        string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value) return;
                _name = value;
                NotifyOfPropertyChange(() => Name);
                NotifyOfPropertyChange(() => CanSayHello);
            }
        }

        public bool CanSayHello
        {
            get { return !string.IsNullOrWhiteSpace(_name); }
        }

        public void SayHello()
        {
            MessageBox.Show(string.Format("Hello, {0}", Name));
        }
    }
}