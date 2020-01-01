using System;
using System.Windows.Input;
using BasicClientServerApp.Client.WPF.ViewModels;

namespace BasicClientServerApp.Client.WPF.Commands
{
    class ShowAllEmployeeCommand : ICommand
    {
        private readonly MainViewModel vm;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public ShowAllEmployeeCommand(MainViewModel vm)
        {
            this.vm = vm;
        }

        public bool CanExecute(object parameter) => vm.CanGetAllEmployee();

        public void Execute(object parameter) => vm.GetAllEmployee();
    }
}
