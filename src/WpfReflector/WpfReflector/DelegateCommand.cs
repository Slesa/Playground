using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace WpfReflector
{
    /**
     * Represents an ICommand-implementation to conveniently create
     * custom ICommands using delegate expressions
     * */
    public class DelegateCommand : ICommand
    {
        Action _execute;

        public DelegateCommand(Action execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _execute.Invoke();
        }
    }
}
