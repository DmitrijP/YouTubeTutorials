using System;
using System.Windows.Input;

namespace MVVMTutorials.WPFui.Commands
{
    class GenericCommand : ICommand
    {
        readonly Action<object> _executeAction;
        readonly Func<object, bool> _canExecuteFunction;

        public GenericCommand(
            Action<object> executeAction,
            Func<object, bool> canExecuteFunction)
        {
            _executeAction = executeAction;
            _canExecuteFunction = canExecuteFunction;
        }

        public GenericCommand(
            Action executeAction,
            Func<bool> canExecuteFunction)
        {
            _executeAction = x => executeAction();
            _canExecuteFunction = x => canExecuteFunction();
        }

        public GenericCommand(Action<object> executeAction)
        {
            _executeAction = executeAction;
            _canExecuteFunction = (x) => { return true; };
        }

        public GenericCommand(Action executeAction)
        {
            _executeAction = (x) => { executeAction(); };
            _canExecuteFunction = (x) => { return true; };
        }

        public bool CanExecute(object parameter)
        {
            return _canExecuteFunction(parameter);
        }

        public void Execute(object parameter)
        {
            _executeAction(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
