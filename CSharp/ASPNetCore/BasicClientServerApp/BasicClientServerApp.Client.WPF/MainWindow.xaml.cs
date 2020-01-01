using System;
using System.Windows;
using BasicClientServerApp.Client.WPF.ViewModels;
using BasicClientServerApp.Client.BusinessLogic.Services;

namespace BasicClientServerApp.Client.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnContentRendered(EventArgs e)
        {
            DataContext = new MainViewModel(new EmployeeService("https://localhost:44308"));
            base.OnContentRendered(e);
        }
    }
}
