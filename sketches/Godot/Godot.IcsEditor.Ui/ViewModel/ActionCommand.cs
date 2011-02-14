using System;
using System.Windows.Input;

namespace Godot.IcsEditor.Ui.ViewModel
{
    public class ActionCommand : ICommand
    {
        readonly Action<object> _executeHandler;
        readonly Func<object, bool> _canExecuteHandler;

        public ActionCommand(Action<object> execute)
        {
            if( execute==null )
                throw new ArgumentNullException("execute");
            _executeHandler = execute;
        }

        public ActionCommand(Action<object> execute, Func<object,bool> canExecute)
            : this(execute)
        {
            _canExecuteHandler = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _executeHandler(parameter);
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecuteHandler == null)
                return true;
            return _canExecuteHandler(parameter);
        }

    }
}