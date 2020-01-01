using System.Windows.Input;
using System.ComponentModel;
using BasicClientServerApp.Client.WPF.Commands;
using BasicClientServerApp.Client.BusinessLogic.Services;

namespace BasicClientServerApp.Client.WPF.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        private readonly EmployeeService employeeService;


        public MainViewModel(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
            CreateCommands();
        }

        private void CreateCommands()
        {
            GetAllEmployeeCommand = new ShowAllEmployeeCommand(this);
        }

        public ICommand GetAllEmployeeCommand { get; set; }

        private string _allEmployee;
        public string AllEmployee { get { return _allEmployee; } private set { _allEmployee = value; OnPropChanged("AllEmployee"); } }

        public async void GetAllEmployee()
        {
            AllEmployee = await employeeService.GetAllEmployeeAsync();
        }

        public bool CanGetAllEmployee()
        {
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropChanged(string memeberName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(memeberName));
        }

    }
}
