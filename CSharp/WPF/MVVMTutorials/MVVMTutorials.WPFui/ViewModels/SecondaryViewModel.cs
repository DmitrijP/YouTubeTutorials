using System.Windows.Input;
using MVVMTutorials.WPFui.Commands;

namespace MVVMTutorials.WPFui.ViewModels
{
    public class SecondaryViewModel : ViewModelBase
    {
        public SecondaryViewModel()
        {
            InitializeCommands();
        }

        private string _mainTextBox;

        public string MainTextBox { get { return _mainTextBox; } set { _mainTextBox = value; Changed(); } }

        private void InitializeCommands()
        {
            SendMessageToMainWindowCommand = new GenericCommand(
                () => { });
        }

        public ICommand SendMessageToMainWindowCommand { get; set; }
    }
}
