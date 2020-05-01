using MVVMTutorials.WPFui.ViewModels;
using System.Windows;

namespace MVVMTutorials.WPFui.Views
{
    /// <summary>
    /// Interaction logic for SecondaryWindow.xaml
    /// </summary>
    public partial class SecondaryWindow : Window, IClosable
    {
        public SecondaryWindow()
        {
            InitializeComponent();
        }
    }
}
