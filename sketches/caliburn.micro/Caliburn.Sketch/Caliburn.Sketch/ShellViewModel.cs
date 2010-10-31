using System;
using System.ComponentModel.Composition;
using System.Windows;
using Caliburn.Micro;

namespace Caliburn.Sketch
{
    [Export(typeof(IShell))]
    public class ShellViewModel : PropertyChangedBase, IShell
    {
        public BindableCollection<Model> Items { get; private set; }

        public ShellViewModel()
        {
            Items = new BindableCollection<Model>
                        {
                            new Model {Id = Guid.NewGuid()},
                            new Model {Id = Guid.NewGuid()},
                            new Model {Id = Guid.NewGuid()},
                            new Model {Id = Guid.NewGuid()},
                        };
        }

        public void Add()
        {
            Items.Add(new Model { Id = Guid.NewGuid()});
        }

        public void Remove(Model child)
        {
            Items.Remove(child);
        }
        /*
        public bool CanSayHello(string name)
        {
            return !string.IsNullOrWhiteSpace(name); 
        }

        public void SayHello(string name)
        {
            MessageBox.Show(string.Format("Hello, {0}", name));
        }*/
        }
}