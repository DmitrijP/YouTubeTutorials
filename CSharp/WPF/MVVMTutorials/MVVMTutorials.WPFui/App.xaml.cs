using System.Windows;
using MVVMTutorials.WPFui.DataStores;
using MVVMTutorials.WPFui.ViewModels;

namespace MVVMTutorials.WPFui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static SimpleDIContainer Container { get; private set; }
        public static ViewFactory ViewFactory { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Container = new SimpleDIContainer();
            ViewFactory = new ViewFactory();
            Container.Register<MainViewModel>();
            Container.Register<SecondaryViewModel>();
            Container.Register<IEmployeeStore, EmployeeStore>();
            var vm = Container.Resolve<MainViewModel>();
            ViewFactory.OpenWindow<MainViewModel>(vm);
        }
    }

}
